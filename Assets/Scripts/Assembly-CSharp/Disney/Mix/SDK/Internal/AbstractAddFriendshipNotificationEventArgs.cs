using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public abstract class AbstractAddFriendshipNotificationEventArgs : EventArgs
	{
		public abstract AddFriendshipNotification Notification { get; protected set; }
	}
}
