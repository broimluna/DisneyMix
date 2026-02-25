using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public class AddChatThreadGameEventMessageNotificationEventArgs : AbstractAddChatThreadGameEventMessageNotificationEventArgs
	{
		public override AddChatThreadGameEventMessageNotification Notification { get; protected set; }

		public AddChatThreadGameEventMessageNotificationEventArgs(AddChatThreadGameEventMessageNotification notification)
		{
			Notification = notification;
		}
	}
}
