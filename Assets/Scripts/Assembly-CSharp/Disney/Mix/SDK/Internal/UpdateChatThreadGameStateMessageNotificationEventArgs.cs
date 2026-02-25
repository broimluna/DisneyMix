using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public class UpdateChatThreadGameStateMessageNotificationEventArgs : AbstractUpdateChatThreadGameStateMessageNotificationEventArgs
	{
		public override UpdateChatThreadGameStateMessageNotification Notification { get; protected set; }

		public UpdateChatThreadGameStateMessageNotificationEventArgs(UpdateChatThreadGameStateMessageNotification notification)
		{
			Notification = notification;
		}
	}
}
