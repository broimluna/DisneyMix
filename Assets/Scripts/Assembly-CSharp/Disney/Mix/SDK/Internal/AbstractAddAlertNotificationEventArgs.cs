using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public abstract class AbstractAddAlertNotificationEventArgs : EventArgs
	{
		public abstract AddAlertNotification Notification { get; protected set; }
	}
}
