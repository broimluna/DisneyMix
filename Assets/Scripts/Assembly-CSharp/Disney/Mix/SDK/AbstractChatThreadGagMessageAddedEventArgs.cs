using System;

namespace Disney.Mix.SDK
{
	public abstract class AbstractChatThreadGagMessageAddedEventArgs : EventArgs
	{
		public abstract IGagMessage Message { get; protected set; }
	}
}
