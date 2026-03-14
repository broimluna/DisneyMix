namespace Disney.Mix.SDK.Internal
{
	public class OfficialAccountUnfollowedPushNotification : AbstractPushNotification, IOfficialAccountUnfollowedPushNotification, IPushNotification
	{
		public OfficialAccountUnfollowedPushNotification(bool notificationsAvailable)
			: base(notificationsAvailable)
		{
		}
	}
}
