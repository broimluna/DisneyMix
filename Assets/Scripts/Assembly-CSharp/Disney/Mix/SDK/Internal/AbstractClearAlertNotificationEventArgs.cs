using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public abstract class AbstractClearAlertNotificationEventArgs : EventArgs
	{
		public abstract ClearAlertNotification Notification { get; protected set; }
	}
}
