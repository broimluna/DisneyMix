using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public class AddChatThreadGameStateMessageNotificationEventArgs : AbstractAddChatThreadGameStateMessageNotificationEventArgs
	{
		public override AddChatThreadGameStateMessageNotification Notification { get; protected set; }

		public AddChatThreadGameStateMessageNotificationEventArgs(AddChatThreadGameStateMessageNotification notification)
		{
			Notification = notification;
		}
	}
}
