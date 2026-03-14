using System;

namespace Disney.Mix.SDK
{
	public abstract class AbstractChatThreadStickerMessageAddedEventArgs : EventArgs
	{
		public abstract IStickerMessage Message { get; protected set; }
	}
}
