using System;

namespace Disney.Mix.SDK
{
	public interface IUnidentifiedUser
	{
		IAvatar Avatar { get; }

		IDisplayName DisplayName { get; }

		string FirstName { get; }

		event EventHandler<AbstractAvatarChangedEventArgs> OnAvatarChanged;
	}
}
