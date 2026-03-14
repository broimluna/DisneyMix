using System;

namespace Disney.Mix.SDK
{
	public abstract class AbstractChatThreadGameStateMessageAddedEventArgs : EventArgs
	{
		public abstract IGameStateMessage Message { get; protected set; }
	}
}
