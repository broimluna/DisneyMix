using System;

namespace Disney.Mix.SDK
{
	public abstract class AbstractAddedToOfficialAccountThreadEventArgs : EventArgs
	{
		public IOfficialAccountChatThread ChatThread { get; protected set; }
	}
}
