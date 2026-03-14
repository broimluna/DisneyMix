using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public abstract class AbstractAddFriendshipInvitationNotificationEventArgs : EventArgs
	{
		public abstract AddFriendshipInvitationNotification Notification { get; protected set; }
	}
}
