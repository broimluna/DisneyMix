using System;
using Disney.Mix.SDK;

namespace Mix.Session.Extensions
{
	public class AvatarHolder : IAvatarHolder
	{
		public IAvatar Avatar { get; private set; }

		public event EventHandler<AbstractAvatarChangedEventArgs> OnAvatarChanged = delegate
		{
		};

		public AvatarHolder(ILocalUser localUser)
		{
			localUser.OnAvatarChanged += delegate(object sender, AbstractAvatarChangedEventArgs e)
			{
				this.OnAvatarChanged(this, e);
			};
			Avatar = localUser.Avatar;
		}

		public AvatarHolder(IRemoteChatMember member)
		{
			if (member != null)
			{
				member.OnAvatarChanged += delegate(object sender, AbstractAvatarChangedEventArgs e)
				{
					this.OnAvatarChanged(this, e);
				};
				Avatar = member.Avatar;
			}
			else
			{
				Avatar = null;
			}
		}

		public AvatarHolder(IFriend friend)
		{
			friend.OnAvatarChanged += delegate(object sender, AbstractAvatarChangedEventArgs e)
			{
				this.OnAvatarChanged(this, e);
			};
			Avatar = friend.Avatar;
		}

		public AvatarHolder(IUnidentifiedUser user)
		{
			user.OnAvatarChanged += delegate(object sender, AbstractAvatarChangedEventArgs e)
			{
				this.OnAvatarChanged(this, e);
			};
			Avatar = user.Avatar;
		}
	}
}
