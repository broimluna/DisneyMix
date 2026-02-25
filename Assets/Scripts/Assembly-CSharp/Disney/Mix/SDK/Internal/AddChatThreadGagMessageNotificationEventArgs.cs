using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public class AddChatThreadGagMessageNotificationEventArgs : AbstractAddChatThreadGagMessageNotificationEventArgs
	{
		public override AddChatThreadGagMessageNotification Notification { get; protected set; }

		public AddChatThreadGagMessageNotificationEventArgs(AddChatThreadGagMessageNotification notification)
		{
			Notification = notification;
		}
	}
}
