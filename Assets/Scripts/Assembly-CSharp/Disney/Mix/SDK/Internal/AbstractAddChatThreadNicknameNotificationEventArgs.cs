using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public abstract class AbstractAddChatThreadNicknameNotificationEventArgs : EventArgs
	{
		public abstract AddChatThreadNicknameNotification Notification { get; protected set; }
	}
}
