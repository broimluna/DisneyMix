using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public abstract class AbstractAddChatThreadGameEventMessageNotificationEventArgs : EventArgs
	{
		public abstract AddChatThreadGameEventMessageNotification Notification { get; protected set; }
	}
}
