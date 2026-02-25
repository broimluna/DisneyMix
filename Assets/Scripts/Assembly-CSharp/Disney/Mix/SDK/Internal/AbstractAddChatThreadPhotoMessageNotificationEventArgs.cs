using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public abstract class AbstractAddChatThreadPhotoMessageNotificationEventArgs : EventArgs
	{
		public abstract AddChatThreadPhotoMessageNotification Notification { get; protected set; }
	}
}
