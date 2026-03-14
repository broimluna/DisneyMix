using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public abstract class AbstractAddChatThreadGagMessageNotificationEventArgs : EventArgs
	{
		public abstract AddChatThreadGagMessageNotification Notification { get; protected set; }
	}
}
