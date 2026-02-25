using System;

namespace Disney.Mix.SDK
{
	public interface IFriend
	{
		IAvatar Avatar { get; }

		IDisplayName DisplayName { get; }

		string FirstName { get; }

		string Id { get; }

		string HashedId { get; }

		bool IsTrusted { get; }

		IUserNickname Nickname { get; }

		AccountStatus Status { get; }

		event EventHandler<AbstractAvatarChangedEventArgs> OnAvatarChanged;

		event EventHandler<AbstractNicknameChangedEventArgs> OnNicknameChanged;
	}
}
