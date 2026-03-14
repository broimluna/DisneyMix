using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public abstract class AbstractAddChatThreadMemberListChangedMessageNotificationEventArgs : EventArgs
	{
		public abstract AddChatThreadMemberListChangedMessageNotification Notification { get; protected set; }
	}
}
