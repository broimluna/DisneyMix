using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public abstract class AbstractAddChatThreadVideoMessageNotificationEventArgs : EventArgs
	{
		public abstract AddChatThreadVideoMessageNotification Notification { get; protected set; }
	}
}
