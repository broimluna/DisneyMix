using System;
using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public abstract class AbstractChatThread : IInternalChatThread
	{
		private static class MessageSender<TMessage, TNotification> where TMessage : IInternalChatMessage where TNotification : BaseNotification
		{
			public static void Send(TMessage message, AbstractLogger logger, IChatMessageHandler chatMessageHandler, IUserDatabase userDatabase, long chatThreadId, Action<Action<TNotification>, Action<string>> chatMessageSender, Action<TNotification> messageSendCompleter, Action<bool, TNotification> callback, Action eventTriggerer, Action<Action<uint>> databaseMessageInserter, Action<TMessage> sequenceNumberUpdater)
			{
				chatMessageSender(delegate(TNotification notification)
				{
					if (!message.Sent)
					{
						if (messageSendCompleter != null)
						{
							messageSendCompleter(notification);
						}
						callback(true, notification);
						if (eventTriggerer != null)
						{
							eventTriggerer();
						}
					}
					else
					{
						callback(true, notification);
					}
					if (message.ChatMessageDocumentId.HasValue)
					{
						chatMessageHandler.DeleteChatMessageDocument(message.ChatMessageDocumentId.Value, chatThreadId);
						message.ChatMessageDocumentId = null;
					}
				}, delegate
				{
					try
					{
						if (!message.ChatMessageDocumentId.HasValue)
						{
							databaseMessageInserter(delegate(uint documentId)
							{
								message.ChatMessageDocumentId = documentId;
								userDatabase.UpdateLatestSequenceNumber(chatThreadId, message.SequenceNumber);
								sequenceNumberUpdater(message);
								callback(false, (TNotification)null);
							});
						}
						else
						{
							callback(false, (TNotification)null);
						}
					}
					catch (Exception ex)
					{
						logger.Critical("Unhandled exception: " + ex);
						callback(false, (TNotification)null);
					}
				});
			}
		}

		private const string GameStateSizeExceeded = "GAMESTATE_SIZE_EXCEEDED";

		private const string InsufficientReadPermission = "INSUFFICIENT_READ_PERMISSION";

		private const string MessageNotFound = "NOT_FOUND";

		private readonly AbstractLogger logger;

		private readonly IChatMessageRetrieverFactory chatMessageRetrieverFactory;

		protected readonly long chatThreadId;

		private readonly string hashedChatThreadId;

		private readonly IChatMessageSender messageSender;

		private readonly IList<IInternalRemoteChatMember> remoteMembers;

		private readonly IList<IInternalRemoteChatMember> formerRemoteMembers;

		protected readonly IList<IInternalChatMessage> messages;

		private readonly string localUserId;

		private readonly IChatMessageFactory chatMessageFactory;

		private readonly IUserDatabase userDatabase;

		private readonly IChatMessageParser chatMessageParser;

		private readonly IChatMessageHandler chatMessageHandler;

		protected readonly IMixWebCallFactory mixWebCallFactory;

		private readonly INotificationQueue notificationQueue;

		private long latestSequenceNumber;

		private long localSequenceNumber;

		public IChatThreadTrustLevel TrustLevel { get; private set; }

		public IChatThreadTrustLevel InternalTrustLevel
		{
			get
			{
				return TrustLevel;
			}
		}

		public uint UnreadMessageCount { get; private set; }

		public IEnumerable<IChatMessage> ChatMessages
		{
			get
			{
				return messages.Cast<IChatMessage>();
			}
		}

		public IEnumerable<IInternalChatMessage> InternalChatMessages
		{
			get
			{
				return messages;
			}
		}

		public long ChatThreadId
		{
			get
			{
				return chatThreadId;
			}
		}

		public IEnumerable<IRemoteChatMember> RemoteMembers
		{
			get
			{
				return remoteMembers.Cast<IRemoteChatMember>();
			}
		}

		public IEnumerable<IInternalRemoteChatMember> InternalMembers
		{
			get
			{
				return remoteMembers;
			}
		}

		public IEnumerable<IRemoteChatMember> FormerRemoteMembers
		{
			get
			{
				return formerRemoteMembers.Cast<IRemoteChatMember>();
			}
		}

		public IEnumerable<IInternalRemoteChatMember> InternalFormerMembers
		{
			get
			{
				return formerRemoteMembers;
			}
		}

		public IChatThreadNickname Nickname { get; private set; }

		public IChatThreadNickname InternalNickname
		{
			get
			{
				return Nickname;
			}
		}

		public string Id
		{
			get
			{
				return hashedChatThreadId;
			}
		}

		public bool AreSequenceNumbersIndexed { get; set; }

		public long LatestSequenceNumber
		{
			get
			{
				return latestSequenceNumber;
			}
			set
			{
				latestSequenceNumber = value;
				localSequenceNumber = latestSequenceNumber + 1;
			}
		}

		public long NextChatMessageSequenceNumber
		{
			get
			{
				IInternalChatMessage internalChatMessage = InternalChatMessages.LastOrDefault();
				return (internalChatMessage != null) ? (internalChatMessage.SequenceNumber + 1) : 1;
			}
		}

		public event EventHandler<AbstractChatThreadTrustStatusChangedEventArgs> OnTrustStatusChanged = delegate
		{
		};

		public event EventHandler<AbstractUnreadMessageCountChangedEventArgs> OnUnreadMessageCountChanged = delegate
		{
		};

		public event EventHandler<AbstractChatThreadNicknameChangedEventArgs> OnNicknameChanged = delegate
		{
		};

		public event EventHandler<AbstractChatThreadMemberAddedEventArgs> OnMemberAdded = delegate
		{
		};

		public event EventHandler<AbstractChatThreadMemberRemovedEventArgs> OnMemberRemoved = delegate
		{
		};

		public event EventHandler<AbstractChatThreadMemberAddedMessageAddedEventArgs> OnMemberAddedMessageAdded = delegate
		{
		};

		public event EventHandler<AbstractChatThreadMemberRemovedMessageAddedEventArgs> OnMemberRemovedMessageAdded = delegate
		{
		};

		public event EventHandler<AbstractChatThreadStickerMessageAddedEventArgs> OnStickerMessageAdded = delegate
		{
		};

		public event EventHandler<AbstractChatThreadTextMessageAddedEventArgs> OnTextMessageAdded = delegate
		{
		};

		public event EventHandler<AbstractChatThreadGameStateMessageAddedEventArgs> OnGameStateMessageAdded = delegate
		{
		};

		public event EventHandler<AbstractChatThreadGameEventMessageAddedEventArgs> OnGameEventMessageAdded = delegate
		{
		};

		public event EventHandler<AbstractChatThreadGagMessageAddedEventArgs> OnGagMessageAdded = delegate
		{
		};

		public event EventHandler<AbstractChatThreadPhotoMessageAddedEventArgs> OnPhotoMessageAdded = delegate
		{
		};

		public event EventHandler<AbstractChatThreadVideoMessageAddedEventArgs> OnVideoMessageAdded = delegate
		{
		};

		protected AbstractChatThread(AbstractLogger logger, IChatMessageRetrieverFactory chatMessageRetrieverFactory, long chatThreadId, string hashedChatThreadId, IChatMessageSender messageSender, IList<IInternalRemoteChatMember> remoteMembers, IChatThreadTrustLevel trustLevel, string localUserId, long latestSequenceNumber, bool areSequenceNumbersIndexed, IChatMessageFactory chatMessageFactory, IUserDatabase userDatabase, IChatMessageParser chatMessageParser, IChatMessageHandler chatMessageHandler, IMixWebCallFactory mixWebCallFactory, INotificationQueue notificationQueue)
		{
			this.logger = logger;
			this.chatMessageRetrieverFactory = chatMessageRetrieverFactory;
			this.chatThreadId = chatThreadId;
			this.hashedChatThreadId = hashedChatThreadId;
			this.messageSender = messageSender;
			this.remoteMembers = remoteMembers;
			this.localUserId = localUserId;
			this.chatMessageFactory = chatMessageFactory;
			this.userDatabase = userDatabase;
			this.chatMessageParser = chatMessageParser;
			this.chatMessageHandler = chatMessageHandler;
			this.mixWebCallFactory = mixWebCallFactory;
			this.notificationQueue = notificationQueue;
			LatestSequenceNumber = latestSequenceNumber;
			AreSequenceNumbersIndexed = areSequenceNumbersIndexed;
			TrustLevel = trustLevel;
			messages = new List<IInternalChatMessage>();
			formerRemoteMembers = new List<IInternalRemoteChatMember>();
		}

		public IChatMessageRetriever CreateChatMessageRetriever(int maximumMessageCount)
		{
			return chatMessageRetrieverFactory.CreateChatMessageRetriever(mixWebCallFactory, this, maximumMessageCount, userDatabase, chatMessageParser);
		}

		public IChatThreadNickname SetNickname(string nickname, Action<ISetChatThreadNicknameResult> callback)
		{
			ChatThreadNickname chatThreadNickname = new ChatThreadNickname(nickname);
			ChatThreadNicknameSetter.SetNickname(logger, notificationQueue, mixWebCallFactory, nickname, chatThreadId, delegate
			{
				chatThreadNickname.ApplyFinished();
				Nickname = chatThreadNickname;
				callback(new SetChatThreadNicknameResult(true));
				this.OnNicknameChanged(this, new ChatThreadNicknameChangedEventArgs());
			}, delegate
			{
				callback(new SetChatThreadNicknameResult(false));
			});
			return chatThreadNickname;
		}

		public void RemoveNickname(Action<IRemoveChatThreadNicknameResult> callback)
		{
			ChatThreadNicknameRemover.RemoveNickname(logger, notificationQueue, mixWebCallFactory, chatThreadId, delegate
			{
				Nickname = null;
				callback(new RemoveChatThreadNicknameResult(true));
				this.OnNicknameChanged(this, new ChatThreadNicknameChangedEventArgs());
			}, delegate
			{
				callback(new RemoveChatThreadNicknameResult(false));
			});
		}

		public IStickerMessage SendStickerMessage(string contentId, Action<ISendStickerMessageResult> callback)
		{
			IInternalStickerMessage internalStickerMessage = chatMessageFactory.CreateStickerMessage(false, localSequenceNumber, localUserId, contentId);
			messages.InsertIntoSortedList(internalStickerMessage, SortBySequenceNumber);
			SendStickerMessage(internalStickerMessage, delegate(bool success)
			{
				callback(new SendStickerMessageResult(success));
			});
			return internalStickerMessage;
		}

		private void SendStickerMessage(IInternalStickerMessage stickerMessage, Action<bool> callback)
		{
			MessageSender<IInternalStickerMessage, AddChatThreadStickerMessageNotification>.Send(stickerMessage, logger, chatMessageHandler, userDatabase, chatThreadId, delegate(Action<AddChatThreadStickerMessageNotification> successCallback, Action<string> failureCallback)
			{
				messageSender.SendStickerMessage(mixWebCallFactory, stickerMessage.ContentId, chatThreadId, stickerMessage.LocalChatMessageId, successCallback, failureCallback);
			}, delegate(AddChatThreadStickerMessageNotification notification)
			{
				StickerChatMessage message = notification.Message;
				stickerMessage.SendComplete(message.ContentId, message.ChatMessageId.Value, message.Created.Value, message.SequenceNumber.Value);
			}, delegate(bool success, AddChatThreadStickerMessageNotification notification)
			{
				callback(success);
			}, delegate
			{
				this.OnStickerMessageAdded(this, new ChatThreadStickerMessageAddedEventArgs(stickerMessage));
			}, delegate(Action<uint> idCallback)
			{
				chatMessageHandler.InsertStickerMessage(stickerMessage, chatThreadId, false, idCallback);
			}, UpdateLatestSequenceNumber);
		}

		public void AddStickerMessage(IInternalStickerMessage message, bool addMessage, bool triggerEvent)
		{
			UpdateLatestSequenceNumber(message);
			if (addMessage)
			{
				messages.InsertIntoSortedList(message, SortBySequenceNumber);
			}
			if (triggerEvent)
			{
				UpdateUnreadMessageCount(message);
				this.OnStickerMessageAdded(this, new ChatThreadStickerMessageAddedEventArgs(message));
			}
		}

		public IGagMessage SendGagMessage(string contentId, IRemoteChatMember targetUser, Action<ISendGagMessageResult> callback)
		{
			IInternalGagMessage internalGagMessage = chatMessageFactory.CreateGagMessage(false, localSequenceNumber, localUserId, contentId, targetUser.Id);
			messages.InsertIntoSortedList(internalGagMessage, SortBySequenceNumber);
			SendGagMessage(internalGagMessage, targetUser.Id, delegate(bool success)
			{
				callback(new SendGagMessageResult(success));
			});
			return internalGagMessage;
		}

		private void SendGagMessage(IInternalGagMessage gagMessage, string targetUserId, Action<bool> callback)
		{
			MessageSender<IInternalGagMessage, AddChatThreadGagMessageNotification>.Send(gagMessage, logger, chatMessageHandler, userDatabase, chatThreadId, delegate(Action<AddChatThreadGagMessageNotification> successCallback, Action<string> failureCallback)
			{
				messageSender.SendGagMessage(mixWebCallFactory, gagMessage.ContentId, targetUserId, chatThreadId, gagMessage.LocalChatMessageId, successCallback, failureCallback);
			}, delegate(AddChatThreadGagMessageNotification notification)
			{
				GagChatMessage message = notification.Message;
				gagMessage.SendComplete(message.ContentId, message.TargetUserId, message.ChatMessageId.Value, message.Created.Value, message.SequenceNumber.Value);
			}, delegate(bool success, AddChatThreadGagMessageNotification notification)
			{
				callback(success);
			}, delegate
			{
				this.OnGagMessageAdded(this, new ChatThreadGagMessageAddedEventArgs(gagMessage));
			}, delegate(Action<uint> idCallback)
			{
				chatMessageHandler.InsertGagMessage(gagMessage, chatThreadId, false, idCallback);
			}, UpdateLatestSequenceNumber);
		}

		public void AddGagMessage(IInternalGagMessage message, bool addMessage, bool triggerEvent)
		{
			UpdateLatestSequenceNumber(message);
			if (addMessage)
			{
				messages.InsertIntoSortedList(message, SortBySequenceNumber);
			}
			if (triggerEvent)
			{
				UpdateUnreadMessageCount(message);
				this.OnGagMessageAdded(this, new ChatThreadGagMessageAddedEventArgs(message));
			}
		}

		public IGameStateMessage SendGameStateMessage(string gameName, Dictionary<string, object> payload, Action<ISendGameStateMessageResult> callback)
		{
			IInternalGameStateMessage internalGameStateMessage = chatMessageFactory.CreateGameStateMessage(false, localSequenceNumber, localUserId, gameName, payload);
			messages.InsertIntoSortedList(internalGameStateMessage, SortBySequenceNumber);
			SendGameStateMessage(internalGameStateMessage, callback);
			return internalGameStateMessage;
		}

		private void SendGameStateMessage(IInternalGameStateMessage gameStateMessage, Action<ISendGameStateMessageResult> callback)
		{
			MessageSender<IInternalGameStateMessage, AddChatThreadGameStateMessageNotification>.Send(gameStateMessage, logger, chatMessageHandler, userDatabase, chatThreadId, delegate(Action<AddChatThreadGameStateMessageNotification> successCallback, Action<string> failureCallback)
			{
				messageSender.SendGameStateMessage(mixWebCallFactory, gameStateMessage.GameName, gameStateMessage.State, chatThreadId, gameStateMessage.LocalChatMessageId, successCallback, delegate(string status)
				{
					failureCallback(status);
					if (status == "GAMESTATE_SIZE_EXCEEDED")
					{
						callback(new SendGameStateMessageTooLargeResult());
					}
					else
					{
						callback(new SendGameStateMessageResult(false));
					}
				});
			}, delegate(AddChatThreadGameStateMessageNotification notification)
			{
				GameStateChatMessage message = notification.Message;
				gameStateMessage.SendComplete(JsonParser.FromJson<Dictionary<string, object>>(message.State), message.ChatMessageId.Value, message.Created.Value, message.SequenceNumber.Value);
			}, delegate(bool success, AddChatThreadGameStateMessageNotification notification)
			{
				if (success)
				{
					callback(new SendGameStateMessageResult(true));
				}
			}, delegate
			{
				this.OnGameStateMessageAdded(this, new ChatThreadGameStateMessageAddedEventArgs(gameStateMessage));
			}, delegate(Action<uint> idCallback)
			{
				chatMessageHandler.InsertGameStateMessage(gameStateMessage, chatThreadId, false, idCallback);
			}, UpdateLatestSequenceNumber);
		}

		public void AddGameStateMessage(IInternalGameStateMessage message, bool addMessage, bool triggerEvent)
		{
			UpdateLatestSequenceNumber(message);
			if (addMessage)
			{
				messages.InsertIntoSortedList(message, SortBySequenceNumber);
			}
			if (triggerEvent)
			{
				UpdateUnreadMessageCount(message);
				this.OnGameStateMessageAdded(this, new ChatThreadGameStateMessageAddedEventArgs(message));
			}
		}

		public void UpdateGameStateMessage(IGameStateMessage gameStateMessage, Dictionary<string, object> payload, Action<IUpdateGameStateMessageResult> callback)
		{
			IInternalGameStateMessage internalGameStateMessage = (IInternalGameStateMessage)gameStateMessage;
			messageSender.UpdateGameStateMessage(mixWebCallFactory, internalGameStateMessage.ChatMessageId, payload, chatThreadId, delegate(UpdateChatThreadGameStateMessageResponse response)
			{
				string result = response.UpdateGameStateNotification.Result;
				Dictionary<string, object> state = JsonParser.FromJson<Dictionary<string, object>>(response.UpdateGameStateNotification.Message.State);
				internalGameStateMessage.UpdateState(result, state);
				List<IInternalGameEventMessage> list = new List<IInternalGameEventMessage>();
				foreach (AddChatThreadGameEventMessageNotification gameEventNotification in response.GameEventNotifications)
				{
					GameEventChatMessage message = gameEventNotification.Message;
					Dictionary<string, object> state2 = JsonParser.FromJson<Dictionary<string, object>>(message.Payload);
					IInternalGameEventMessage internalGameEventMessage = chatMessageFactory.CreateGameEventMessage(true, message.SequenceNumber.Value, localUserId, internalGameStateMessage, message.GameName, state2);
					internalGameEventMessage.SendCompleteWithOffsetTime(message.ChatMessageId.Value, message.Created.Value, message.SequenceNumber.Value);
					messages.InsertIntoSortedList(internalGameEventMessage, SortBySequenceNumber);
					list.Add(internalGameEventMessage);
				}
				callback(new UpdateGameStateMessageResult(true, result));
				foreach (IInternalGameEventMessage item in list)
				{
					this.OnGameEventMessageAdded(this, new ChatThreadGameEventMessageAddedEventArgs(item));
				}
			}, delegate
			{
				callback(new UpdateGameStateMessageResult(false, null));
			});
		}

		public void GetGameStateMessage(IGameEventMessage gameEventMessage, Action<IGetGameStateMessageResult> callback)
		{
			Action<bool> callCallback = delegate(bool success)
			{
				callback(new GetGameStateMessageResult(success));
			};
			if (gameEventMessage.GameStateMessage != null)
			{
				callCallback(true);
				return;
			}
			IInternalGameEventMessage internalGameEventMessage = (IInternalGameEventMessage)gameEventMessage;
			IInternalGameStateMessage internalGameStateMessage = chatMessageParser.RetrieveParsedChatMessage(chatThreadId, internalGameEventMessage.GameStateMessageId) as IInternalGameStateMessage;
			if (internalGameStateMessage != null)
			{
				internalGameEventMessage.UpdateGameStateMessage(internalGameStateMessage);
				callCallback(true);
				return;
			}
			ChatMessageGetter.GetChatMessages(logger, mixWebCallFactory, new List<long?> { internalGameEventMessage.GameStateMessageId }, chatThreadId, delegate(GetRecentChatThreadMessagesResponse response)
			{
				try
				{
					List<IInternalChatMessage> list = new List<IInternalChatMessage>();
					foreach (GameStateChatMessage item in response.GameState)
					{
						chatMessageParser.ParseMessage(item, null, list);
					}
					chatMessageParser.InsertParsedChatMessageDocuments();
					if (!list.Any())
					{
						callCallback(false);
					}
					else
					{
						internalGameEventMessage.UpdateGameStateMessage((IInternalGameStateMessage)list.First());
						callCallback(true);
					}
				}
				catch (Exception ex)
				{
					logger.Critical("Unhandled exception: " + ex);
					callCallback(false);
				}
			}, delegate(string status)
			{
				switch (status)
				{
				case "INSUFFICIENT_READ_PERMISSION":
					callback(new GetGameStateMessageForbiddenResult());
					break;
				case "NOT_FOUND":
					callback(new GetGameStateMessageNotFoundResult());
					break;
				default:
					callCallback(false);
					break;
				}
			});
		}

		public void AddGameEventMessage(IInternalGameEventMessage message, bool triggerEvent)
		{
			UpdateLatestSequenceNumber(message);
			messages.InsertIntoSortedList(message, SortBySequenceNumber);
			if (triggerEvent)
			{
				UpdateUnreadMessageCount(message);
				this.OnGameEventMessageAdded(this, new ChatThreadGameEventMessageAddedEventArgs(message));
			}
		}

		public IPhotoMessage SendPhotoMessage(string photoPath, PhotoEncoding encoding, int width, int height, Action<ISendPhotoMessageResult> callback)
		{
			throw new NotImplementedException();
		}

		public void AddPhotoMessage(IInternalPhotoMessage message, bool addMessage, bool triggerEvent)
		{
			UpdateLatestSequenceNumber(message);
			if (addMessage)
			{
				messages.InsertIntoSortedList(message, SortBySequenceNumber);
			}
			if (triggerEvent)
			{
				UpdateUnreadMessageCount(message);
				this.OnPhotoMessageAdded(this, new ChatThreadPhotoMessageAddedEventArgs(message));
			}
		}

		public ITextMessage SendTextMessage(string text, Action<ISendTextMessageResult> callback)
		{
			IInternalTextMessage internalTextMessage = chatMessageFactory.CreateTextMessage(false, localSequenceNumber, localUserId, text);
			messages.InsertIntoSortedList(internalTextMessage, SortBySequenceNumber);
			SendTextMessage(internalTextMessage, delegate(bool success, bool isModerated)
			{
				callback(new SendTextMessageResult(success, isModerated));
			});
			return internalTextMessage;
		}

		private void SendTextMessage(IInternalTextMessage textMessage, Action<bool, bool> callback)
		{
			MessageSender<IInternalTextMessage, AddChatThreadTextMessageNotification>.Send(textMessage, logger, chatMessageHandler, userDatabase, chatThreadId, delegate(Action<AddChatThreadTextMessageNotification> successCallback, Action<string> failureCallback)
			{
				messageSender.SendTextMessage(mixWebCallFactory, textMessage, chatThreadId, successCallback, failureCallback);
			}, null, delegate(bool success, AddChatThreadTextMessageNotification notification)
			{
				callback(success, notification != null && notification.Message.IsModerated.Value);
			}, null, delegate(Action<uint> idCallback)
			{
				chatMessageHandler.InsertTextMessage(textMessage, chatThreadId, false, idCallback);
			}, UpdateLatestSequenceNumber);
		}

		public void AddTextMessage(IInternalTextMessage message, bool addMessage, bool triggerEvent)
		{
			UpdateLatestSequenceNumber(message);
			if (addMessage)
			{
				messages.InsertIntoSortedList(message, SortBySequenceNumber);
			}
			if (triggerEvent)
			{
				UpdateUnreadMessageCount(message);
				this.OnTextMessageAdded(this, new ChatThreadTextMessageAddedEventArgs(message));
			}
		}

		public IVideoMessage SendVideoMessage(string videoPath, VideoFormat format, int bitrate, int duration, int videoWidth, int videoHeight, string thumbnailPath, PhotoEncoding thumbnailEncoding, int thumbnailWidth, int thumbnailHeight, Action<ISendVideoMessageResult> callback)
		{
			throw new NotImplementedException();
		}

		public void AddVideoMessage(IInternalVideoMessage message, bool addMessage, bool triggerEvent)
		{
			UpdateLatestSequenceNumber(message);
			if (addMessage)
			{
				messages.InsertIntoSortedList(message, SortBySequenceNumber);
			}
			if (triggerEvent)
			{
				UpdateUnreadMessageCount(message);
				this.OnVideoMessageAdded(this, new ChatThreadVideoMessageAddedEventArgs(message));
			}
		}

		private void UpdateUnreadMessageCount(IInternalChatMessage message)
		{
			if (message.SenderId != localUserId)
			{
				UnreadMessageCount++;
			}
		}

		private void UpdateLatestSequenceNumber(IInternalChatMessage message)
		{
			if (message.SequenceNumber > LatestSequenceNumber)
			{
				LatestSequenceNumber = message.SequenceNumber;
			}
		}

		public void ModerateTextMessage(string text, Action<ITextModerationResult> callback)
		{
			messageSender.ModerateTextMessage(mixWebCallFactory, text, TrustLevel.AllMembersTrustEachOther, chatThreadId, delegate(ModerateTextResponse response)
			{
				callback(new TextModerationResult(true, response.Moderated.Value, response.Text));
			}, delegate
			{
				callback(new TextModerationResult(false, false, null));
			});
		}

		public void ResendChatMessage(IChatMessage message, Action<IResendChatMessageResult> callback)
		{
			Action<bool> callCallback = delegate(bool success)
			{
				callback(new ResendChatMessageResult(success, false));
			};
			Action<bool, bool> callback2 = delegate(bool success, bool isModerated)
			{
				callback(new ResendChatMessageResult(success, isModerated));
			};
			if (message.Sent)
			{
				logger.Critical("Tried to re-send a message that has already been sent!");
				callCallback(false);
				return;
			}
			IInternalGagMessage internalGagMessage = message as IInternalGagMessage;
			if (internalGagMessage != null)
			{
				SendGagMessage(internalGagMessage, internalGagMessage.TargetUserId, callCallback);
				return;
			}
			IInternalGameStateMessage internalGameStateMessage = message as IInternalGameStateMessage;
			if (internalGameStateMessage != null)
			{
				SendGameStateMessage(internalGameStateMessage, delegate(ISendGameStateMessageResult result)
				{
					callCallback(result.Success);
				});
				return;
			}
			IInternalPhotoMessage internalPhotoMessage = message as IInternalPhotoMessage;
			if (internalPhotoMessage != null)
			{
				return;
			}
			IInternalStickerMessage internalStickerMessage = message as IInternalStickerMessage;
			if (internalStickerMessage != null)
			{
				SendStickerMessage(internalStickerMessage, callCallback);
				return;
			}
			IInternalTextMessage internalTextMessage = message as IInternalTextMessage;
			if (internalTextMessage != null)
			{
				SendTextMessage(internalTextMessage, callback2);
				return;
			}
			IInternalVideoMessage internalVideoMessage = message as IInternalVideoMessage;
			if (internalVideoMessage == null)
			{
			}
		}

		public void AddRemoteMember(IInternalRemoteChatMember member, bool triggerEvent)
		{
			remoteMembers.Add(member);
			if (triggerEvent)
			{
				this.OnMemberAdded(this, new ChatThreadMemberAddedEventArgs(member.Id));
			}
		}

		public void AddMemberAddedMessage(IInternalChatMemberAddedMessage message, bool triggerEvent)
		{
			messages.InsertIntoSortedList(message, SortBySequenceNumber);
			UpdateLatestSequenceNumber(message);
			if (triggerEvent)
			{
				UnreadMessageCount = 1u;
				this.OnMemberAddedMessageAdded(this, new ChatThreadMemberAddedMessageAddedEventArgs(message));
			}
		}

		public void RemoveRemoteMember(IInternalRemoteChatMember member, bool addToFormerList, bool triggerEvent)
		{
			remoteMembers.Remove(member);
			if (addToFormerList)
			{
				formerRemoteMembers.Add(member);
			}
			if (triggerEvent)
			{
				this.OnMemberRemoved(this, new ChatThreadMemberRemovedEventArgs(member.Id));
			}
		}

		public void DispatchLocalUserRemovedEvent()
		{
			this.OnMemberRemoved(this, new ChatThreadMemberRemovedEventArgs(localUserId));
		}

		public void AddMemberRemovedMessage(IInternalChatMemberRemovedMessage message, bool triggerEvent)
		{
			messages.InsertIntoSortedList(message, SortBySequenceNumber);
			UpdateLatestSequenceNumber(message);
			if (triggerEvent)
			{
				UnreadMessageCount = 1u;
				this.OnMemberRemovedMessageAdded(this, new ChatThreadMemberRemovedMessageAddedEventArgs(message));
			}
		}

		protected void DispatchMemberRemovedEvent(AbstractChatThreadMemberRemovedEventArgs eventArgs)
		{
			this.OnMemberRemoved(this, eventArgs);
		}

		public void AddFormerRemoteMember(IInternalRemoteChatMember member)
		{
			formerRemoteMembers.Add(member);
		}

		public void UpdateChatTrustLevel(bool trustLevel)
		{
			TrustLevel = new ChatThreadTrustLevel(trustLevel);
			this.OnTrustStatusChanged(this, new ChatThreadTrustStatusChangedEventArgs(TrustLevel));
		}

		public void UpdateNickname(string nickname)
		{
			if (nickname == null)
			{
				Nickname = null;
			}
			else
			{
				ChatThreadNickname chatThreadNickname = new ChatThreadNickname(nickname);
				chatThreadNickname.ApplyFinished();
				Nickname = chatThreadNickname;
			}
			this.OnNicknameChanged(this, new ChatThreadNicknameChangedEventArgs());
		}

		private static int SortBySequenceNumber(IInternalChatMessage a, IInternalChatMessage b)
		{
			int num = a.SequenceNumber.CompareTo(b.SequenceNumber);
			return (num != 0) ? num : a.Created.CompareTo(b.Created);
		}

		public void ClearUnreadMessageCount(Action<IClearUnreadMessageCountResult> callback)
		{
			UnreadMessageCount = 0u;
			UnreadMessageCountClearer.ClearUnreadMessageCount(logger, notificationQueue, mixWebCallFactory, chatThreadId, delegate
			{
				callback(new ClearUnreadMessageCountResult(true));
				this.OnUnreadMessageCountChanged(this, new UnreadMessageCountChangedEventArgs());
			}, delegate
			{
				callback(new ClearUnreadMessageCountResult(false));
			});
		}

		public void UpdateUnreadMessageCount(uint unreadMessageCount)
		{
			UnreadMessageCount = unreadMessageCount;
			this.OnUnreadMessageCountChanged(this, new UnreadMessageCountChangedEventArgs());
		}
	}
}
