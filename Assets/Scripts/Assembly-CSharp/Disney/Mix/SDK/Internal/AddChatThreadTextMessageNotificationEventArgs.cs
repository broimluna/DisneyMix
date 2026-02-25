using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public class AddChatThreadTextMessageNotificationEventArgs : AbstractAddChatThreadTextMessageNotificationEventArgs
	{
		public override AddChatThreadTextMessageNotification Notification { get; protected set; }

		public AddChatThreadTextMessageNotificationEventArgs(AddChatThreadTextMessageNotification notification)
		{
			Notification = notification;
		}
	}
}
