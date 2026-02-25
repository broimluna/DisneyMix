using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public abstract class AbstractRemoveChatThreadNicknameNotificationEventArgs : EventArgs
	{
		public abstract RemoveChatThreadNicknameNotification Notification { get; protected set; }
	}
}
