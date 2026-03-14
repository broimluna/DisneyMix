using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public abstract class AbstractRemoveNicknameNotificationEventArgs : EventArgs
	{
		public abstract RemoveNicknameNotification Notification { get; protected set; }
	}
}
