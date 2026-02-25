namespace Disney.Mix.SDK.Internal
{
	public class UntrustedPushNotification : AbstractPushNotification, IPushNotification, IUntrustedPushNotification
	{
		public UntrustedPushNotification(bool notificationsAvailable)
			: base(notificationsAvailable)
		{
		}
	}
}
