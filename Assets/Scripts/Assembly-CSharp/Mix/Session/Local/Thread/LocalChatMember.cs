using System;
using Disney.Mix.SDK;

namespace Mix.Session.Local.Thread
{
	public class LocalChatMember : IFriend, IRemoteChatMember
	{
		public IFriend friend;

		public bool IsTrusted
		{
			get
			{
				return friend.IsTrusted;
			}
		}

		public IDisplayName DisplayName
		{
			get
			{
				return friend.DisplayName;
			}
		}

		public IUserNickname Nickname
		{
			get
			{
				return friend.Nickname;
			}
		}

		public IAvatar Avatar
		{
			get
			{
				return friend.Avatar;
			}
		}

		public AccountStatus Status
		{
			get
			{
				return AccountStatus.Active;
			}
		}

		public string Id
		{
			get
			{
				return friend.Id;
			}
		}

		public string HashedId
		{
			get
			{
				return friend.HashedId;
			}
		}

		public string FirstName
		{
			get
			{
				return friend.FirstName;
			}
		}

		public ChatMemberType ChatMemberType
		{
			get
			{
				return ChatMemberType.Guest;
			}
		}

		public event EventHandler<AbstractAvatarChangedEventArgs> OnAvatarChanged = delegate
		{
		};

		public event EventHandler<AbstractNicknameChangedEventArgs> OnNicknameChanged = delegate
		{
		};

		public LocalChatMember(IFriend friendToCopy)
		{
			friend = friendToCopy;
			friend.OnAvatarChanged += delegate(object sender, AbstractAvatarChangedEventArgs e)
			{
				this.OnAvatarChanged(sender, e);
			};
			friend.OnNicknameChanged += delegate(object sender, AbstractNicknameChangedEventArgs e)
			{
				this.OnNicknameChanged(sender, e);
			};
		}

		public void Report(IChatThread thread, ReportUserReason reason, Action<IReportUserResult> callback)
		{
			MixSession.User.ReportUser(friend, reason, callback);
		}
	}
}
