using System;
using DG.Tweening;
using Disney.LaunchPad.Packages.FiniteStateMachine;

namespace Mix.Games.Tray.Drop.GameLogic
{
	public class ScoreboardDropGameState : DropGameState
	{
		private const float ANIM_TIME = 2f;

		private GameOverUI mScreen;

		private void Awake()
		{
			base.State.EventDispatcher.AddListener<StateEnterEvent>(OnStateEnter);
			base.State.EventDispatcher.AddListener<StateExitEvent>(OnStateExit);
		}

		private bool OnStateEnter(StateEnterEvent evnt)
		{
			base.Game.ShowScoreboard();
			base.Game.Player.gameObject.SetActive(false);
			if (base.Game.GhostPlayer != null)
			{
				base.Game.GhostPlayer.gameObject.SetActive(false);
			}
			base.Game.Environment.gameObject.SetActive(false);
			base.Game.GameController.MixGameCamera.gameObject.SetActive(false);
			base.Game.CommonUIElements.OpenMask();
			DropAudio.SwitchSound("MUS/Music", "Results");
			DOVirtual.DelayedCall(base.Game.DelayBeforeScoreboard, ShowUI);
			return false;
		}

		private bool OnStateExit(StateExitEvent evnt)
		{
			base.Game.UIManager.UnloadUI(base.Game.GameOverUI);
			CommonUI.OnMaskClosed = (Action)Delegate.Combine(CommonUI.OnMaskClosed, new Action(FinishExit));
			base.Game.CommonUIElements.CloseMask();
			return false;
		}

		private void ShowUI()
		{
			mScreen = base.Game.UIManager.LoadUI(base.Game.GameOverUI) as GameOverUI;
			mScreen.Init(base.Game.AllowRetry(), OnRetry, OnSaveAndQuit);
		}

		private void FinishExit()
		{
			CommonUI.OnMaskClosed = (Action)Delegate.Remove(CommonUI.OnMaskClosed, new Action(FinishExit));
			base.Game.HideScoreboard();
		}

		public void OnSaveAndQuit()
		{
			mScreen.DisableButtons();
			DropAudio.PlaySound("SFX/UI/ButtonPress");
			base.Game.GameEventDispatcher.DispatchEvent(new GameSendEvent());
		}

		public void OnRetry()
		{
			mScreen.DisableButtons();
			DropAudio.PlaySound("SFX/UI/ButtonPress");
			base.Game.GameEventDispatcher.DispatchEvent(new GameRetryEvent());
		}
	}
}
