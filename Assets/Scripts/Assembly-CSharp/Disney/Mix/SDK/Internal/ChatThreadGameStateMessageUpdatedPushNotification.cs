namespace Disney.Mix.SDK.Internal
{
	public class ChatThreadGameStateMessageUpdatedPushNotification : AbstractPushNotification, IChatThreadGameStateMessageUpdatedPushNotification, IPushNotification
	{
		public ChatThreadGameStateMessageUpdatedPushNotification(bool notificationsAvailable)
			: base(notificationsAvailable)
		{
		}
	}
}
