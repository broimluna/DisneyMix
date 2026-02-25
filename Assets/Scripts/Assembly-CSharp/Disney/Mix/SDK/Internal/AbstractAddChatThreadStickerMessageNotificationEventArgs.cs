using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	public abstract class AbstractAddChatThreadStickerMessageNotificationEventArgs : EventArgs
	{
		public abstract AddChatThreadStickerMessageNotification Notification { get; protected set; }
	}
}
