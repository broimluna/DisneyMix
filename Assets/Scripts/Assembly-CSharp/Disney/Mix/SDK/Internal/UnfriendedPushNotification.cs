namespace Disney.Mix.SDK.Internal
{
	public class UnfriendedPushNotification : AbstractPushNotification, IPushNotification, IUnfriendedPushNotification
	{
		public UnfriendedPushNotification(bool notificationsAvailable)
			: base(notificationsAvailable)
		{
		}
	}
}
