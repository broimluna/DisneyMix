using System;
using Disney.Mix.SDK;

namespace Mix.Session.Extensions
{
	public class InvitableUser : IInvitableUser
	{
		public enum UserType
		{
			unidentified = 0,
			remote = 1
		}

		public IAvatar Avatar { get; private set; }

		public UserType userType { get; private set; }

		public IDisplayName DisplayName { get; private set; }

		public string FirstName { get; private set; }

		public IRemoteChatMember RemoteChatMember { get; private set; }

		public IUnidentifiedUser UnidentifiedUser { get; private set; }

		public event EventHandler<AbstractAvatarChangedEventArgs> OnAvatarChanged = delegate
		{
		};

		public InvitableUser(IRemoteChatMember member)
		{
			member.OnAvatarChanged += delegate(object sender, AbstractAvatarChangedEventArgs e)
			{
				this.OnAvatarChanged(this, e);
			};
			Avatar = member.Avatar;
			userType = UserType.remote;
			DisplayName = member.DisplayName;
			FirstName = member.FirstName;
			RemoteChatMember = member;
		}

		public InvitableUser(IUnidentifiedUser user)
		{
			user.OnAvatarChanged += delegate(object sender, AbstractAvatarChangedEventArgs e)
			{
				this.OnAvatarChanged(this, e);
			};
			Avatar = user.Avatar;
			userType = UserType.unidentified;
			DisplayName = user.DisplayName;
			FirstName = user.FirstName;
			UnidentifiedUser = user;
		}
	}
}
