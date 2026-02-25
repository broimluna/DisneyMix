using System;
using Disney.Mix.SDK;

namespace Mix.Session.Extensions
{
	public interface IInvitableUser
	{
		InvitableUser.UserType userType { get; }

		IAvatar Avatar { get; }

		IDisplayName DisplayName { get; }

		string FirstName { get; }

		IRemoteChatMember RemoteChatMember { get; }

		IUnidentifiedUser UnidentifiedUser { get; }

		event EventHandler<AbstractAvatarChangedEventArgs> OnAvatarChanged;
	}
}
