using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public class ClearMemberChatHistoryNotificationEventArgs : AbstractClearMemberChatHistoryNotificationEventArgs
	{
		public override ClearMemberChatHistoryNotification Notification { get; protected set; }

		public ClearMemberChatHistoryNotificationEventArgs(ClearMemberChatHistoryNotification notification)
		{
			Notification = notification;
		}
	}
}
