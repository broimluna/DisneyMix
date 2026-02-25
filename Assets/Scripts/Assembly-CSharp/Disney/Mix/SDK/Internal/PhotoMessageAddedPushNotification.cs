namespace Disney.Mix.SDK.Internal
{
	public class PhotoMessageAddedPushNotification : AbstractChatMessageAddedPushNotification, IChatMessageAddedPushNotification, IPhotoMessageAddedPushNotification, IPushNotification
	{
		public PhotoMessageAddedPushNotification(bool notificationsAvailable, string chatThreadId, string messageId)
			: base(notificationsAvailable, chatThreadId, messageId)
		{
		}
	}
}
