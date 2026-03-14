using System;
using Disney.Mix.SDK;

namespace Mix.Session.Extensions
{
	public interface IAvatarHolder
	{
		IAvatar Avatar { get; }

		event EventHandler<AbstractAvatarChangedEventArgs> OnAvatarChanged;
	}
}
