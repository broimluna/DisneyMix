using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public abstract class AbstractAddChatThreadNotificationEventArgs : EventArgs
	{
		public abstract AddChatThreadNotification Notification { get; protected set; }
	}
}
