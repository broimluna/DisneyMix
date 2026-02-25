using System;

namespace Disney.Mix.SDK
{
	public abstract class AbstractChatThreadGameEventMessageAddedEventArgs : EventArgs
	{
		public abstract IGameEventMessage Message { get; protected set; }
	}
}
