namespace Disney.Mix.SDK.Internal
{
	public class AvatarChangedPushNotification : AbstractPushNotification, IAvatarChangedPushNotification, IPushNotification
	{
		public AvatarChangedPushNotification(bool notificationsAvailable)
			: base(notificationsAvailable)
		{
		}
	}
}
