using System;

namespace Disney.Mix.SDK
{
	public abstract class AbstractAddedToGroupChatThreadEventArgs : EventArgs
	{
		public IGroupChatThread ChatThread { get; protected set; }
	}
}
