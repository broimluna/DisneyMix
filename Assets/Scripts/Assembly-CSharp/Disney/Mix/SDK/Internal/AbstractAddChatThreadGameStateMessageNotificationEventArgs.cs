using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public abstract class AbstractAddChatThreadGameStateMessageNotificationEventArgs : EventArgs
	{
		public abstract AddChatThreadGameStateMessageNotification Notification { get; protected set; }
	}
}
