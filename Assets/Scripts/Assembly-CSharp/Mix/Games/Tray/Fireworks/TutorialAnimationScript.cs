using Mix.DeviceDb;
using UnityEngine;

namespace Mix.Games.Tray.Fireworks
{
	public class TutorialAnimationScript : MonoBehaviour
	{
		public FireworksGame Game;

		public GameObject PromptText;

		public GameObject ContinueButton;

		public RectTransform FingerPosition;

		private Vector3 mFingerPosition;

		private Vector3 mTapLaunchPosition;

		private FireworkGesture mGestureFirework;

		private bool mIsMovingGestureFirework;

		private Vector3 mGestureStartLocation;

		private Vector2 mResetPosition = new Vector2(0f, 0f);

		private Vector3 mResetScale = new Vector3(0f, 0f, 1f);

		private bool mIsContinueActive;

		public void PlayTapFireworkTutorial()
		{
			mTapLaunchPosition = Toolbox.Instance.mFireworkGame.transform.InverseTransformPoint(GetFireworkPositionForFinger());
			Game.LaunchDisplayFirework(mTapLaunchPosition);
		}

		public void PlayButtonFireworkTutorial()
		{
			Game.LaunchDisplayFirework(default(Vector3), 2);
		}

		public void TurnOnGestureFireworkTutorial()
		{
			mGestureFirework = Toolbox.Instance.mFireworkManager.mGestureFirework;
			mIsMovingGestureFirework = true;
			mGestureFirework.gameObject.SetActive(true);
			mGestureFirework.Play();
			mGestureStartLocation = GetFireworkPositionForFinger();
			mGestureFirework.gameObject.transform.position = mGestureStartLocation;
		}

		public void TurnOffGestureFireworkTutorial()
		{
			mIsMovingGestureFirework = false;
			mGestureFirework.Stop();
		}

		public void SetGestureLocation()
		{
			mGestureFirework.gameObject.transform.position = mGestureStartLocation;
			mGestureFirework = null;
		}

		public void TurnOffAnimation()
		{
			RectTransform component = base.gameObject.GetComponent<RectTransform>();
			component.anchoredPosition = mResetPosition;
			component.localScale = mResetScale;
			base.gameObject.SetActive(false);
		}

		public void ShowTapToContinue()
		{
			if (!mIsContinueActive)
			{
				PromptText.SetActive(true);
				ContinueButton.SetActive(true);
				mIsContinueActive = true;
			}
		}

		public void SetTutorialPlayed()
		{
			if (!DebugSceneIndicator.IsDebugScene)
			{
				IKeyValDatabaseApi keyValDocumentCollectionApi = Mix.Singleton<MixDocumentCollections>.Instance.keyValDocumentCollectionApi;
				keyValDocumentCollectionApi.SaveUserValueFromBool("TutorialPlayed", true);
			}
		}

		private Vector3 GetFireworkPositionForFinger()
		{
			mFingerPosition = FingerPosition.position;
			return Toolbox.Instance.mSceneManager.mCurrentScene.TapPlane.GetPointProjectedOntoTapPlane(mFingerPosition, Game.GameController.MixGameCamera);
		}

		private void Update()
		{
			if (mIsMovingGestureFirework && mGestureFirework != null)
			{
				Game.LightUpSceneG();
				mGestureFirework.gameObject.transform.position = GetFireworkPositionForFinger();
			}
		}
	}
}
