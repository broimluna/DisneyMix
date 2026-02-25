using DG.Tweening;
using UnityEngine;

namespace Mix.Games.Tray.Fireworks
{
	public class PlaybackLoader : MonoBehaviour
	{
		private const string ENTER_ANIMATION = "EnterAnimation";

		private const string IDLE_ANIMATION = "PlaybackIdle";

		public float TimeBeforePrompt = 1.5f;

		public GameObject LoadSpinner;

		public GameObject LoadPrompt;

		public Animator CameraAnimController;

		public FireworksGame GameManager;

		public GameObject SelectionScreen;

		public GameObject PlaybackLoadScreen;

		protected float mNormalizedTime;

		protected bool mIsEnterAnimationComplete;

		private float mTimeBeforeSpinner;

		private float mPromptPaddingTime = 1.5f;

		private bool mSceneLoaded;

		private Tween mSpinnerDelayTween;

		private Tween mPromptDelayTween;

		private void EnableSpinner(bool doSpin)
		{
			if (LoadSpinner != null)
			{
				LoadSpinner.SetActive(doSpin);
			}
		}

		private void ActivateSpinnerDelay()
		{
			if (!mSceneLoaded)
			{
				EnableSpinner(true);
			}
		}

		private void EnablePrompt(bool enable)
		{
			if (LoadPrompt != null && PlaybackLoadScreen != null)
			{
				if (enable)
				{
					PlaybackLoadScreen.SetActive(true);
					LoadPrompt.transform.DOScale(1f, 0.2f).SetEase(Ease.OutBack);
				}
				else
				{
					LoadPrompt.transform.DOScale(0f, 0.2f).SetEase(Ease.InBack);
				}
			}
		}

		private void ActivatePromptDelay()
		{
			if (!mSceneLoaded)
			{
				EnablePrompt(true);
			}
		}

		public void BeginLoading(Scene scene)
		{
			mTimeBeforeSpinner = TimeBeforePrompt + mPromptPaddingTime;
			mPromptDelayTween = DOVirtual.DelayedCall(TimeBeforePrompt, delegate
			{
				ActivatePromptDelay();
			});
			mSpinnerDelayTween = DOVirtual.DelayedCall(mTimeBeforeSpinner, delegate
			{
				ActivateSpinnerDelay();
			});
			if (!SelectionScreen.IsNullOrDisposed())
			{
				SelectionScreen.SetActive(false);
			}
			if (!GameManager.IsNullOrDisposed() && !GameManager.Background.IsNullOrDisposed())
			{
				SpriteRenderer component = GameManager.Background.transform.GetComponent<SpriteRenderer>();
				if (!component.IsNullOrDisposed() && !scene.IsNullOrDisposed())
				{
					component.sprite = scene.Background;
				}
			}
		}

		public void EndLoading()
		{
			mSceneLoaded = true;
			if (mPromptDelayTween != null)
			{
				mPromptDelayTween.Kill();
			}
			if (mSpinnerDelayTween != null)
			{
				mSpinnerDelayTween.Kill();
			}
			EnableSpinner(false);
			EnablePrompt(false);
			DOVirtual.DelayedCall(0.5f, delegate
			{
				PlaybackLoadScreen.SetActive(false);
			});
			if (CameraAnimController != null)
			{
				CameraAnimController.Play("EnterAnimation");
			}
		}

		public void BeginShowPlayback()
		{
			mIsEnterAnimationComplete = true;
			if (GameManager != null)
			{
				GameManager.StartTheShow();
			}
		}

		public void Pause()
		{
			if (mSceneLoaded && !mIsEnterAnimationComplete)
			{
				mNormalizedTime = CameraAnimController.GetCurrentAnimatorStateInfo(0).normalizedTime;
			}
		}

		public void Resume()
		{
			if (mSceneLoaded && !mIsEnterAnimationComplete)
			{
				CameraAnimController.Play("EnterAnimation", -1, mNormalizedTime);
			}
		}
	}
}
