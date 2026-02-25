using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public class ClearUnreadMessageCountNotificationEventArgs : AbstractClearUnreadMessageCountNotificationEventArgs
	{
		public override ClearUnreadMessageCountNotification Notification { get; protected set; }

		public ClearUnreadMessageCountNotificationEventArgs(ClearUnreadMessageCountNotification notification)
		{
			Notification = notification;
		}
	}
}
