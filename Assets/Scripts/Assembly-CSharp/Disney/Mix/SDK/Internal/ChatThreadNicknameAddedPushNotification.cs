namespace Disney.Mix.SDK.Internal
{
	public class ChatThreadNicknameAddedPushNotification : AbstractPushNotification, IChatThreadNicknameAddedPushNotification, IPushNotification
	{
		public ChatThreadNicknameAddedPushNotification(bool notificationsAvailable)
			: base(notificationsAvailable)
		{
		}
	}
}
