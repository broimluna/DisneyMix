using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public class AddChatThreadNotificationEventArgs : AbstractAddChatThreadNotificationEventArgs
	{
		public override AddChatThreadNotification Notification { get; protected set; }

		public AddChatThreadNotificationEventArgs(AddChatThreadNotification notification)
		{
			Notification = notification;
		}
	}
}
