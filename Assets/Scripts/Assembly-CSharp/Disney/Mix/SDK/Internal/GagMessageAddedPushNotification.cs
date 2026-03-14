namespace Disney.Mix.SDK.Internal
{
	public class GagMessageAddedPushNotification : AbstractChatMessageAddedPushNotification, IChatMessageAddedPushNotification, IGagMessageAddedPushNotification, IPushNotification
	{
		public GagMessageAddedPushNotification(bool notificationsAvailable, string chatThreadId, string messageId)
			: base(notificationsAvailable, chatThreadId, messageId)
		{
		}
	}
}
