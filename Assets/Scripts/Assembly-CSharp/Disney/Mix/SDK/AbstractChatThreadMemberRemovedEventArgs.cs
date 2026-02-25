using System;

namespace Disney.Mix.SDK
{
	public abstract class AbstractChatThreadMemberRemovedEventArgs : EventArgs
	{
		public abstract string MemberId { get; protected set; }
	}
}
