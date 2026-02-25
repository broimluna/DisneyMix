using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public class AddChatThreadStickerMessageNotificationEventArgs : AbstractAddChatThreadStickerMessageNotificationEventArgs
	{
		public override AddChatThreadStickerMessageNotification Notification { get; protected set; }

		public AddChatThreadStickerMessageNotificationEventArgs(AddChatThreadStickerMessageNotification notification)
		{
			Notification = notification;
		}
	}
}
