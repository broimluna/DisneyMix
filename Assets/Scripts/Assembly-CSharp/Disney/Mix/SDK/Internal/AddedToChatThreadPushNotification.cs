namespace Disney.Mix.SDK.Internal
{
	public class AddedToChatThreadPushNotification : AbstractPushNotification, IAddedToChatThreadPushNotification, IPushNotification
	{
		public AddedToChatThreadPushNotification(bool notificationsAvailable)
			: base(notificationsAvailable)
		{
		}
	}
}
