using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public class RemoveChatThreadNicknameNotificationEventArgs : AbstractRemoveChatThreadNicknameNotificationEventArgs
	{
		public override RemoveChatThreadNicknameNotification Notification { get; protected set; }

		public RemoveChatThreadNicknameNotificationEventArgs(RemoveChatThreadNicknameNotification notification)
		{
			Notification = notification;
		}
	}
}
