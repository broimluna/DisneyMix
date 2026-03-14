using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public class AddChatThreadMemberListChangedMessageNotificationEventArgs : AbstractAddChatThreadMemberListChangedMessageNotificationEventArgs
	{
		public override AddChatThreadMemberListChangedMessageNotification Notification { get; protected set; }

		public AddChatThreadMemberListChangedMessageNotificationEventArgs(AddChatThreadMemberListChangedMessageNotification notification)
		{
			Notification = notification;
		}
	}
}
