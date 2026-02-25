using System;
using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public class ChatMessageRetriever : IInternalChatMessageRetriever, IChatMessageRetriever
	{
		private const long InvalidSequenceNumber = long.MaxValue;

		private readonly AbstractLogger logger;

		private readonly IMixWebCallFactory webCallFactory;

		private readonly IInternalChatThread chatThread;

		private readonly int maximumMessageCount;

		private readonly ISessionStatus sessionStatus;

		private readonly IUserDatabase userDatabase;

		private readonly IChatMessageParser chatMessageParser;

		private readonly IEpochTime epochTime;

		private readonly List<IInternalChatMessage> excludedLocalMessages;

		private readonly List<IInternalChatMessage> retrievedMessages;

		private bool completedFirstRetrieval;

		private bool retrievedWhenOffline;

		private long? lastLocalTimeChecked;

		private long nextLocalSequenceNumberToCheck;

		private long nextRemoteSequenceNumberToCheck;

		private bool sequenceNumberRetrievalFailed;

		public ChatMessageRetriever(AbstractLogger logger, IMixWebCallFactory webCallFactory, IInternalChatThread chatThread, int maximumMessageCount, ISessionStatus sessionStatus, IUserDatabase userDatabase, IChatMessageParser chatMessageParser, IEpochTime epochTime)
		{
			this.logger = logger;
			this.webCallFactory = webCallFactory;
			this.chatThread = chatThread;
			this.maximumMessageCount = maximumMessageCount;
			this.sessionStatus = sessionStatus;
			this.userDatabase = userDatabase;
			this.chatMessageParser = chatMessageParser;
			this.epochTime = epochTime;
			excludedLocalMessages = new List<IInternalChatMessage>();
			retrievedMessages = new List<IInternalChatMessage>();
			nextRemoteSequenceNumberToCheck = ((chatThread.LatestSequenceNumber == 0L) ? long.MaxValue : chatThread.LatestSequenceNumber);
			nextLocalSequenceNumberToCheck = nextRemoteSequenceNumberToCheck;
		}

		public void RetrieveMessages(Action<IRetrieveChatThreadMessagesResult> localCallback, Action<IRetrieveChatThreadMessagesResult> remoteCallback)
		{
			Action<bool, IEnumerable<IChatMessage>, bool> action = delegate(bool success, IEnumerable<IChatMessage> messages, bool isDone)
			{
				remoteCallback(new RetrieveChatThreadMessagesResult(success, messages, isDone));
			};
			try
			{
				RetrieveLocalMessagesWithIndexingCheck(delegate(IInternalChatMessage message, IRetrieveChatThreadMessagesResult result)
				{
					OnLocalMessagesRetrieved(message, result, localCallback, remoteCallback);
				});
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				action(false, null, false);
			}
		}

		private void OnLocalMessagesRetrieved(IInternalChatMessage lastMessage, IRetrieveChatThreadMessagesResult result, Action<IRetrieveChatThreadMessagesResult> localCallback, Action<IRetrieveChatThreadMessagesResult> remoteCallback)
		{
			Action<bool, IEnumerable<IChatMessage>, bool> action = delegate(bool success, IEnumerable<IChatMessage> messages, bool isDone)
			{
				remoteCallback(new RetrieveChatThreadMessagesResult(success, messages, isDone));
			};
			localCallback(result);
			if (sequenceNumberRetrievalFailed)
			{
				action(false, null, true);
				return;
			}
			if (!completedFirstRetrieval && sessionStatus.IsPaused)
			{
				retrievedWhenOffline = true;
				GetChatThreadRecentMessagesWithoutLatestSequenceNumber(action);
				return;
			}
			if (nextRemoteSequenceNumberToCheck < 1)
			{
				action(true, new IChatMessage[0], true);
				return;
			}
			if (nextRemoteSequenceNumberToCheck == long.MaxValue)
			{
				GetChatThreadRecentMessages(lastMessage, action);
				return;
			}
			IEnumerable<IChatMessage> chatMessages = result.ChatMessages;
			int num = chatMessages.Count();
			if (lastMessage != null && num == maximumMessageCount && nextLocalSequenceNumberToCheck > 0)
			{
				if (lastMessage.SequenceNumber >= 1)
				{
					nextRemoteSequenceNumberToCheck = lastMessage.SequenceNumber - 1;
				}
				action(true, Enumerable.Empty<IChatMessage>(), nextRemoteSequenceNumberToCheck < 1);
			}
			else
			{
				List<long?> sequenceRanges = GetSequenceRanges(num, chatMessages.Cast<IInternalChatMessage>());
				if (sequenceRanges.Count == 0)
				{
					action(true, Enumerable.Empty<IChatMessage>(), nextRemoteSequenceNumberToCheck < 1);
				}
				else
				{
					GetChatThreadMessagesBySequenceRanges(sequenceRanges, action);
				}
			}
		}

		private void GetChatThreadRecentMessages(IInternalChatMessage lastMessage, Action<bool, IEnumerable<IChatMessage>, bool> remoteCallback)
		{
			List<long?> list = new List<long?>();
			list.Add(chatThread.ChatThreadId);
			List<long?> list2 = list;
			if (lastMessage == null)
			{
				list2.Add(1L);
			}
			else
			{
				list2.Add(lastMessage.SequenceNumber - 1);
			}
			GetChatThreadsRecentMessagesRequest getChatThreadsRecentMessagesRequest = new GetChatThreadsRecentMessagesRequest();
			getChatThreadsRecentMessagesRequest.LowerBoundSequenceNumbers = list2;
			getChatThreadsRecentMessagesRequest.MaxMessagesPerChatThread = maximumMessageCount;
			GetChatThreadsRecentMessagesRequest request = getChatThreadsRecentMessagesRequest;
			IWebCall<GetChatThreadsRecentMessagesRequest, GetChatThreadMessagesResponse> webCall = webCallFactory.ChatThreadMessagesRecentPost(request);
			webCall.OnResponse += delegate(object sender, WebCallEventArgs<GetChatThreadMessagesResponse> e)
			{
				HandleGetChatMessagesSuccess(e.Response, remoteCallback);
			};
			webCall.OnError += delegate
			{
				remoteCallback(false, null, false);
			};
			webCall.Execute();
		}

		private void GetChatThreadRecentMessagesWithoutLatestSequenceNumber(Action<bool, IEnumerable<IChatMessage>, bool> remoteCallback)
		{
			List<long?> list = new List<long?>();
			list.Add(chatThread.ChatThreadId);
			list.Add(nextRemoteSequenceNumberToCheck + 1);
			List<long?> lowerBoundSequenceNumbers = list;
			GetChatThreadsRecentMessagesRequest getChatThreadsRecentMessagesRequest = new GetChatThreadsRecentMessagesRequest();
			getChatThreadsRecentMessagesRequest.LowerBoundSequenceNumbers = lowerBoundSequenceNumbers;
			getChatThreadsRecentMessagesRequest.MaxMessagesPerChatThread = maximumMessageCount;
			GetChatThreadsRecentMessagesRequest request = getChatThreadsRecentMessagesRequest;
			IWebCall<GetChatThreadsRecentMessagesRequest, GetChatThreadMessagesResponse> webCall = webCallFactory.ChatThreadMessagesRecentPost(request);
			webCall.OnResponse += delegate(object sender, WebCallEventArgs<GetChatThreadMessagesResponse> e)
			{
				HandleGetChatMessagesSuccess(e.Response, remoteCallback);
			};
			webCall.OnError += delegate
			{
				remoteCallback(false, null, false);
			};
			webCall.Execute();
		}

		private List<long?> GetSequenceRanges(int localMessageCount, IEnumerable<IInternalChatMessage> chatMessages)
		{
			List<long?> list = new List<long?>();
			long num = nextRemoteSequenceNumberToCheck - maximumMessageCount + 1;
			if (num < 1)
			{
				num = 1L;
			}
			long num2 = nextRemoteSequenceNumberToCheck;
			if (localMessageCount == 0)
			{
				list.Add(num);
				list.Add(num2);
			}
			else
			{
				bool flag = false;
				foreach (IInternalChatMessage chatMessage in chatMessages)
				{
					long sequenceNumber = chatMessage.SequenceNumber;
					if (sequenceNumber >= num)
					{
						flag = true;
						if (sequenceNumber == num)
						{
							num++;
							continue;
						}
						list.Add(num);
						list.Add(sequenceNumber - 1);
						num = chatMessage.SequenceNumber + 1;
					}
				}
				if (num2 >= num || !flag)
				{
					list.Add(num);
					list.Add(num2);
				}
			}
			return list;
		}

		private void GetChatThreadMessagesBySequenceRanges(List<long?> ranges, Action<bool, IEnumerable<IChatMessage>, bool> remoteCallback)
		{
			GetChatThreadMessagesBySequenceRangesRequest getChatThreadMessagesBySequenceRangesRequest = new GetChatThreadMessagesBySequenceRangesRequest();
			getChatThreadMessagesBySequenceRangesRequest.ChatThreadId = chatThread.ChatThreadId;
			getChatThreadMessagesBySequenceRangesRequest.Ranges = ranges;
			GetChatThreadMessagesBySequenceRangesRequest request = getChatThreadMessagesBySequenceRangesRequest;
			IWebCall<GetChatThreadMessagesBySequenceRangesRequest, GetChatThreadMessagesResponse> webCall = webCallFactory.ChatThreadMessagesBySequenceRangePost(request);
			webCall.OnResponse += delegate(object sender, WebCallEventArgs<GetChatThreadMessagesResponse> e)
			{
				HandleGetChatMessagesSuccess(e.Response, remoteCallback);
			};
			webCall.OnError += delegate
			{
				remoteCallback(false, null, false);
			};
			webCall.Execute();
		}

		public void RetrieveMessages(Action<IRetrieveChatThreadMessagesResult> callback)
		{
			try
			{
				RetrieveLocalMessagesWithIndexingCheck(delegate(IInternalChatMessage message, IRetrieveChatThreadMessagesResult result)
				{
					callback(result);
				});
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				callback(new RetrieveChatThreadMessagesResult(false, new IChatMessage[0], true));
			}
		}

		private void RetrieveLocalMessagesWithIndexingCheck(Action<IInternalChatMessage, IRetrieveChatThreadMessagesResult> callback)
		{
			long chatThreadId = chatThread.ChatThreadId;
			if (!chatThread.AreSequenceNumbersIndexed)
			{
				userDatabase.IndexSequenceNumberField(chatThreadId, delegate
				{
					chatThread.AreSequenceNumbersIndexed = true;
					RetrieveLocalMessagesWithZeroSequenceNumberCheck(callback);
				});
			}
			else
			{
				RetrieveLocalMessagesWithZeroSequenceNumberCheck(callback);
			}
		}

		private void RetrieveLocalMessagesWithZeroSequenceNumberCheck(Action<IInternalChatMessage, IRetrieveChatThreadMessagesResult> callback)
		{
			long chatThreadId = chatThread.ChatThreadId;
			if (userDatabase.ContainsZeroSequenceNumber(chatThreadId) && !sequenceNumberRetrievalFailed)
			{
				GetChatThreadSequenceNumberRequest getChatThreadSequenceNumberRequest = new GetChatThreadSequenceNumberRequest();
				getChatThreadSequenceNumberRequest.ChatThreadId = chatThreadId;
				getChatThreadSequenceNumberRequest.StartMessageTimestamp = epochTime.Milliseconds;
				GetChatThreadSequenceNumberRequest request = getChatThreadSequenceNumberRequest;
				IWebCall<GetChatThreadSequenceNumberRequest, GetChatThreadSequenceNumberResponse> webCall = webCallFactory.ChatThreadMessagesSequenceNumberPost(request);
				webCall.OnResponse += delegate(object sender, WebCallEventArgs<GetChatThreadSequenceNumberResponse> e)
				{
					HandleGetSequenceNumberSuccess(e.Response, callback);
				};
				webCall.OnError += delegate
				{
					sequenceNumberRetrievalFailed = true;
					RetrieveLocalMessages(callback);
				};
				webCall.Execute();
			}
			else
			{
				RetrieveLocalMessages(callback);
			}
		}

		private IEnumerable<IInternalChatMessage> RetrieveMemoryMessages()
		{
			IEnumerable<IInternalChatMessage> internalChatMessages = chatThread.InternalChatMessages;
			if (!internalChatMessages.Any() || sequenceNumberRetrievalFailed)
			{
				return Enumerable.Empty<IInternalChatMessage>();
			}
			long end = ((nextLocalSequenceNumberToCheck == long.MaxValue) ? internalChatMessages.Last().SequenceNumber : nextLocalSequenceNumberToCheck);
			long start = end - maximumMessageCount + 1;
			if (start < 1 && end >= 0)
			{
				start--;
			}
			return internalChatMessages.Where((IInternalChatMessage m) => m.SequenceNumber >= start && m.SequenceNumber <= end);
		}

		private void RetrieveLocalMessages(Action<IInternalChatMessage, IRetrieveChatThreadMessagesResult> callback)
		{
			IEnumerable<IInternalChatMessage> enumerable = RetrieveMemoryMessages();
			if (!enumerable.Any() || enumerable.Count() != maximumMessageCount)
			{
				enumerable = (sequenceNumberRetrievalFailed ? chatMessageParser.RetrieveParsedChatMessages(chatThread, maximumMessageCount, lastLocalTimeChecked, excludedLocalMessages) : ((nextLocalSequenceNumberToCheck != long.MaxValue) ? chatMessageParser.RetrieveParsedChatMessages(chatThread, nextLocalSequenceNumberToCheck, maximumMessageCount) : chatMessageParser.RetrieveParsedChatMessages(chatThread, maximumMessageCount)));
				IOrderedEnumerable<IInternalChatMessage> orderedEnumerable2;
				if (sequenceNumberRetrievalFailed)
				{
					IOrderedEnumerable<IInternalChatMessage> orderedEnumerable = enumerable.OrderBy((IInternalChatMessage m) => m.Created);
					orderedEnumerable2 = orderedEnumerable;
				}
				else
				{
					orderedEnumerable2 = from m in enumerable
						orderby m.SequenceNumber, m.Created
						select m;
				}
				enumerable = orderedEnumerable2;
			}
			IList<IInternalChatMessage> list = (enumerable as IList<IInternalChatMessage>) ?? enumerable.ToList();
			IInternalChatMessage lastMessage = list.FirstOrDefault();
			retrievedMessages.AddRange(list);
			bool isDone = list.Count < maximumMessageCount;
			if (sequenceNumberRetrievalFailed)
			{
				if (lastMessage != null)
				{
					lastLocalTimeChecked = lastMessage.Created;
					excludedLocalMessages.RemoveAll((IInternalChatMessage m) => m.Created != lastMessage.Created);
					excludedLocalMessages.AddRange(list.Where((IInternalChatMessage m) => m.Created == lastMessage.Created));
				}
			}
			else if (lastMessage != null)
			{
				nextLocalSequenceNumberToCheck = lastMessage.SequenceNumber - 1;
				SetNextLocalSequenceNumberToCheck(lastMessage);
			}
			RetrieveChatThreadMessagesResult arg = new RetrieveChatThreadMessagesResult(true, list.Cast<IChatMessage>(), isDone);
			callback(lastMessage, arg);
		}

		private void HandleGetChatMessagesSuccess(GetChatThreadMessagesResponse response, Action<bool, IEnumerable<IChatMessage>, bool> callRemoteCallback)
		{
			try
			{
				if (!ValidateResponse(response))
				{
					logger.Critical("Failed to validate get chat messages response: " + JsonParser.ToJson(response));
					callRemoteCallback(false, null, false);
					return;
				}
				List<IInternalChatMessage> list = new List<IInternalChatMessage>();
				foreach (GagChatMessage item in response.Gag)
				{
					chatMessageParser.ParseMessage(item, chatThread, list, retrievedMessages);
				}
				foreach (GameStateChatMessage item2 in response.GameState)
				{
					chatMessageParser.ParseMessage(item2, chatThread, list, retrievedMessages);
				}
				foreach (GameEventChatMessage item3 in response.GameEvent)
				{
					chatMessageParser.ParseMessage(item3, chatThread, list, retrievedMessages);
				}
				foreach (PhotoChatMessage item4 in response.Photo)
				{
					chatMessageParser.ParseMessage(item4, chatThread, list, retrievedMessages);
				}
				foreach (StickerChatMessage item5 in response.Sticker)
				{
					chatMessageParser.ParseMessage(item5, chatThread, list, retrievedMessages);
				}
				foreach (TextChatMessage item6 in response.Text)
				{
					chatMessageParser.ParseMessage(item6, chatThread, list, retrievedMessages);
				}
				foreach (VideoChatMessage item7 in response.Video)
				{
					chatMessageParser.ParseMessage(item7, chatThread, list, retrievedMessages);
				}
				foreach (MemberListChangedChatMessage item8 in response.MemberListChanged)
				{
					chatMessageParser.ParseMessage(item8, chatThread, list, retrievedMessages);
				}
				chatMessageParser.InsertParsedChatMessageDocuments();
				list = (from m in list
					orderby m.SequenceNumber, m.Created
					select m).ToList();
				IInternalChatMessage lastMessage = list.FirstOrDefault();
				if (lastMessage != null)
				{
					nextRemoteSequenceNumberToCheck = lastMessage.SequenceNumber - 1;
					SetNextLocalSequenceNumberToCheck(lastMessage);
					long created = lastMessage.Created;
					long? num = lastLocalTimeChecked;
					if (!num.HasValue || created <= lastLocalTimeChecked.Value)
					{
						lastLocalTimeChecked = created;
						excludedLocalMessages.RemoveAll((IInternalChatMessage m) => m.Created != lastMessage.Created);
						excludedLocalMessages.AddRange(list.Where((IInternalChatMessage m) => m.Created == lastMessage.Created));
					}
				}
				if (!completedFirstRetrieval)
				{
					completedFirstRetrieval = true;
				}
				bool arg = false;
				if (retrievedWhenOffline)
				{
					retrievedWhenOffline = false;
				}
				else
				{
					arg = nextRemoteSequenceNumberToCheck < 1 || list.Count == 0;
				}
				callRemoteCallback(true, list.Cast<IChatMessage>(), arg);
			}
			catch (Exception ex)
			{
				logger.Critical("Unhandled exception: " + ex);
				callRemoteCallback(false, null, false);
			}
		}

		private void SetNextLocalSequenceNumberToCheck(IInternalChatMessage message)
		{
			nextLocalSequenceNumberToCheck = message.SequenceNumber - 1;
			if (nextLocalSequenceNumberToCheck == 0L)
			{
				nextLocalSequenceNumberToCheck = -1L;
			}
		}

		private static bool ValidateResponse(GetChatThreadMessagesResponse response)
		{
			return response.Gag != null && response.GameEvent != null && response.GameState != null && response.MemberListChanged != null && response.Photo != null && response.Sticker != null && response.Text != null && response.Video != null;
		}

		private static bool ValidateResponse(GetChatThreadSequenceNumberResponse response)
		{
			if (response.SequenceNumbers == null)
			{
				return false;
			}
			int num = response.SequenceNumbers.Count;
			if (num % 2 == 1)
			{
				return false;
			}
			for (int i = 1; i < num; i += 2)
			{
				if (!response.SequenceNumbers[i].HasValue)
				{
					response.SequenceNumbers.RemoveRange(i - 1, 2);
					i -= 2;
					num -= 2;
				}
				else if (response.SequenceNumbers[i].Value <= 0)
				{
					return false;
				}
			}
			return true;
		}

		private void HandleGetSequenceNumberSuccess(GetChatThreadSequenceNumberResponse response, Action<IInternalChatMessage, IRetrieveChatThreadMessagesResult> callback)
		{
			if (!ValidateResponse(response))
			{
				logger.Critical("Failed to validate get sequence numbers response: " + JsonParser.ToJson(response));
				callback(null, new RetrieveChatThreadMessagesResult(false, null, false));
				return;
			}
			sequenceNumberRetrievalFailed = false;
			List<long> sequenceNumberList = response.SequenceNumbers.Cast<long>().ToList();
			userDatabase.UpdateChatMessageSequenceNumbers(chatThread.ChatThreadId, sequenceNumberList);
			RetrieveLocalMessages(callback);
		}
	}
}
