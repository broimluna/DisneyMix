using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal
{
	internal class GameEventMessage : ChatMessage, IInternalGameEventMessage, IInternalChatMessage, IChatMessage, IGameEventMessage
	{
		public IGameStateMessage GameStateMessage { get; private set; }

		public string GameName { get; private set; }

		public long GameStateMessageId { get; private set; }

		public Dictionary<string, object> EventState { get; private set; }

		public GameEventMessage(bool sent, long sequenceNumber, string senderId, IInternalGameStateMessage gameStateMessage, string gameName, Dictionary<string, object> state, IEpochTime epochTime, long localChatMessageId)
			: base(sent, sequenceNumber, senderId, epochTime, localChatMessageId)
		{
			GameStateMessage = gameStateMessage;
			GameName = gameName;
			GameStateMessageId = gameStateMessage.ChatMessageId;
			EventState = state;
		}

		public GameEventMessage(bool sent, long sequenceNumber, string senderId, long gameStateMessageId, string gameName, Dictionary<string, object> state, IEpochTime epochTime, long localChatMessageId)
			: base(sent, sequenceNumber, senderId, epochTime, localChatMessageId)
		{
			GameStateMessageId = gameStateMessageId;
			GameName = gameName;
			EventState = state;
		}

		public void UpdateGameStateMessage(IInternalGameStateMessage gameStateMessage)
		{
			GameStateMessage = gameStateMessage;
		}
	}
}
