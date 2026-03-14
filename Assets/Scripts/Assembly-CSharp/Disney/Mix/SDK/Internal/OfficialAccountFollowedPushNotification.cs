namespace Disney.Mix.SDK.Internal
{
	public class OfficialAccountFollowedPushNotification : AbstractPushNotification, IOfficialAccountFollowedPushNotification, IPushNotification
	{
		public OfficialAccountFollowedPushNotification(bool notificationsAvailable)
			: base(notificationsAvailable)
		{
		}
	}
}
