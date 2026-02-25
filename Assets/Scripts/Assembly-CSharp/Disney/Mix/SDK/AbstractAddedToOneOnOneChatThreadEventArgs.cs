using System;

namespace Disney.Mix.SDK
{
	public abstract class AbstractAddedToOneOnOneChatThreadEventArgs : EventArgs
	{
		public IOneOnOneChatThread ChatThread { get; protected set; }
	}
}
