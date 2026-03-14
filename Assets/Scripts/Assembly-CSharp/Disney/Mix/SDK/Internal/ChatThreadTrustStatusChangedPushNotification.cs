namespace Disney.Mix.SDK.Internal
{
	public class ChatThreadTrustStatusChangedPushNotification : AbstractPushNotification, IChatThreadTrustStatusChangedPushNotification, IPushNotification
	{
		public ChatThreadTrustStatusChangedPushNotification(bool notificationsAvailable)
			: base(notificationsAvailable)
		{
		}
	}
}
