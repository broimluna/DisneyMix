using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public abstract class AbstractAddChatThreadTextMessageNotificationEventArgs : EventArgs
	{
		public abstract AddChatThreadTextMessageNotification Notification { get; protected set; }
	}
}
