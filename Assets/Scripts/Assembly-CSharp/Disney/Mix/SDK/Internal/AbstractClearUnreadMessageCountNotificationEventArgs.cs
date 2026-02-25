using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public abstract class AbstractClearUnreadMessageCountNotificationEventArgs : EventArgs
	{
		public abstract ClearUnreadMessageCountNotification Notification { get; protected set; }
	}
}
