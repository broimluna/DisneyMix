using System;
using System.Collections.Generic;

namespace Disney.Mix.SDK
{
	public interface IGameStateMessage : IChatMessage
	{
		string GameName { get; }

		Dictionary<string, object> State { get; }

		event EventHandler<AbstractChatThreadGameStateMessageUpdatedEventArgs> OnStateUpdated;
	}
}
