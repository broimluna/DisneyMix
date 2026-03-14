using System;

namespace Disney.Mix.SDK
{
	public abstract class AbstractChatThreadPhotoMessageAddedEventArgs : EventArgs
	{
		public abstract IPhotoMessage Message { get; protected set; }
	}
}
