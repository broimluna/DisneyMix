using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public class SetAvatarNotificationEventArgs : AbstractSetAvatarNotificationEventArgs
	{
		public override SetAvatarNotification Notification { get; protected set; }

		public SetAvatarNotificationEventArgs(SetAvatarNotification notification)
		{
			Notification = notification;
		}
	}
}
