using System.Collections.Generic;
using Disney.Mix.SDK;
using Mix.FakeFriend.Datatypes;
using Mix.Session.Extensions;

namespace Mix.Games.Session
{
	public class GameSessionThreadParameters : IGameThreadParameters
	{
		protected IChatThread mThread;

		bool IGameThreadParameters.IsGroupSession
		{
			get
			{
				return mThread is IGroupChatThread || mThread is IOfficialAccountChatThread;
			}
		}

		bool IGameThreadParameters.IsOneOnOneFriend
		{
			get
			{
				if (mThread is IOneOnOneChatThread)
				{
					IOneOnOneChatThread thread = mThread as IOneOnOneChatThread;
					return thread.IsOtherUserFriend();
				}
				return false;
			}
		}

		bool IGameThreadParameters.IsOneOnOneSession
		{
			get
			{
				return mThread is IOneOnOneChatThread;
			}
		}

		IEnumerable<string> IGameThreadParameters.Members
		{
			get
			{
				List<string> list = new List<string>();
				foreach (IRemoteChatMember remoteMember in mThread.RemoteMembers)
				{
					list.Add(remoteMember.Id);
				}
				return list;
			}
		}

		IEnumerable<string> IGameThreadParameters.FormerMembers
		{
			get
			{
				List<string> list = new List<string>();
				foreach (IRemoteChatMember formerRemoteMember in mThread.FormerRemoteMembers)
				{
					list.Add(formerRemoteMember.Id);
				}
				return list;
			}
		}

		object IGameThreadParameters.Thread
		{
			get
			{
				return mThread;
			}
		}

		string IGameThreadParameters.ThreadId
		{
			get
			{
				return mThread.Id;
			}
		}

		bool IGameThreadParameters.IsFakeThread
		{
			get
			{
				return mThread is FakeThread;
			}
		}

		public GameSessionThreadParameters(IChatThread aThread)
		{
			mThread = aThread;
		}

		string IGameThreadParameters.GetSenderName(string aSenderId)
		{
			return mThread.GetNickOrDisplayById(aSenderId);
		}
	}
}
