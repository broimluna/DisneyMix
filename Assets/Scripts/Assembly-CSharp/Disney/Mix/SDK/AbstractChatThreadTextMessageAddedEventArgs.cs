using System;

namespace Disney.Mix.SDK
{
	public abstract class AbstractChatThreadTextMessageAddedEventArgs : EventArgs
	{
		public abstract ITextMessage Message { get; protected set; }
	}
}
