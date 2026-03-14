using System;

namespace Disney.Mix.SDK
{
	public abstract class AbstractChatThreadMemberRemovedMessageAddedEventArgs : EventArgs
	{
		public abstract IChatMemberRemovedMessage Message { get; protected set; }
	}
}
