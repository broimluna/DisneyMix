using System;

namespace Disney.Mix.SDK
{
	public abstract class AbstractChatThreadMemberAddedMessageAddedEventArgs : EventArgs
	{
		public abstract IChatMemberAddedMessage Message { get; protected set; }
	}
}
