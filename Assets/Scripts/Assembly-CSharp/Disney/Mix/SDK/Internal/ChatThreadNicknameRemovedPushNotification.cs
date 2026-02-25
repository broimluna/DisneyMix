namespace Disney.Mix.SDK.Internal
{
	public class ChatThreadNicknameRemovedPushNotification : AbstractPushNotification, IChatThreadNicknameRemovedPushNotification, IPushNotification
	{
		public ChatThreadNicknameRemovedPushNotification(bool notificationsAvailable)
			: base(notificationsAvailable)
		{
		}
	}
}
