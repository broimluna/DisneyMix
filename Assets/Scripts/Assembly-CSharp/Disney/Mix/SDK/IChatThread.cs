using System;
using System.Collections.Generic;

namespace Disney.Mix.SDK
{
	public interface IChatThread
	{
		IChatThreadTrustLevel TrustLevel { get; }

		IEnumerable<IChatMessage> ChatMessages { get; }

		IEnumerable<IRemoteChatMember> RemoteMembers { get; }

		IEnumerable<IRemoteChatMember> FormerRemoteMembers { get; }

		uint UnreadMessageCount { get; }

		string Id { get; }

		event EventHandler<AbstractChatThreadTrustStatusChangedEventArgs> OnTrustStatusChanged;

		event EventHandler<AbstractChatThreadMemberAddedEventArgs> OnMemberAdded;

		event EventHandler<AbstractChatThreadMemberRemovedEventArgs> OnMemberRemoved;

		event EventHandler<AbstractChatThreadMemberAddedMessageAddedEventArgs> OnMemberAddedMessageAdded;

		event EventHandler<AbstractChatThreadMemberRemovedMessageAddedEventArgs> OnMemberRemovedMessageAdded;

		event EventHandler<AbstractChatThreadStickerMessageAddedEventArgs> OnStickerMessageAdded;

		event EventHandler<AbstractChatThreadTextMessageAddedEventArgs> OnTextMessageAdded;

		event EventHandler<AbstractChatThreadPhotoMessageAddedEventArgs> OnPhotoMessageAdded;

		event EventHandler<AbstractChatThreadVideoMessageAddedEventArgs> OnVideoMessageAdded;

		event EventHandler<AbstractChatThreadGameStateMessageAddedEventArgs> OnGameStateMessageAdded;

		event EventHandler<AbstractChatThreadGameEventMessageAddedEventArgs> OnGameEventMessageAdded;

		event EventHandler<AbstractUnreadMessageCountChangedEventArgs> OnUnreadMessageCountChanged;

		IChatMessageRetriever CreateChatMessageRetriever(int maximumMessageCount);

		IPhotoMessage SendPhotoMessage(string photoPath, PhotoEncoding encoding, int width, int height, Action<ISendPhotoMessageResult> callback);

		ITextMessage SendTextMessage(string text, Action<ISendTextMessageResult> callback);

		IVideoMessage SendVideoMessage(string videoPath, VideoFormat format, int bitrate, int duration, int videoWidth, int videoHeight, string thumbnailPath, PhotoEncoding thumbnailEncoding, int thumbnailWidth, int thumbnailHeight, Action<ISendVideoMessageResult> callback);

		IStickerMessage SendStickerMessage(string contentId, Action<ISendStickerMessageResult> callback);

		IGameStateMessage SendGameStateMessage(string gameName, Dictionary<string, object> payload, Action<ISendGameStateMessageResult> callback);

		void UpdateGameStateMessage(IGameStateMessage message, Dictionary<string, object> payload, Action<IUpdateGameStateMessageResult> callback);

		void GetGameStateMessage(IGameEventMessage message, Action<IGetGameStateMessageResult> callback);

		void ResendChatMessage(IChatMessage message, Action<IResendChatMessageResult> callback);

		void ModerateTextMessage(string text, Action<ITextModerationResult> callback);

		void ClearUnreadMessageCount(Action<IClearUnreadMessageCountResult> callback);
	}
}
