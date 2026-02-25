namespace Disney.Mix.SDK.Internal
{
	public class StickerMessageAddedPushNotification : AbstractChatMessageAddedPushNotification, IChatMessageAddedPushNotification, IPushNotification, IStickerMessageAddedPushNotification
	{
		public StickerMessageAddedPushNotification(bool notificationsAvailable, string chatThreadId, string messageId)
			: base(notificationsAvailable, chatThreadId, messageId)
		{
		}
	}
}
