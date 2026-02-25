using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public abstract class AbstractClearMemberChatHistoryNotificationEventArgs : EventArgs
	{
		public abstract ClearMemberChatHistoryNotification Notification { get; protected set; }
	}
}
