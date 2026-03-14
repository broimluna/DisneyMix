using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public abstract class AbstractRemoveFriendshipInvitationNotificationEventArgs : EventArgs
	{
		public abstract RemoveFriendshipInvitationNotification Notification { get; protected set; }
	}
}
