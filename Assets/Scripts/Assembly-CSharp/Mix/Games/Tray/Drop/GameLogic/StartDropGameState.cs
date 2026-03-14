using System;
using DG.Tweening;
using Disney.LaunchPad.Packages.FiniteStateMachine;
using UnityEngine;

namespace Mix.Games.Tray.Drop.GameLogic
{
	public class StartDropGameState : DropGameState
	{
		private IntroScene introScene;

		private void Awake()
		{
			base.State.EventDispatcher.AddListener<StateEnterEvent>(OnStateEnter);
			base.State.EventDispatcher.AddListener<StateExitEvent>(OnStateExit);
		}

		private bool OnStateEnter(StateEnterEvent evnt)
		{
			Pattern pattern = base.Game.LevelGenerator.GenerateInitialPattern();
			base.Game.GameTime = 0f;
			base.Game.Environment.gameObject.SetActive(true);
			base.Game.GameController.MixGameCamera.gameObject.SetActive(true);
			StartPlayer(pattern.StartPlatform);
			StartGhostPlayer(pattern.StartPlatform);
			base.Game.CameraZoomer.Zoom = base.Game.InitialZoomAmount;
			if (base.Game.IsFirstRound)
			{
				introScene = UnityEngine.Object.Instantiate(base.Game.IntroScenePrefab);
				introScene.transform.SetParent(base.Game.transform, false);
				IntroScene obj = introScene;
				obj.OnIntroComplete = (Action)Delegate.Combine(obj.OnIntroComplete, new Action(OnIntroAnimationComplete));
				DropAudio.PlaySound("SFX/IntroCutScene");
			}
			else
			{
				DOVirtual.DelayedCall(base.Game.DelayBeforeStarting, delegate
				{
					base.Game.GameEventDispatcher.DispatchEvent(new GameStartEvent());
				});
			}
			return false;
		}

		private bool OnStateExit(StateExitEvent evnt)
		{
			base.Game.StartPlaying();
			base.Game.FollowCamera.enabled = true;
			base.Game.FollowCamera.SnapToTarget();
			base.Game.CommonUIElements.OpenMask();
			DropAudio.SwitchSound("MUS/Music", "Gameplay");
			return false;
		}

		private void StartPlayer(Platform startingPlatform)
		{
			base.Game.Player.gameObject.SetActive(true);
			base.Game.Player.SnapToPlatform(startingPlatform, 0f);
			base.Game.Player.Active = true;
		}

		private void StartGhostPlayer(Platform startingPlatform)
		{
			if (!base.Game.GhostPlayer.IsNullOrDisposed())
			{
				base.Game.Player.gameObject.SetActive(true);
				base.Game.GhostPlayer.SnapToPlatform(startingPlatform, 0f);
				base.Game.GhostPlayer.Active = true;
			}
		}

		private void OnIntroAnimationComplete()
		{
			IntroScene obj = introScene;
			obj.OnIntroComplete = (Action)Delegate.Remove(obj.OnIntroComplete, new Action(OnIntroAnimationComplete));
			UnityEngine.Object.Destroy(introScene.gameObject, 2f);
			base.Game.GameEventDispatcher.DispatchEvent(new GameStartEvent());
		}
	}
}
