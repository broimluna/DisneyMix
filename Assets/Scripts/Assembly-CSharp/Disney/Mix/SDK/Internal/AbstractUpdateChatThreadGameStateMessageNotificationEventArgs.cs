using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public abstract class AbstractUpdateChatThreadGameStateMessageNotificationEventArgs : EventArgs
	{
		public abstract UpdateChatThreadGameStateMessageNotification Notification { get; protected set; }
	}
}
