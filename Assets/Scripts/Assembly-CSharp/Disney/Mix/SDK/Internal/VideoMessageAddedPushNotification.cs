namespace Disney.Mix.SDK.Internal
{
	public class VideoMessageAddedPushNotification : AbstractChatMessageAddedPushNotification, IChatMessageAddedPushNotification, IPushNotification, IVideoMessageAddedPushNotification
	{
		public VideoMessageAddedPushNotification(bool notificationsAvailable, string chatThreadId, string messageId)
			: base(notificationsAvailable, chatThreadId, messageId)
		{
		}
	}
}
