using Mix.Games.Tray.Friendzy;
using Mix.Games.Tray.Friendzy.FSM;
using Mix.Games.Tray.Friendzy.Menu;
using Mix.Games.Tray.Friendzy.Message;

public class PlayerOneState : IState
{
	private FriendzyGame mFriendzyGame;

	public PlayerOneState(FriendzyGame inFriendzyGame)
	{
		mFriendzyGame = inFriendzyGame;
		mFriendzyGame.WhatPlayerIsPlaying = PLAYER.PLAYER_1;
		mFriendzyGame.FriendzyMenuController = new MenuController(mFriendzyGame);
	}

	void IState.Enter()
	{
	}

	void IState.Update()
	{
	}

	void IState.Exit()
	{
		mFriendzyGame.FriendzyMenuController.TurnOffMenu();
	}

	void IState.ReceiveMessage(IMessage eventMessage)
	{
	}
}
