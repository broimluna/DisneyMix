using System;
using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal
{
	public class GameStateMessage : ChatMessage, IInternalGameStateMessage, IInternalChatMessage, IChatMessage, IGameStateMessage
	{
		public string GameName { get; private set; }

		public Dictionary<string, object> State { get; private set; }

		public event EventHandler<AbstractChatThreadGameStateMessageUpdatedEventArgs> OnStateUpdated = delegate
		{
		};

		public GameStateMessage(bool sent, long sequenceNumber, string senderId, string gameName, Dictionary<string, object> state, IEpochTime epochTime, long localChatMessageId)
			: base(sent, sequenceNumber, senderId, epochTime, localChatMessageId)
		{
			GameName = gameName;
			State = state;
		}

		public void SendComplete(Dictionary<string, object> state, long id, long timeSent, long sequenceNumber)
		{
			SendCompleteWithOffsetTime(id, timeSent, sequenceNumber);
			State = state;
		}

		public void UpdateState(string result, Dictionary<string, object> state)
		{
			State = state;
			this.OnStateUpdated(this, new ChatThreadGameStateMessageUpdatedEventArgs(result));
		}
	}
}
