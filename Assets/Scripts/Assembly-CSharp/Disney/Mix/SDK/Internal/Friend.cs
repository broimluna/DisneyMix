using System;

namespace Disney.Mix.SDK.Internal
{
	public class Friend : IInternalFriend, IFriend
	{
		private readonly IUserDatabase userDatabase;

		public string Swid { get; private set; }

		public bool IsTrusted { get; private set; }

		public string HashedId
		{
			get
			{
				return userDatabase.GetUserBySwid(Swid).HashedSwid;
			}
		}

		public IUserNickname Nickname { get; private set; }

		public IDisplayName DisplayName { get; private set; }

		public string FirstName { get; private set; }

		public IAvatar Avatar
		{
			get
			{
				return InternalAvatar;
			}
		}

		public IInternalAvatar InternalAvatar
		{
			get
			{
				UserDocument userBySwid = userDatabase.GetUserBySwid(Swid);
				AvatarDocument avatar = userDatabase.GetAvatar(userBySwid.AvatarId);
				return AvatarBuilder.Build(avatar);
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

		public string Id
		{
			get
			{
				return Swid;
			}
		}

		public event EventHandler<AbstractAvatarChangedEventArgs> OnAvatarChanged = delegate
		{
		};

		public event EventHandler<AbstractNicknameChangedEventArgs> OnNicknameChanged = delegate
		{
		};

		public Friend(string swid, bool isTrusted, IDisplayName displayName, string firstName, IUserDatabase userDatabase)
		{
			this.userDatabase = userDatabase;
			Swid = swid;
			IsTrusted = isTrusted;
			DisplayName = displayName;
			FirstName = firstName;
		}

		public void ChangeTrust(bool isTrusted)
		{
			IsTrusted = isTrusted;
		}

		public void UpdateNickname(IUserNickname nickname)
		{
			Nickname = nickname;
			this.OnNicknameChanged(this, new NicknameChangedEventArgs(nickname));
		}

		public void DispatchOnAvatarChanged()
		{
			this.OnAvatarChanged(this, new AvatarChangedEventArgs(Avatar));
		}
	}
}
