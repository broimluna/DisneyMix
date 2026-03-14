using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal
{
	public class ChatMessageFactory : IChatMessageFactory
	{
		private readonly IEpochTime epochTime;

		private readonly IRandom random;

		public ChatMessageFactory(IEpochTime epochTime, IRandom random)
		{
			this.epochTime = epochTime;
			this.random = random;
		}

		public IInternalChatMemberAddedMessage CreateChatMemberAddedMessage(bool sent, long sequenceNumber, IEnumerable<string> memberIds)
		{
			return new ChatMemberAddedMessage(sent, sequenceNumber, null, memberIds, epochTime, random.NextLong());
		}

		public IInternalChatMemberRemovedMessage CreateChatMemberRemovedMessage(bool sent, long sequenceNumber, string memberId)
		{
			return new ChatMemberRemovedMessage(sent, sequenceNumber, null, memberId, epochTime, random.NextLong());
		}

		public IInternalGagMessage CreateGagMessage(bool sent, long sequenceNumber, string senderId, string contentId, string targetUserId)
		{
			return new GagMessage(sent, sequenceNumber, senderId, contentId, targetUserId, epochTime, random.NextLong());
		}

		public IInternalPhotoMessage CreatePhotoMessage(bool sent, long sequenceNumber, string senderId, string photoId, string caption, IEnumerable<IPhotoFlavor> photoFlavors)
		{
			return new PhotoMessage(sent, sequenceNumber, senderId, epochTime, random.NextLong(), photoId, caption, photoFlavors);
		}

		public IInternalStickerMessage CreateStickerMessage(bool sent, long sequenceNumber, string senderId, string contentId)
		{
			return new StickerMessage(sent, sequenceNumber, senderId, contentId, epochTime, random.NextLong());
		}

		public IInternalTextMessage CreateTextMessage(bool sent, long sequenceNumber, string senderId, string text)
		{
			return new TextMessage(sent, sequenceNumber, senderId, text, epochTime, random.NextLong());
		}

		public IInternalVideoMessage CreateVideoMessage(bool sent, long sequenceNumber, string senderId, string videoId, string caption, long duration, IPhotoFlavor thumbnail, IEnumerable<IVideoFlavor> videoFlavors)
		{
			return new VideoMessage(sent, sequenceNumber, senderId, epochTime, random.NextLong(), videoId, caption, duration, thumbnail, videoFlavors);
		}

		public IInternalGameStateMessage CreateGameStateMessage(bool sent, long sequenceNumber, string senderId, string gameName, Dictionary<string, object> state)
		{
			return new GameStateMessage(sent, sequenceNumber, senderId, gameName, state, epochTime, random.NextLong());
		}

		public IInternalGameEventMessage CreateGameEventMessage(bool sent, long sequenceNumber, string senderId, IInternalGameStateMessage gameStateMessage, string gameName, Dictionary<string, object> state)
		{
			return new GameEventMessage(sent, sequenceNumber, senderId, gameStateMessage, gameName, state, epochTime, random.NextLong());
		}

		public IInternalGameEventMessage CreateGameEventMessage(bool sent, long sequenceNumber, string senderId, long gameStateMessageId, string gameName, Dictionary<string, object> state)
		{
			return new GameEventMessage(sent, sequenceNumber, senderId, gameStateMessageId, gameName, state, epochTime, random.NextLong());
		}
	}
}
