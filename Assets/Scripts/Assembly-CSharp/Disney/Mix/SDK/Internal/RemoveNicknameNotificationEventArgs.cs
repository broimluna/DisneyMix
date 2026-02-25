using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public class RemoveNicknameNotificationEventArgs : AbstractRemoveNicknameNotificationEventArgs
	{
		public override RemoveNicknameNotification Notification { get; protected set; }

		public RemoveNicknameNotificationEventArgs(RemoveNicknameNotification notification)
		{
			Notification = notification;
		}
	}
}
