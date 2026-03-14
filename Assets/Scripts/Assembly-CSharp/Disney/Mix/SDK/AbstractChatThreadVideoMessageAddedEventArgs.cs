using System;

namespace Disney.Mix.SDK
{
	public abstract class AbstractChatThreadVideoMessageAddedEventArgs : EventArgs
	{
		public abstract IVideoMessage Message { get; protected set; }
	}
}
