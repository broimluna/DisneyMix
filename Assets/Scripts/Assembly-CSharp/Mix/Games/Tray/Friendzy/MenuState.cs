using Mix.Games.Tray.Friendzy.FSM;
using Mix.Games.Tray.Friendzy.Message;
using UnityEngine;

namespace Mix.Games.Tray.Friendzy
{
	public class MenuState : IState
	{
		private FriendzyGame mFriendzyGame;

		public MenuState(FriendzyGame inFriendzyGame)
		{
			mFriendzyGame = inFriendzyGame;
			Debug.Log(mFriendzyGame);
		}

		void IState.Enter()
		{
		}

		void IState.Update()
		{
		}

		void IState.Exit()
		{
		}

		void IState.ReceiveMessage(IMessage eventMessage)
		{
		}
	}
}
