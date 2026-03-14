using System;

namespace Disney.Mix.SDK
{
	public abstract class AbstractChatThreadTrustStatusChangedEventArgs : EventArgs
	{
		public abstract IChatThreadTrustLevel TrustLevel { get; protected set; }
	}
}
