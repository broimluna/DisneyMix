using DG.Tweening;
using Mix.Games.Data;
using Mix.Games.Tray.Common;
using Mix.Ui;
using UnityEngine;

namespace Mix.Games.Tray.HighFive
{
	public class HighFiveGame : MonoBehaviour, IMixGame
	{
		private const string MUSIC_CUE_NAME = "HighFive/MUS/Music";

		public HighFivePlayMode PlayMode;

		public AnimationEvents GameCameraAnimation;

		public bool DebugIsFirstPlayer = true;

		protected bool mShouldResumeGame;

		protected bool mIsIntroPlaying;

		private HighFiveGameController mGameController;

		public HighFiveGameController GameController
		{
			get
			{
				return mGameController;
			}
		}

		void IMixGame.Initialize(MixGameData aData)
		{
			GameCameraAnimation.OnAnimationStart += delegate
			{
				mIsIntroPlaying = true;
			};
			GameCameraAnimation.OnAnimationEnd += delegate
			{
				mIsIntroPlaying = false;
				if (mShouldResumeGame)
				{
					mShouldResumeGame = false;
					PlayMode.Resume();
				}
			};
			if (DebugSceneIndicator.IsMainScene)
			{
				if (aData != null)
				{
					HighFiveData gameData = GameController.GetGameData<HighFiveData>();
					PlayMode.Init(gameData.RandomSeed, false);
					int num = 0;
					for (int num2 = 0; num2 < gameData.Responses.Count; num2++)
					{
						num = Mathf.Max(num, gameData.Responses[num2].TotalScore);
					}
					PlayMode.GameStats.BestScore = num;
				}
				else
				{
					PlayMode.Init(0, true);
				}
			}
			else
			{
				PlayMode.Init(0, DebugIsFirstPlayer);
			}
			EnterPlayMode();
		}

		void IMixGame.Pause()
		{
			if (!mIsIntroPlaying)
			{
				PlayMode.Pause();
			}
			BaseGameController.Instance.Session.SessionSounds.PauseSoundEvent("HighFive/MUS/Music", base.gameObject);
			DOTween.PauseAll();
		}

		void IMixGame.Play()
		{
			DOVirtual.DelayedCall(0.1f, delegate
			{
				BaseGameController.Instance.Session.SessionSounds.PlayMusic("HighFive/MUS/Music", base.gameObject);
			});
		}

		void IMixGame.Quit()
		{
			BaseGameController.Instance.Session.SessionSounds.StopSoundEvent("HighFive/MUS/Music", base.gameObject);
			DOTween.KillAll();
		}

		void IMixGame.Resume()
		{
			mShouldResumeGame = true;
			DOVirtual.DelayedCall(0.1f, delegate
			{
				BaseGameController.Instance.Session.SessionSounds.UnpauseSoundEvent("HighFive/MUS/Music", base.gameObject);
			});
			DOTween.PlayAll();
		}

		private void OnEnable()
		{
			TrayGamePhysics.ResetAllGameLayers();
		}

		private void Awake()
		{
			mGameController = base.transform.parent.gameObject.GetComponent<HighFiveGameController>();
		}

		private void EnterPlayMode()
		{
			PlayMode.gameObject.SetActive(true);
		}
	}
}
