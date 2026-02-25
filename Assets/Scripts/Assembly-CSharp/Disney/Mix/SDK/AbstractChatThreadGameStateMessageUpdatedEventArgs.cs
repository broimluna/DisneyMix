using System;

namespace Disney.Mix.SDK
{
	public abstract class AbstractChatThreadGameStateMessageUpdatedEventArgs : EventArgs
	{
		public abstract string Result { get; protected set; }
	}
}
