using System;

namespace Disney.Mix.SDK.Internal
{
	public class RemoteChatMember : IInternalRemoteChatMember, IRemoteChatMember
	{
		private readonly IUserDatabase userDatabase;

		public string Swid { get; private set; }

		public IDisplayName DisplayName { get; private set; }

		public string FirstName { get; private set; }

		public ChatMemberType ChatMemberType { get; private set; }

		public IAvatar Avatar
		{
			get
			{
				return InternalAvatar;
			}
		}

		public string Id
		{
			get
			{
				return Swid;
			}
		}

		public string HashedId
		{
			get
			{
				return userDatabase.GetUserBySwid(Swid).HashedSwid;
			}
		}

		public AccountStatus Status
		{
			get
			{
				string status = userDatabase.GetUserBySwid(Swid).Status;
				return GuestControllerUtils.GetAccountStatus(status);
			}
		}

		public IInternalAvatar InternalAvatar
		{
			get
			{
				UserDocument userBySwid = userDatabase.GetUserBySwid(Swid);
				long avatarId = userBySwid.AvatarId;
				if (avatarId != 0L)
				{
					AvatarDocument avatar = userDatabase.GetAvatar(userBySwid.AvatarId);
					return AvatarBuilder.Build(avatar);
				}
				return null;
			}
		}

		public event EventHandler<AbstractAvatarChangedEventArgs> OnAvatarChanged = delegate
		{
		};

		public RemoteChatMember(string swid, IDisplayName displayName, string firstName, IUserDatabase userDatabase)
		{
			this.userDatabase = userDatabase;
			Swid = swid;
			ChatMemberType = (OfficialAccountUtils.IsBotId(swid) ? ChatMemberType.Bot : ChatMemberType.Guest);
			DisplayName = displayName;
			FirstName = firstName;
		}

		public void DispatchOnAvatarChanged()
		{
			this.OnAvatarChanged(this, new AvatarChangedEventArgs(Avatar));
		}

		public void UpdateNames(string displayName, string firstName)
		{
			DisplayName = new DisplayName(displayName);
			FirstName = firstName;
		}
	}
}
