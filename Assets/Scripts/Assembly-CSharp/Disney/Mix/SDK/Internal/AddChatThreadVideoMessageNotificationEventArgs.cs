using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public class AddChatThreadVideoMessageNotificationEventArgs : AbstractAddChatThreadVideoMessageNotificationEventArgs
	{
		public override AddChatThreadVideoMessageNotification Notification { get; protected set; }

		public AddChatThreadVideoMessageNotificationEventArgs(AddChatThreadVideoMessageNotification notification)
		{
			Notification = notification;
		}
	}
}
