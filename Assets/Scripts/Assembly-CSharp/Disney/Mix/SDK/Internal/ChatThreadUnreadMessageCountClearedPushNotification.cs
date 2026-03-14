namespace Disney.Mix.SDK.Internal
{
	public class ChatThreadUnreadMessageCountClearedPushNotification : AbstractPushNotification, IChatThreadUnreadMessageCountClearedPushNotification, IPushNotification
	{
		public ChatThreadUnreadMessageCountClearedPushNotification(bool notificationsAvailable)
			: base(notificationsAvailable)
		{
		}
	}
}
