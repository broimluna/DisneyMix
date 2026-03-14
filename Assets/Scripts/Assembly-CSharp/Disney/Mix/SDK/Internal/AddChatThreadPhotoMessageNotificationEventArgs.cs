using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public class AddChatThreadPhotoMessageNotificationEventArgs : AbstractAddChatThreadPhotoMessageNotificationEventArgs
	{
		public override AddChatThreadPhotoMessageNotification Notification { get; protected set; }

		public AddChatThreadPhotoMessageNotificationEventArgs(AddChatThreadPhotoMessageNotification notification)
		{
			Notification = notification;
		}
	}
}
