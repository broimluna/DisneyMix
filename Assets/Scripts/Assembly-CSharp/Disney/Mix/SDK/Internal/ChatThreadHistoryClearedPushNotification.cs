namespace Disney.Mix.SDK.Internal
{
	public class ChatThreadHistoryClearedPushNotification : AbstractPushNotification, IChatThreadHistoryClearedPushNotification, IPushNotification
	{
		public ChatThreadHistoryClearedPushNotification(bool notificationsAvailable)
			: base(notificationsAvailable)
		{
		}
	}
}
