using System;
using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK;
using Mix.Session.Local.Messages;

namespace Mix.Session.Local.Thread
{
	public class GroupLocalThread : LocalThread, IChatThread, IGroupChatThread
	{
		 IChatThreadNickname IGroupChatThread.Nickname
		{
			get
			{
				return base.Nickname;
			}
		}

		public GroupLocalThread(ILocalUser user, IEnumerable<IFriend> members)
			: base(user, members)
		{
		}

		protected override void CreateThread(Action<IChatThread> callback)
		{
			if (IsCreating || User == null)
			{
				return;
			}
			IsCreating = true;
			User.CreateGroupChatThread(members, actionGenerator.CreateAction(delegate(ICreateGroupChatThreadResult result)
			{
				if (MixSession.IsValidSession)
				{
					if (result.Success)
					{
						callback(result.ChatThread);
					}
					else if (localSends != null)
					{
						foreach (ActionAndCallback value in localSends.Values)
						{
							if (value != null && value.isPendingSend && value.sendAction != null)
							{
								value.sendAction(false, value.resendCallback);
								value.isPendingSend = false;
							}
						}
					}
					IsCreating = false;
				}
			}));
		}

		public IGagMessage SendGagMessage(string contentId, IRemoteChatMember target, Action<ISendGagMessageResult> callback)
		{
			IGroupChatThread groupThread = realThread as IGroupChatThread;
			if (groupThread != null)
			{
				return groupThread.SendGagMessage(contentId, target, callback);
			}
			LocalGagMessage fakeMsg = new LocalGagMessage(contentId, target.Id, User.Id);
			localSends[fakeMsg] = new ActionAndCallback(delegate(bool result, Action<bool> resendCallback)
			{
				if (result)
				{
					IRemoteChatMember remoteChatMember = realThread.RemoteMembers.FirstOrDefault((IRemoteChatMember mem) => mem.Id == target.Id);
					if (remoteChatMember == null)
					{
						remoteChatMember = realThread.FormerRemoteMembers.FirstOrDefault((IRemoteChatMember mem) => mem.Id == target.Id);
					}
					IGagMessage message = null;
					groupThread = realThread as IGroupChatThread;
					message = groupThread.SendGagMessage(contentId, remoteChatMember, delegate(ISendGagMessageResult res)
					{
						handleSendResponse(fakeMsg, message, res, res.Success, resendCallback, callback);
					});
					handlePreSendResponse(fakeMsg, message);
				}
				else
				{
					handleFailedThreadCreation(resendCallback, callback);
				}
			}, null);
			CreateThread(base.InternalSetRealThread);
			return fakeMsg;
		}

		public bool Equals(IGroupChatThread other)
		{
			if (other == null || !(other is GroupLocalThread))
			{
				return false;
			}
			return other.Id == Id;
		}

		 IChatThreadNickname IGroupChatThread.SetNickname(string nickname, Action<ISetChatThreadNicknameResult> callback)
		{
			return SetNickname(nickname, callback);
		}

		 void IGroupChatThread.RemoveNickname(Action<IRemoveChatThreadNicknameResult> callback)
		{
			RemoveNickname(callback);
		}

		 event EventHandler<AbstractChatThreadNicknameChangedEventArgs> IGroupChatThread.OnNicknameChanged
		 {
			 add { base.OnNicknameChanged += value; }
			 remove { base.OnNicknameChanged -= value; }
		 }

		 event EventHandler<AbstractChatThreadGagMessageAddedEventArgs> IGroupChatThread.OnGagMessageAdded
		 {
			 add { base.OnGagMessageAdded += value; }
			 remove { base.OnGagMessageAdded -= value; }
		 }
	}
}
