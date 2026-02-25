using System;

namespace Disney.Mix.SDK
{
	public interface IRemoteChatMember
	{
		IAvatar Avatar { get; }

		IDisplayName DisplayName { get; }

		string FirstName { get; }

		ChatMemberType ChatMemberType { get; }

		string Id { get; }

		string HashedId { get; }

		AccountStatus Status { get; }

		event EventHandler<AbstractAvatarChangedEventArgs> OnAvatarChanged;
	}
}
