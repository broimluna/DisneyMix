namespace Disney.Mix.SDK.Internal
{
	public class TextMessageAddedPushNotification : AbstractChatMessageAddedPushNotification, IChatMessageAddedPushNotification, IPushNotification, ITextMessageAddedPushNotification
	{
		public TextMessageAddedPushNotification(bool notificationsAvailable, string chatThreadId, string messageId)
			: base(notificationsAvailable, chatThreadId, messageId)
		{
		}
	}
}
