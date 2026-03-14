using System;
using DG.Tweening;
using Disney.LaunchPad.Packages.FiniteStateMachine;
using Mix.Games.Session;

namespace Mix.Games.Tray.Drop.GameLogic
{
	public class GameOverDropGameState : DropGameState
	{
		private void Awake()
		{
			base.State.EventDispatcher.AddListener<StateEnterEvent>(OnStateEnter);
			base.State.EventDispatcher.AddListener<StateExitEvent>(OnStateExit);
		}

		private bool OnStateEnter(StateEnterEvent evnt)
		{
			string aGameMessage = string.Format("{0}|{1}|{2}|{3}", base.Game.Score, base.Game.Player.BonusCount, base.Game.Player.DeathType.ToString().ToLower(), base.Game.LevelGenerator.CurrentPatternName);
			base.Game.GameController.LogEvent(GameLogEventType.ACTION, "winter_waltz_player_died", aGameMessage);
			base.Game.StopPlaying();
			base.Game.UIManager.UnloadUI(base.Game.PlayingUI);
			Sequence s = DOTween.Sequence();
			s.InsertCallback(base.Game.DelayBeforeClosingMask, base.Game.CommonUIElements.CloseMask);
			CommonUI.OnMaskClosed = (Action)Delegate.Combine(CommonUI.OnMaskClosed, new Action(OnMaskClosed));
			return false;
		}

		private bool OnStateExit(StateExitEvent evnt)
		{
			return false;
		}

		private void OnMaskClosed()
		{
			CommonUI.OnMaskClosed = (Action)Delegate.Remove(CommonUI.OnMaskClosed, new Action(OnMaskClosed));
			base.Game.LevelGenerator.Reset();
			base.Game.GameEventDispatcher.DispatchEvent(new GameScoreboardEvent());
		}
	}
}
