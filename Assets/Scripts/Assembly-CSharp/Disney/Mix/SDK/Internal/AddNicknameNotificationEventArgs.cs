using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public class AddNicknameNotificationEventArgs : AbstractAddNicknameNotificationEventArgs
	{
		public override AddNicknameNotification Notification { get; protected set; }

		public AddNicknameNotificationEventArgs(AddNicknameNotification notification)
		{
			Notification = notification;
		}
	}
}
