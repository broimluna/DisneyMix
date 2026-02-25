using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public class ChatMessageParser : IChatMessageParser
	{
		private readonly AbstractLogger logger;

		private readonly string localUserSwid;

		private readonly IChatMessageFactory chatMessageFactory;

		private readonly IUserDatabase userDatabase;

		private readonly IEpochTime epochTime;

		private readonly IDictionary<long, IList<ChatMessageDocument>> parsedChatMessageDocuments;

		private readonly IPhotoFlavorFactory photoFlavorFactory;

		private readonly IVideoFlavorFactory videoFlavorFactory;

		private readonly IMixWebCallFactory mixWebCallFactory;

		public ChatMessageParser(AbstractLogger logger, string localUserSwid, IChatMessageFactory chatMessageFactory, IUserDatabase userDatabase, IEpochTime epochTime, IPhotoFlavorFactory photoFlavorFactory, IVideoFlavorFactory videoFlavorFactory, IMixWebCallFactory mixWebCallFactory)
		{
			this.logger = logger;
			this.localUserSwid = localUserSwid;
			this.chatMessageFactory = chatMessageFactory;
			this.userDatabase = userDatabase;
			this.epochTime = epochTime;
			parsedChatMessageDocuments = new Dictionary<long, IList<ChatMessageDocument>>();
			this.photoFlavorFactory = photoFlavorFactory;
			this.videoFlavorFactory = videoFlavorFactory;
			this.mixWebCallFactory = mixWebCallFactory;
		}

		public void ParseMessage(BaseChatMessage message, IInternalChatThread chatThread, IList<IInternalChatMessage> messageList)
		{
			ParseMessage(message, chatThread, messageList, null);
		}

		public void ParseMessage(BaseChatMessage message, IInternalChatThread chatThread, IList<IInternalChatMessage> messageList, IList<IInternalChatMessage> existingMessages)
		{
			IInternalChatMessage internalChatMessage = null;
			if (existingMessages != null)
			{
				internalChatMessage = existingMessages.FirstOrDefault((IInternalChatMessage c) => c.ChatMessageId == message.ChatMessageId);
			}
			if (internalChatMessage != null)
			{
				return;
			}
			if (chatThread != null)
			{
				internalChatMessage = chatThread.InternalChatMessages.FirstOrDefault((IInternalChatMessage c) => c.ChatMessageId == message.ChatMessageId);
			}
			if (internalChatMessage != null)
			{
				if (existingMessages != null)
				{
					existingMessages.Add(internalChatMessage);
					messageList.Add(internalChatMessage);
				}
				return;
			}
			if (chatThread == null || DoesChatMemberExist(message.SenderUserId, chatThread))
			{
				GagChatMessage gagChatMessage = message as GagChatMessage;
				if (gagChatMessage != null)
				{
					string payload = ChatMessageDocumentPayloadFactory.Create(gagChatMessage);
					CreateChatMessageDocument(gagChatMessage, payload);
					if (DoesChatMemberExist(gagChatMessage.TargetUserId, chatThread))
					{
						IInternalGagMessage internalGagMessage = chatMessageFactory.CreateGagMessage(true, gagChatMessage.SequenceNumber.Value, gagChatMessage.SenderUserId, gagChatMessage.ContentId, gagChatMessage.TargetUserId);
						internalGagMessage.SendCompleteWithOffsetTime(message.ChatMessageId.Value, message.Created.Value, message.SequenceNumber.Value);
						internalGagMessage.LocalChatMessageId = message.ClientChatMessageId.Value;
						chatThread.AddGagMessage(internalGagMessage, true, false);
						messageList.Add(internalGagMessage);
					}
					return;
				}
				GameStateChatMessage gameStateChatMessage = message as GameStateChatMessage;
				if (gameStateChatMessage != null)
				{
					string payload2 = ChatMessageDocumentPayloadFactory.Create(gameStateChatMessage);
					CreateChatMessageDocument(gameStateChatMessage, payload2);
					IInternalGameStateMessage gameStateMessage = chatMessageFactory.CreateGameStateMessage(true, gameStateChatMessage.SequenceNumber.Value, gameStateChatMessage.SenderUserId, gameStateChatMessage.GameName, JsonParser.FromJson<Dictionary<string, object>>(gameStateChatMessage.State));
					gameStateMessage.SendCompleteWithOffsetTime(message.ChatMessageId.Value, message.Created.Value, message.SequenceNumber.Value);
					gameStateMessage.LocalChatMessageId = message.ClientChatMessageId.Value;
					if (chatThread != null)
					{
						IEnumerable<IInternalGameEventMessage> enumerable = from m in chatThread.InternalChatMessages
							where m is IInternalGameEventMessage && ((IInternalGameEventMessage)m).GameStateMessageId == gameStateMessage.ChatMessageId
							select (IInternalGameEventMessage)m;
						foreach (IInternalGameEventMessage item in enumerable)
						{
							item.UpdateGameStateMessage(gameStateMessage);
						}
						chatThread.AddGameStateMessage(gameStateMessage, true, false);
					}
					messageList.Add(gameStateMessage);
					return;
				}
				GameEventChatMessage gameEventChatMessage = message as GameEventChatMessage;
				if (gameEventChatMessage != null)
				{
					string payload3 = ChatMessageDocumentPayloadFactory.Create(gameEventChatMessage);
					string gameName = gameEventChatMessage.GameName;
					CreateChatMessageDocument(gameEventChatMessage, payload3);
					IInternalGameStateMessage internalGameStateMessage = chatThread.InternalChatMessages.FirstOrDefault((IInternalChatMessage m) => m.ChatMessageId == gameEventChatMessage.GameStateMessageId.Value) as IInternalGameStateMessage;
					IInternalGameEventMessage internalGameEventMessage2;
					if (internalGameStateMessage != null)
					{
						IInternalGameEventMessage internalGameEventMessage = chatMessageFactory.CreateGameEventMessage(true, gameEventChatMessage.SequenceNumber.Value, gameEventChatMessage.SenderUserId, internalGameStateMessage, gameName, JsonParser.FromJson<Dictionary<string, object>>(gameEventChatMessage.Payload));
						internalGameEventMessage2 = internalGameEventMessage;
					}
					else
					{
						internalGameEventMessage2 = chatMessageFactory.CreateGameEventMessage(true, gameEventChatMessage.SequenceNumber.Value, gameEventChatMessage.SenderUserId, gameEventChatMessage.GameStateMessageId.Value, gameName, JsonParser.FromJson<Dictionary<string, object>>(gameEventChatMessage.Payload));
					}
					IInternalGameEventMessage internalGameEventMessage3 = internalGameEventMessage2;
					internalGameEventMessage3.SendCompleteWithOffsetTime(message.ChatMessageId.Value, message.Created.Value, message.SequenceNumber.Value);
					internalGameEventMessage3.LocalChatMessageId = message.ClientChatMessageId.Value;
					chatThread.AddGameEventMessage(internalGameEventMessage3, false);
					messageList.Add(internalGameEventMessage3);
					return;
				}
				PhotoChatMessage photoChatMessage = message as PhotoChatMessage;
				if (photoChatMessage != null)
				{
					string payload4 = ChatMessageDocumentPayloadFactory.Create(photoChatMessage);
					CreateChatMessageDocument(photoChatMessage, payload4);
					List<IPhotoFlavor> photoFlavors = photoChatMessage.PhotoFlavors.Select((Disney.Mix.SDK.Internal.MixDomain.PhotoFlavor f) => CreatePhotoFlavor(photoChatMessage.PhotoId, f, photoFlavorFactory)).ToList();
					IInternalPhotoMessage internalPhotoMessage = chatMessageFactory.CreatePhotoMessage(true, photoChatMessage.SequenceNumber.Value, photoChatMessage.SenderUserId, photoChatMessage.PhotoId, photoChatMessage.Caption, photoFlavors);
					internalPhotoMessage.SendCompleteWithOffsetTime(message.ChatMessageId.Value, message.Created.Value, message.SequenceNumber.Value);
					internalPhotoMessage.LocalChatMessageId = message.ClientChatMessageId.Value;
					chatThread.AddPhotoMessage(internalPhotoMessage, true, false);
					messageList.Add(internalPhotoMessage);
					return;
				}
				StickerChatMessage stickerChatMessage = message as StickerChatMessage;
				if (stickerChatMessage != null)
				{
					string payload5 = ChatMessageDocumentPayloadFactory.Create(stickerChatMessage);
					CreateChatMessageDocument(stickerChatMessage, payload5);
					IInternalStickerMessage internalStickerMessage = chatMessageFactory.CreateStickerMessage(true, stickerChatMessage.SequenceNumber.Value, stickerChatMessage.SenderUserId, stickerChatMessage.ContentId);
					internalStickerMessage.SendCompleteWithOffsetTime(message.ChatMessageId.Value, message.Created.Value, message.SequenceNumber.Value);
					internalStickerMessage.LocalChatMessageId = message.ClientChatMessageId.Value;
					chatThread.AddStickerMessage(internalStickerMessage, true, false);
					messageList.Add(internalStickerMessage);
					return;
				}
				TextChatMessage textChatMessage = message as TextChatMessage;
				if (textChatMessage != null)
				{
					string payload6 = ChatMessageDocumentPayloadFactory.Create(textChatMessage);
					CreateChatMessageDocument(textChatMessage, payload6);
					IInternalTextMessage internalTextMessage = chatMessageFactory.CreateTextMessage(true, textChatMessage.SequenceNumber.Value, textChatMessage.SenderUserId, textChatMessage.Text);
					internalTextMessage.SendCompleteWithOffsetTime(message.ChatMessageId.Value, message.Created.Value, message.SequenceNumber.Value);
					internalTextMessage.LocalChatMessageId = message.ClientChatMessageId.Value;
					chatThread.AddTextMessage(internalTextMessage, true, false);
					messageList.Add(internalTextMessage);
					return;
				}
				VideoChatMessage videoChatMessage = message as VideoChatMessage;
				if (videoChatMessage != null)
				{
					string payload7 = ChatMessageDocumentPayloadFactory.Create(videoChatMessage);
					CreateChatMessageDocument(videoChatMessage, payload7);
					IPhotoFlavor thumbnail = CreatePhotoFlavor(videoChatMessage.VideoId, videoChatMessage.Thumbnail, photoFlavorFactory);
					List<IVideoFlavor> videoFlavors = videoChatMessage.VideoFlavors.Select((Disney.Mix.SDK.Internal.MixDomain.VideoFlavor f) => videoFlavorFactory.Create(videoChatMessage.VideoId, f.VideoFlavorId, f.Bitrate.Value, VideoFormatTypeConverter.ConvertServerTypeToDatabaseType(f.Format), f.Width.Value, f.Height.Value, mixWebCallFactory)).ToList();
					IInternalVideoMessage internalVideoMessage = chatMessageFactory.CreateVideoMessage(true, videoChatMessage.SequenceNumber.Value, videoChatMessage.SenderUserId, videoChatMessage.VideoId, videoChatMessage.Caption, videoChatMessage.Duration.Value, thumbnail, videoFlavors);
					internalVideoMessage.SendCompleteWithOffsetTime(message.ChatMessageId.Value, message.Created.Value, message.SequenceNumber.Value);
					internalVideoMessage.LocalChatMessageId = message.ClientChatMessageId.Value;
					chatThread.AddVideoMessage(internalVideoMessage, true, false);
					messageList.Add(internalVideoMessage);
					return;
				}
			}
			MemberListChangedChatMessage memberListChangedChatMessage = message as MemberListChangedChatMessage;
			if (memberListChangedChatMessage == null)
			{
				return;
			}
			string payload8 = ChatMessageDocumentPayloadFactory.Create(memberListChangedChatMessage);
			CreateChatMessageDocument(memberListChangedChatMessage, payload8);
			bool value = memberListChangedChatMessage.IsAdded.Value;
			List<string> list = new List<string>();
			foreach (string memberUserId in memberListChangedChatMessage.MemberUserIds)
			{
				if (DoesChatMemberExist(memberUserId, chatThread))
				{
					list.Add(memberUserId);
				}
			}
			if (list.Count > 0)
			{
				if (value)
				{
					IInternalChatMemberAddedMessage internalChatMemberAddedMessage = chatMessageFactory.CreateChatMemberAddedMessage(true, message.SequenceNumber.Value, list);
					internalChatMemberAddedMessage.SendCompleteWithOffsetTime(message.ChatMessageId.Value, message.Created.Value, message.SequenceNumber.Value);
					internalChatMemberAddedMessage.LocalChatMessageId = message.ClientChatMessageId.Value;
					chatThread.AddMemberAddedMessage(internalChatMemberAddedMessage, false);
					messageList.Add(internalChatMemberAddedMessage);
				}
				else
				{
					IInternalChatMemberRemovedMessage internalChatMemberRemovedMessage = chatMessageFactory.CreateChatMemberRemovedMessage(true, message.SequenceNumber.Value, list.First());
					internalChatMemberRemovedMessage.SendCompleteWithOffsetTime(message.ChatMessageId.Value, message.Created.Value, message.SequenceNumber.Value);
					internalChatMemberRemovedMessage.LocalChatMessageId = message.ClientChatMessageId.Value;
					chatThread.AddMemberRemovedMessage(internalChatMemberRemovedMessage, false);
					messageList.Add(internalChatMemberRemovedMessage);
				}
			}
		}

		private void CreateChatMessageDocument(BaseChatMessage message, string payload)
		{
			ChatMessageDocument chatMessageDocument = new ChatMessageDocument();
			chatMessageDocument.ChatMessageId = message.ChatMessageId.Value;
			chatMessageDocument.SenderId = message.SenderUserId;
			chatMessageDocument.Created = message.Created.Value;
			chatMessageDocument.ChatMessageType = message.MessageType;
			chatMessageDocument.Payload = payload;
			chatMessageDocument.IsSent = true;
			chatMessageDocument.SequenceNumber = message.SequenceNumber.Value;
			ChatMessageDocument item = chatMessageDocument;
			long value = message.ChatThreadId.Value;
			if (!parsedChatMessageDocuments.ContainsKey(value))
			{
				parsedChatMessageDocuments[value] = new List<ChatMessageDocument>();
			}
			parsedChatMessageDocuments[value].Add(item);
		}

		public void InsertParsedChatMessageDocuments()
		{
			ChatMessageDocument[] updateDocuments = new ChatMessageDocument[0];
			foreach (KeyValuePair<long, IList<ChatMessageDocument>> parsedChatMessageDocument in parsedChatMessageDocuments)
			{
				userDatabase.InsertAndUpdateChatMessages(parsedChatMessageDocument.Key, parsedChatMessageDocument.Value, updateDocuments);
			}
			parsedChatMessageDocuments.Clear();
		}

		public IEnumerable<IInternalChatMessage> RetrieveParsedChatMessages(IInternalChatThread chatThread, int maximumMessageCount, long? timestampOffset, IList<IInternalChatMessage> excludedMessages)
		{
			List<IInternalChatMessage> list = new List<IInternalChatMessage>();
			IEnumerable<ChatMessageDocument> enumerable = RetrieveChatMessages(chatThread, maximumMessageCount, timestampOffset, excludedMessages);
			foreach (ChatMessageDocument item in enumerable)
			{
				ParseMessageDocument(item, chatThread, list);
			}
			return list;
		}

		public IEnumerable<IInternalChatMessage> RetrieveParsedChatMessages(IInternalChatThread chatThread, int maxMessageCount)
		{
			List<IInternalChatMessage> list = new List<IInternalChatMessage>();
			IEnumerable<ChatMessageDocument> enumerable = RetrieveChatMessages(chatThread, maxMessageCount);
			foreach (ChatMessageDocument item in enumerable)
			{
				ParseMessageDocument(item, chatThread, list);
			}
			return list;
		}

		public IEnumerable<IInternalChatMessage> RetrieveParsedChatMessages(IInternalChatThread chatThread, long maxSequenceNumber, int maxMessageCount)
		{
			List<IInternalChatMessage> list = new List<IInternalChatMessage>();
			IEnumerable<ChatMessageDocument> enumerable = RetrieveChatMessages(chatThread, maxSequenceNumber, maxMessageCount);
			foreach (ChatMessageDocument item in enumerable)
			{
				ParseMessageDocument(item, chatThread, list);
			}
			return list;
		}

		public IInternalChatMessage RetrieveParsedChatMessage(long chatThreadId, long chatMessageId)
		{
			ChatMessageDocument chatMessage = userDatabase.GetChatMessage(chatThreadId, chatMessageId);
			if (chatMessage == null)
			{
				return null;
			}
			List<IInternalChatMessage> list = new List<IInternalChatMessage>();
			ParseMessageDocument(chatMessage, null, list);
			return list.FirstOrDefault();
		}

		private void ParseMessageDocument(ChatMessageDocument document, IInternalChatThread chatThread, IList<IInternalChatMessage> messageList)
		{
			if (chatThread != null)
			{
				IInternalChatMessage internalChatMessage = chatThread.InternalChatMessages.FirstOrDefault(delegate(IInternalChatMessage m)
				{
					int result;
					if (m.ChatMessageId == document.ChatMessageId)
					{
						if (m.ChatMessageDocumentId.HasValue)
						{
							uint? chatMessageDocumentId = m.ChatMessageDocumentId;
							result = ((chatMessageDocumentId.GetValueOrDefault() == document.Id && chatMessageDocumentId.HasValue) ? 1 : 0);
						}
						else
						{
							result = 1;
						}
					}
					else
					{
						result = 0;
					}
					return (byte)result != 0;
				});
				if (internalChatMessage != null)
				{
					messageList.Add(internalChatMessage);
					return;
				}
			}
			switch (document.ChatMessageType)
			{
			case "GAG":
			{
				GagMessageDocumentPayload gagMessageDocumentPayload = JsonParser.FromJson<GagMessageDocumentPayload>(document.Payload);
				IInternalGagMessage internalGagMessage = chatMessageFactory.CreateGagMessage(false, document.SequenceNumber, document.SenderId, gagMessageDocumentPayload.ContentId, gagMessageDocumentPayload.TargetUserId);
				internalGagMessage.LocalChatMessageId = document.LocalChatMessageId;
				if (document.IsSent)
				{
					internalGagMessage.SendComplete(document.ChatMessageId, document.Created, document.SequenceNumber);
				}
				else
				{
					internalGagMessage.TimeSent = epochTime.FromMilliseconds(document.Created);
					internalGagMessage.ChatMessageDocumentId = document.Id;
				}
				if (chatThread != null)
				{
					chatThread.AddGagMessage(internalGagMessage, true, false);
				}
				messageList.Add(internalGagMessage);
				break;
			}
			case "GAME_EVENT":
			{
				GameEventMessageDocumentPayload gameEventMessageDocumentPayload = JsonParser.FromJson<GameEventMessageDocumentPayload>(document.Payload);
				long gameStateMessageId = gameEventMessageDocumentPayload.GameStateMessageId.Value;
				IInternalGameStateMessage internalGameStateMessage = chatThread.InternalChatMessages.FirstOrDefault((IInternalChatMessage m) => m.ChatMessageId == gameStateMessageId) as IInternalGameStateMessage;
				string gameName = gameEventMessageDocumentPayload.GameName;
				IInternalGameEventMessage internalGameEventMessage2;
				if (internalGameStateMessage != null)
				{
					IInternalGameEventMessage internalGameEventMessage = chatMessageFactory.CreateGameEventMessage(false, document.SequenceNumber, document.SenderId, internalGameStateMessage, gameName, JsonParser.FromJson<Dictionary<string, object>>(gameEventMessageDocumentPayload.Payload));
					internalGameEventMessage2 = internalGameEventMessage;
				}
				else
				{
					internalGameEventMessage2 = chatMessageFactory.CreateGameEventMessage(false, document.SequenceNumber, document.SenderId, gameStateMessageId, gameName, JsonParser.FromJson<Dictionary<string, object>>(gameEventMessageDocumentPayload.Payload));
				}
				IInternalGameEventMessage internalGameEventMessage3 = internalGameEventMessage2;
				if (document.IsSent)
				{
					internalGameEventMessage3.SendComplete(document.ChatMessageId, document.Created, document.SequenceNumber);
				}
				else
				{
					internalGameEventMessage3.TimeSent = epochTime.FromMilliseconds(document.Created);
					internalGameEventMessage3.ChatMessageDocumentId = document.Id;
				}
				chatThread.AddGameEventMessage(internalGameEventMessage3, false);
				messageList.Add(internalGameEventMessage3);
				break;
			}
			case "GAME_STATE":
			{
				GameStateMessageDocumentPayload gameStateMessageDocumentPayload = JsonParser.FromJson<GameStateMessageDocumentPayload>(document.Payload);
				IInternalGameStateMessage gameStateMessage = chatMessageFactory.CreateGameStateMessage(false, document.SequenceNumber, document.SenderId, gameStateMessageDocumentPayload.GameName, JsonParser.FromJson<Dictionary<string, object>>(gameStateMessageDocumentPayload.State));
				gameStateMessage.LocalChatMessageId = document.LocalChatMessageId;
				if (document.IsSent)
				{
					gameStateMessage.SendComplete(document.ChatMessageId, document.Created, document.SequenceNumber);
				}
				else
				{
					gameStateMessage.TimeSent = epochTime.FromMilliseconds(document.Created);
					gameStateMessage.ChatMessageDocumentId = document.Id;
				}
				if (chatThread != null)
				{
					IEnumerable<IInternalGameEventMessage> enumerable = from m in chatThread.InternalChatMessages
						where m is IInternalGameEventMessage && ((IInternalGameEventMessage)m).GameStateMessageId == gameStateMessage.ChatMessageId
						select (IInternalGameEventMessage)m;
					foreach (IInternalGameEventMessage item in enumerable)
					{
						item.UpdateGameStateMessage(gameStateMessage);
					}
					chatThread.AddGameStateMessage(gameStateMessage, true, false);
				}
				messageList.Add(gameStateMessage);
				break;
			}
			case "MEMBER_LIST_CHANGED":
			{
				MemberListChangedMessageDocumentPayload memberListChangedMessageDocumentPayload = JsonParser.FromJson<MemberListChangedMessageDocumentPayload>(document.Payload);
				bool value = memberListChangedMessageDocumentPayload.IsAdded.Value;
				List<string> list = new List<string>();
				foreach (string memberUserId in memberListChangedMessageDocumentPayload.MemberUserIds)
				{
					if (DoesChatMemberExist(memberUserId, chatThread))
					{
						list.Add(memberUserId);
					}
				}
				if (list.Count <= 0)
				{
					break;
				}
				if (value)
				{
					IInternalChatMemberAddedMessage internalChatMemberAddedMessage = chatMessageFactory.CreateChatMemberAddedMessage(false, document.SequenceNumber, list);
					if (document.IsSent)
					{
						internalChatMemberAddedMessage.SendComplete(document.ChatMessageId, document.Created, document.SequenceNumber);
					}
					else
					{
						internalChatMemberAddedMessage.TimeSent = epochTime.FromMilliseconds(document.Created);
						internalChatMemberAddedMessage.ChatMessageDocumentId = document.Id;
					}
					if (chatThread != null)
					{
						chatThread.AddMemberAddedMessage(internalChatMemberAddedMessage, false);
					}
					messageList.Add(internalChatMemberAddedMessage);
				}
				else
				{
					IInternalChatMemberRemovedMessage internalChatMemberRemovedMessage = chatMessageFactory.CreateChatMemberRemovedMessage(false, document.SequenceNumber, list.First());
					if (document.IsSent)
					{
						internalChatMemberRemovedMessage.SendComplete(document.ChatMessageId, document.Created, document.SequenceNumber);
					}
					else
					{
						internalChatMemberRemovedMessage.TimeSent = epochTime.FromMilliseconds(document.Created);
						internalChatMemberRemovedMessage.ChatMessageDocumentId = document.Id;
					}
					if (chatThread != null)
					{
						chatThread.AddMemberRemovedMessage(internalChatMemberRemovedMessage, false);
					}
					messageList.Add(internalChatMemberRemovedMessage);
				}
				break;
			}
			case "PHOTO":
			{
				PhotoMessageDocumentPayload photoMessageDocumentPayload = JsonParser.FromJson<PhotoMessageDocumentPayload>(document.Payload);
				List<IPhotoFlavor> photoFlavors = photoMessageDocumentPayload.PhotoFlavors.Select((PhotoFlavorDocumentPayload f) => CreatePhotoFlavor(photoMessageDocumentPayload.PhotoId, f, photoFlavorFactory)).ToList();
				IInternalPhotoMessage internalPhotoMessage = chatMessageFactory.CreatePhotoMessage(false, document.SequenceNumber, document.SenderId, photoMessageDocumentPayload.PhotoId, photoMessageDocumentPayload.Caption, photoFlavors);
				internalPhotoMessage.LocalChatMessageId = document.LocalChatMessageId;
				if (document.IsSent)
				{
					internalPhotoMessage.SendComplete(document.ChatMessageId, document.Created, document.SequenceNumber);
				}
				else
				{
					internalPhotoMessage.TimeSent = epochTime.FromMilliseconds(document.Created);
					internalPhotoMessage.ChatMessageDocumentId = document.Id;
				}
				if (chatThread != null)
				{
					chatThread.AddPhotoMessage(internalPhotoMessage, true, false);
				}
				messageList.Add(internalPhotoMessage);
				break;
			}
			case "STICKER":
			{
				StickerMessageDocumentPayload stickerMessageDocumentPayload = JsonParser.FromJson<StickerMessageDocumentPayload>(document.Payload);
				IInternalStickerMessage internalStickerMessage = chatMessageFactory.CreateStickerMessage(false, document.SequenceNumber, document.SenderId, stickerMessageDocumentPayload.ContentId);
				internalStickerMessage.LocalChatMessageId = document.LocalChatMessageId;
				if (document.IsSent)
				{
					internalStickerMessage.SendComplete(document.ChatMessageId, document.Created, document.SequenceNumber);
				}
				else
				{
					internalStickerMessage.TimeSent = epochTime.FromMilliseconds(document.Created);
					internalStickerMessage.ChatMessageDocumentId = document.Id;
				}
				if (chatThread != null)
				{
					chatThread.AddStickerMessage(internalStickerMessage, true, false);
				}
				messageList.Add(internalStickerMessage);
				break;
			}
			case "TEXT":
			{
				TextMessageDocumentPayload textMessageDocumentPayload = JsonParser.FromJson<TextMessageDocumentPayload>(document.Payload);
				IInternalTextMessage internalTextMessage = chatMessageFactory.CreateTextMessage(false, document.SequenceNumber, document.SenderId, textMessageDocumentPayload.Text);
				internalTextMessage.LocalChatMessageId = document.LocalChatMessageId;
				if (document.IsSent)
				{
					internalTextMessage.SendComplete(document.ChatMessageId, document.Created, document.SequenceNumber);
				}
				else
				{
					internalTextMessage.TimeSent = epochTime.FromMilliseconds(document.Created);
					internalTextMessage.ChatMessageDocumentId = document.Id;
				}
				if (chatThread != null)
				{
					chatThread.AddTextMessage(internalTextMessage, true, false);
				}
				messageList.Add(internalTextMessage);
				break;
			}
			case "VIDEO":
			{
				VideoMessageDocumentPayload videoMessageDocumentPayload = JsonParser.FromJson<VideoMessageDocumentPayload>(document.Payload);
				IPhotoFlavor thumbnail = CreatePhotoFlavor(videoMessageDocumentPayload.VideoId, videoMessageDocumentPayload.Thumbnail, photoFlavorFactory);
				List<IVideoFlavor> videoFlavors = videoMessageDocumentPayload.VideoFlavors.Select((VideoFlavorDocumentPayload f) => videoFlavorFactory.Create(videoMessageDocumentPayload.VideoId, f.VideoFlavorId, f.BitRate, VideoFormatTypeConverter.ConvertServerTypeToDatabaseType(f.Format), f.Width, f.Height, mixWebCallFactory)).ToList();
				IInternalVideoMessage internalVideoMessage = chatMessageFactory.CreateVideoMessage(false, document.SequenceNumber, document.SenderId, videoMessageDocumentPayload.VideoId, videoMessageDocumentPayload.Caption, videoMessageDocumentPayload.Duration, thumbnail, videoFlavors);
				internalVideoMessage.LocalChatMessageId = document.LocalChatMessageId;
				if (document.IsSent)
				{
					internalVideoMessage.SendComplete(document.ChatMessageId, document.Created, document.SequenceNumber);
				}
				else
				{
					internalVideoMessage.TimeSent = epochTime.FromMilliseconds(document.Created);
					internalVideoMessage.ChatMessageDocumentId = document.Id;
				}
				if (chatThread != null)
				{
					chatThread.AddVideoMessage(internalVideoMessage, true, false);
				}
				messageList.Add(internalVideoMessage);
				break;
			}
			}
		}

		private bool DoesChatMemberExist(string chatMemberId, IInternalChatThread chatThread)
		{
			bool flag = false;
			if (chatThread != null)
			{
				flag = localUserSwid == chatMemberId || chatThread.InternalMembers.Any((IInternalRemoteChatMember m) => m.Swid == chatMemberId) || chatThread.InternalFormerMembers.Any((IInternalRemoteChatMember m) => m.Swid == chatMemberId);
			}
			if (!flag)
			{
				UserDocument userBySwid = userDatabase.GetUserBySwid(chatMemberId);
				if (userBySwid != null)
				{
					long avatarId = userBySwid.AvatarId;
					if (avatarId == 0L)
					{
					}
					IInternalRemoteChatMember member = RemoteUserFactory.CreateRemoteChatMember(userBySwid.Swid, userBySwid.DisplayName, userBySwid.FirstName, userDatabase);
					if (chatThread != null)
					{
						chatThread.AddFormerRemoteMember(member);
						flag = true;
					}
				}
			}
			return flag;
		}

		private IEnumerable<ChatMessageDocument> RetrieveChatMessages(IInternalChatThread chatThread, int maximumMessageCount, long? timestampOffset, IList<IInternalChatMessage> excludedMessages)
		{
			List<ChatMessageDocument> list = new List<ChatMessageDocument>();
			long created = epochTime.Milliseconds;
			if (timestampOffset.HasValue)
			{
				created = timestampOffset.Value;
			}
			ChatMessageDocument[] messagesCreatedBefore = userDatabase.GetMessagesCreatedBefore(chatThread.ChatThreadId, created);
			int num = 0;
			ChatMessageDocument[] array = messagesCreatedBefore;
			ChatMessageDocument document;
			for (int i = 0; i < array.Length; i++)
			{
				document = array[i];
				if (excludedMessages.All(delegate(IInternalChatMessage m)
				{
					int result;
					if (m.ChatMessageId != document.ChatMessageId)
					{
						if (m.ChatMessageDocumentId.HasValue)
						{
							uint? chatMessageDocumentId = m.ChatMessageDocumentId;
							result = ((chatMessageDocumentId.GetValueOrDefault() != document.Id || !chatMessageDocumentId.HasValue) ? 1 : 0);
						}
						else
						{
							result = 1;
						}
					}
					else
					{
						result = 0;
					}
					return (byte)result != 0;
				}))
				{
					num++;
					list.Add(document);
					if (num == maximumMessageCount)
					{
						break;
					}
				}
			}
			return list;
		}

		private IEnumerable<ChatMessageDocument> RetrieveChatMessages(IInternalChatThread chatThread, int maxMessageCount)
		{
			return userDatabase.GetMessagesBeforeMaxSequenceNumberInclusive(chatThread.ChatThreadId, maxMessageCount);
		}

		private IEnumerable<ChatMessageDocument> RetrieveChatMessages(IInternalChatThread chatThread, long maxSequenceNumber, int maxMessageCount)
		{
			return userDatabase.GetMessagesBeforeSequenceNumberInclusive(chatThread.ChatThreadId, maxSequenceNumber, maxMessageCount);
		}

		private static IPhotoFlavor CreatePhotoFlavor(string mediaId, Disney.Mix.SDK.Internal.MixDomain.PhotoFlavor flavor, IPhotoFlavorFactory photoFlavorFactory)
		{
			return photoFlavorFactory.Create(mediaId, flavor.PhotoFlavorId, flavor.Url, PhotoEncodingTypeConverter.ConvertServerTypeToDatabaseType(flavor.Encoding), flavor.Width.Value, flavor.Height.Value, null);
		}

		private static IPhotoFlavor CreatePhotoFlavor(string mediaId, PhotoFlavorDocumentPayload flavor, IPhotoFlavorFactory photoFlavorFactory)
		{
			return photoFlavorFactory.Create(mediaId, flavor.PhotoFlavorId, flavor.Url, PhotoEncodingTypeConverter.ConvertServerTypeToDatabaseType(flavor.Encoding), flavor.Width, flavor.Height, null);
		}
	}
}
