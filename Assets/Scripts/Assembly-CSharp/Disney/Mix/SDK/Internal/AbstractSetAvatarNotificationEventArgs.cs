using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public abstract class AbstractSetAvatarNotificationEventArgs : EventArgs
	{
		public abstract SetAvatarNotification Notification { get; protected set; }
	}
}
