namespace Disney.Mix.SDK.Internal
{
	public class ChatThreadMemberAddedPushNotification : AbstractPushNotification, IChatThreadMemberAddedPushNotification, IPushNotification
	{
		public ChatThreadMemberAddedPushNotification(bool notificationsAvailable)
			: base(notificationsAvailable)
		{
		}
	}
}
