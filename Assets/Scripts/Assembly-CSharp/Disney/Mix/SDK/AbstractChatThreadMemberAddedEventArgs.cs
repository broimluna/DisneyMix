using System;

namespace Disney.Mix.SDK
{
	public abstract class AbstractChatThreadMemberAddedEventArgs : EventArgs
	{
		public abstract string MemberId { get; protected set; }
	}
}
