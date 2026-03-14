using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public class AddChatThreadNicknameNotificationEventArgs : AbstractAddChatThreadNicknameNotificationEventArgs
	{
		public override AddChatThreadNicknameNotification Notification { get; protected set; }

		public AddChatThreadNicknameNotificationEventArgs(AddChatThreadNicknameNotification notification)
		{
			Notification = notification;
		}
	}
}
