namespace Disney.Mix.SDK.Internal
{
	public class ChatThreadMemberRemovedPushNotification : AbstractPushNotification, IChatThreadMemberRemovedPushNotification, IPushNotification
	{
		public ChatThreadMemberRemovedPushNotification(bool notificationsAvailable)
			: base(notificationsAvailable)
		{
		}
	}
}
