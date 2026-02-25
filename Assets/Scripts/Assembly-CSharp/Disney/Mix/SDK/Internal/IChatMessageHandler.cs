using System;

namespace Disney.Mix.SDK.Internal
{
	public interface IChatMessageHandler
	{
		void InsertGagMessage(IInternalGagMessage message, long chatThreadId, bool isSent, Action<uint> callback);

		void InsertGameStateMessage(IInternalGameStateMessage message, long chatThreadId, bool isSent, Action<uint> callback);

		void InsertPhotoMessage(IInternalPhotoMessage message, long chatThreadId, bool isSent, Action<uint> callback);

		void InsertStickerMessage(IInternalStickerMessage message, long chatThreadId, bool isSent, Action<uint> callback);

		void InsertTextMessage(IInternalTextMessage message, long chatThreadId, bool isSent, Action<uint> callback);

		void InsertVideoMessage(IInternalVideoMessage message, long chatThreadId, bool isSent, Action<uint> callback);

		void DeleteChatMessageDocument(uint documentId, long chatThreadId);
	}
}
