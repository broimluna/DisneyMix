using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public abstract class AbstractRemoveFollowshipNotificationEventArgs : EventArgs
	{
		public abstract RemoveFollowshipNotification Notification { get; protected set; }
	}
}
