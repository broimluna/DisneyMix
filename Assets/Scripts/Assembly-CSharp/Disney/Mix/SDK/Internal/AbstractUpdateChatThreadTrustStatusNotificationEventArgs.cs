using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public abstract class AbstractUpdateChatThreadTrustStatusNotificationEventArgs : EventArgs
	{
		public abstract UpdateChatThreadTrustStatusNotification Notification { get; protected set; }
	}
}
