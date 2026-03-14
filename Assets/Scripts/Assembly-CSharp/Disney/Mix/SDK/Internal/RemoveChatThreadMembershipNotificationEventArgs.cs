using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public class RemoveChatThreadMembershipNotificationEventArgs : AbstractRemoveChatThreadMembershipNotificationEventArgs
	{
		public override RemoveChatThreadMembershipNotification Notification { get; protected set; }

		public RemoveChatThreadMembershipNotificationEventArgs(RemoveChatThreadMembershipNotification notification)
		{
			Notification = notification;
		}
	}
}
