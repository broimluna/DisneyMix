using System;
using System.Collections.Generic;
using System.Linq;
using Disney.Mix.SDK;
using Mix.Session.Local.Messages;

namespace Mix.Session.Local.Thread
{
	public class OneOnOneLocalThread : LocalThread, IChatThread, IOneOnOneChatThread
	{
		 IChatThreadNickname IOneOnOneChatThread.Nickname
		{
			get
			{
				return base.Nickname;
			}
		}

		public event EventHandler<AbstractChatHistoryClearedEventArgs> OnChatHistoryCleared = delegate
		{
		};

		public OneOnOneLocalThread(ILocalUser user, IEnumerable<IFriend> members)
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
			User.CreateOneOnOneChatThread(members.FirstOrDefault(), actionGenerator.CreateAction(delegate(ICreateOneOnOneChatThreadResult result)
			{
				if (MixSession.IsValidSession)
				{
					if (result.Success)
					{
						result.ChatThread.OnChatHistoryCleared += delegate(object sender, AbstractChatHistoryClearedEventArgs e)
						{
							this.OnChatHistoryCleared(this, e);
						};
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

		public void ClearChatHistory(Action<IClearChatHistoryResult> callback)
		{
			if (realThread != null)
			{
				((IOneOnOneChatThread)realThread).ClearChatHistory(callback);
			}
		}

		public IGagMessage SendGagMessage(string contentId, IRemoteChatMember target, Action<ISendGagMessageResult> callback)
		{
			IOneOnOneChatThread oneOnOneThread = realThread as IOneOnOneChatThread;
			if (oneOnOneThread != null)
			{
				return oneOnOneThread.SendGagMessage(contentId, target, callback);
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
					oneOnOneThread = realThread as IOneOnOneChatThread;
					message = oneOnOneThread.SendGagMessage(contentId, remoteChatMember, delegate(ISendGagMessageResult res)
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

		public bool Equals(IOneOnOneChatThread other)
		{
			if (other == null || !(other is OneOnOneLocalThread))
			{
				return false;
			}
			return other.Id == Id;
		}

		 IChatThreadNickname IOneOnOneChatThread.SetNickname(string nickname, Action<ISetChatThreadNicknameResult> callback)
		{
			return SetNickname(nickname, callback);
		}

		 void IOneOnOneChatThread.RemoveNickname(Action<IRemoveChatThreadNicknameResult> callback)
		{
			RemoveNickname(callback);
		}

		 event EventHandler<AbstractChatThreadNicknameChangedEventArgs> IOneOnOneChatThread.OnNicknameChanged
		{
			add { base.OnNicknameChanged += value; }
			remove { base.OnNicknameChanged -= value; }
		}

		 event EventHandler<AbstractChatThreadGagMessageAddedEventArgs> IOneOnOneChatThread.OnGagMessageAdded
		{
			add { base.OnGagMessageAdded += value; }
			remove { base.OnGagMessageAdded -= value; }
		}
	}
}
