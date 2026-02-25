using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal
{
	public interface IChatMessageFactory
	{
		IInternalChatMemberAddedMessage CreateChatMemberAddedMessage(bool sent, long sequenceNumber, IEnumerable<string> memberIds);

		IInternalChatMemberRemovedMessage CreateChatMemberRemovedMessage(bool sent, long sequenceNumber, string memberId);

		IInternalGagMessage CreateGagMessage(bool sent, long sequenceNumber, string senderId, string contentId, string targetUserId);

		IInternalPhotoMessage CreatePhotoMessage(bool sent, long sequenceNumber, string senderId, string photoId, string caption, IEnumerable<IPhotoFlavor> photoFlavors);

		IInternalStickerMessage CreateStickerMessage(bool sent, long sequenceNumber, string senderId, string contentId);

		IInternalTextMessage CreateTextMessage(bool sent, long sequenceNumber, string senderId, string text);

		IInternalVideoMessage CreateVideoMessage(bool sent, long sequenceNumber, string senderId, string videoId, string caption, long duration, IPhotoFlavor thumbnail, IEnumerable<IVideoFlavor> videoFlavors);

		IInternalGameStateMessage CreateGameStateMessage(bool sent, long sequenceNumber, string senderId, string gameName, Dictionary<string, object> state);

		IInternalGameEventMessage CreateGameEventMessage(bool sent, long sequenceNumber, string senderId, IInternalGameStateMessage gameStateMessage, string gameName, Dictionary<string, object> state);

		IInternalGameEventMessage CreateGameEventMessage(bool sent, long sequenceNumber, string senderId, long gameStateMessageId, string gameName, Dictionary<string, object> state);
	}
}
