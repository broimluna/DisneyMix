using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public abstract class AbstractAddChatThreadMembershipNotificationEventArgs : EventArgs
	{
		public abstract AddChatThreadMembershipNotification Notification { get; protected set; }
	}
}
