using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public abstract class AbstractAddFollowshipNotificationEventArgs : EventArgs
	{
		public abstract AddFollowshipNotification Notification { get; protected set; }
	}
}
