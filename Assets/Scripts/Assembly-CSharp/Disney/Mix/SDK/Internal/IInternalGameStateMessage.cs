using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal
{
	public interface IInternalGameStateMessage : IInternalChatMessage, IChatMessage, IGameStateMessage
	{
		void SendComplete(Dictionary<string, object> state, long id, long timeSent, long sequenceNumber);

		void UpdateState(string result, Dictionary<string, object> state);
	}
}
