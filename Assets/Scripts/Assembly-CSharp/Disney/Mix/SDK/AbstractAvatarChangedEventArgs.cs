using System;

namespace Disney.Mix.SDK
{
	public abstract class AbstractAvatarChangedEventArgs : EventArgs
	{
		public IAvatar Avatar { get; protected set; }
	}
}
