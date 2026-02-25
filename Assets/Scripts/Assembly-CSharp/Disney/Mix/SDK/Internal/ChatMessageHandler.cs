using System;

namespace Disney.Mix.SDK.Internal
{
	public class ChatMessageHandler : IChatMessageHandler
	{
		private readonly string localUserSwid;

		private readonly IUserDatabase userDatabase;

		private readonly IEpochTime epochTime;

		public ChatMessageHandler(string localUserSwid, IUserDatabase userDatabase, IEpochTime epochTime)
		{
			this.localUserSwid = localUserSwid;
			this.userDatabase = userDatabase;
			this.epochTime = epochTime;
		}

		public void InsertGagMessage(IInternalGagMessage message, long chatThreadId, bool isSent, Action<uint> callback)
		{
			userDatabase.IndexSequenceNumberField(chatThreadId, delegate
			{
				string payload = ChatMessageDocumentPayloadFactory.Create(message);
				long created = epochTime.RawMillisecondsSince(message.TimeSent);
				ChatMessageDocument chatMessageDocument = new ChatMessageDocument
				{
					ChatMessageId = message.ChatMessageId,
					SenderId = localUserSwid,
					Created = created,
					ChatMessageType = "GAG",
					Payload = payload,
					IsSent = isSent,
					LocalChatMessageId = message.LocalChatMessageId,
					SequenceNumber = message.SequenceNumber
				};
				userDatabase.InsertChatMessage(chatThreadId, chatMessageDocument);
				callback(chatMessageDocument.Id);
			});
		}

		public void InsertGameStateMessage(IInternalGameStateMessage message, long chatThreadId, bool isSent, Action<uint> callback)
		{
			userDatabase.IndexSequenceNumberField(chatThreadId, delegate
			{
				string payload = ChatMessageDocumentPayloadFactory.Create(message);
				long created = epochTime.RawMillisecondsSince(message.TimeSent);
				ChatMessageDocument chatMessageDocument = new ChatMessageDocument
				{
					ChatMessageId = message.ChatMessageId,
					SenderId = localUserSwid,
					Created = created,
					ChatMessageType = "GAME_STATE",
					Payload = payload,
					IsSent = isSent,
					LocalChatMessageId = message.LocalChatMessageId,
					SequenceNumber = message.SequenceNumber
				};
				userDatabase.InsertChatMessage(chatThreadId, chatMessageDocument);
				callback(chatMessageDocument.Id);
			});
		}

		public void InsertPhotoMessage(IInternalPhotoMessage message, long chatThreadId, bool isSent, Action<uint> callback)
		{
			userDatabase.IndexSequenceNumberField(chatThreadId, delegate
			{
				string payload = ChatMessageDocumentPayloadFactory.Create(message);
				long created = epochTime.RawMillisecondsSince(message.TimeSent);
				ChatMessageDocument chatMessageDocument = new ChatMessageDocument
				{
					ChatMessageId = message.ChatMessageId,
					SenderId = localUserSwid,
					Created = created,
					ChatMessageType = "PHOTO",
					Payload = payload,
					IsSent = isSent,
					LocalChatMessageId = message.LocalChatMessageId,
					SequenceNumber = message.SequenceNumber
				};
				userDatabase.InsertChatMessage(chatThreadId, chatMessageDocument);
				callback(chatMessageDocument.Id);
			});
		}

		public void InsertStickerMessage(IInternalStickerMessage message, long chatThreadId, bool isSent, Action<uint> callback)
		{
			userDatabase.IndexSequenceNumberField(chatThreadId, delegate
			{
				string payload = ChatMessageDocumentPayloadFactory.Create(message);
				long created = epochTime.RawMillisecondsSince(message.TimeSent);
				ChatMessageDocument chatMessageDocument = new ChatMessageDocument
				{
					ChatMessageId = message.ChatMessageId,
					SenderId = localUserSwid,
					Created = created,
					ChatMessageType = "STICKER",
					Payload = payload,
					IsSent = isSent,
					LocalChatMessageId = message.LocalChatMessageId,
					SequenceNumber = message.SequenceNumber
				};
				userDatabase.InsertChatMessage(chatThreadId, chatMessageDocument);
				callback(chatMessageDocument.Id);
			});
		}

		public void InsertTextMessage(IInternalTextMessage message, long chatThreadId, bool isSent, Action<uint> callback)
		{
			userDatabase.IndexSequenceNumberField(chatThreadId, delegate
			{
				string payload = ChatMessageDocumentPayloadFactory.Create(message);
				long created = epochTime.RawMillisecondsSince(message.TimeSent);
				ChatMessageDocument chatMessageDocument = new ChatMessageDocument
				{
					ChatMessageId = message.ChatMessageId,
					SenderId = localUserSwid,
					Created = created,
					ChatMessageType = "TEXT",
					Payload = payload,
					IsSent = isSent,
					LocalChatMessageId = message.LocalChatMessageId,
					SequenceNumber = message.SequenceNumber
				};
				userDatabase.InsertChatMessage(chatThreadId, chatMessageDocument);
				callback(chatMessageDocument.Id);
			});
		}

		public void InsertVideoMessage(IInternalVideoMessage message, long chatThreadId, bool isSent, Action<uint> callback)
		{
			userDatabase.IndexSequenceNumberField(chatThreadId, delegate
			{
				string payload = ChatMessageDocumentPayloadFactory.Create(message);
				long created = epochTime.RawMillisecondsSince(message.TimeSent);
				ChatMessageDocument chatMessageDocument = new ChatMessageDocument
				{
					ChatMessageId = message.ChatMessageId,
					SenderId = localUserSwid,
					Created = created,
					ChatMessageType = "VIDEO",
					Payload = payload,
					IsSent = isSent,
					LocalChatMessageId = message.LocalChatMessageId,
					SequenceNumber = message.SequenceNumber
				};
				userDatabase.InsertChatMessage(chatThreadId, chatMessageDocument);
				callback(chatMessageDocument.Id);
			});
		}

		public void DeleteChatMessageDocument(uint documentId, long chatThreadId)
		{
			userDatabase.IndexSequenceNumberField(chatThreadId, delegate
			{
				userDatabase.DeleteChatMessageDocument(chatThreadId, documentId);
			});
		}
	}
}
