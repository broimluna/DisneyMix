using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public abstract class AbstractAddNicknameNotificationEventArgs : EventArgs
	{
		public abstract AddNicknameNotification Notification { get; protected set; }
	}
}
