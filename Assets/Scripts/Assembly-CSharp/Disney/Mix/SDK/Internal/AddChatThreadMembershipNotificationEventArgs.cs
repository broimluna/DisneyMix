using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public class AddChatThreadMembershipNotificationEventArgs : AbstractAddChatThreadMembershipNotificationEventArgs
	{
		public override AddChatThreadMembershipNotification Notification { get; protected set; }

		public AddChatThreadMembershipNotificationEventArgs(AddChatThreadMembershipNotification notification)
		{
			Notification = notification;
		}
	}
}
