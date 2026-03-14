using Mix.DeviceDb;
using UnityEngine;

namespace Mix.Games.Tray.Fireworks
{
	public class PlayMenuTransitionAnimations : MonoBehaviour
	{
		private const string mENTER_ANIMATION = "EnterAnimation";

		private const string mEXIT_ANIMATION = "ExitAnimation";

		public GameObject mParentMenu;

		public GameObject[] mNextMenus;

		public GameObject[] AlternateNextMenus;

		public Animator[] mDependentAnimators;

		private Animator mParentAnimator;

		private string mLastAnimationPlayed = string.Empty;

		private void OnEnable()
		{
			mParentAnimator = base.gameObject.transform.GetComponent<Animator>();
			if (mLastAnimationPlayed.Length > 0)
			{
				PlayAnimationIncludingDependents(mLastAnimationPlayed);
			}
		}

		private void Start()
		{
		}

		public void PlayExitAnimation()
		{
			PlayAnimationIncludingDependents("ExitAnimation");
		}

		public void PlayDependentAnimations(string animation)
		{
			if (mDependentAnimators == null)
			{
				return;
			}
			for (int i = 0; i < mDependentAnimators.Length; i++)
			{
				if (mDependentAnimators[i] != null)
				{
					mDependentAnimators[i].Play(animation);
				}
			}
		}

		private void PlayAnimationIncludingDependents(string animation)
		{
			if (mParentAnimator != null)
			{
				mLastAnimationPlayed = animation;
				mParentAnimator.Play(animation);
				PlayDependentAnimations(animation);
			}
		}

		public void ShowNextMenus()
		{
			IKeyValDatabaseApi keyValDatabaseApi = null;
			if (!(mParentMenu != null))
			{
				return;
			}
			if (!DebugSceneIndicator.IsDebugScene)
			{
				keyValDatabaseApi = Mix.Singleton<MixDocumentCollections>.Instance.keyValDocumentCollectionApi;
			}
			if (DebugSceneIndicator.IsDebugScene && mParentMenu.name.Contains("Creation") && AlternateNextMenus != null)
			{
				GameObject[] alternateNextMenus = AlternateNextMenus;
				foreach (GameObject gameObject in alternateNextMenus)
				{
					if (gameObject != null)
					{
						gameObject.SetActive(true);
					}
				}
			}
			else if (!DebugSceneIndicator.IsDebugScene && mParentMenu.name.Contains("SceneSelection") && keyValDatabaseApi.LoadUserValueAsBool("TutorialPlayed") && AlternateNextMenus != null)
			{
				GameObject[] alternateNextMenus2 = AlternateNextMenus;
				foreach (GameObject gameObject2 in alternateNextMenus2)
				{
					if (gameObject2 != null)
					{
						gameObject2.SetActive(true);
					}
				}
			}
			else if (mNextMenus != null)
			{
				for (int k = 0; k < mNextMenus.Length; k++)
				{
					if (mNextMenus[k] != null)
					{
						mNextMenus[k].SetActive(true);
					}
				}
			}
			mParentMenu.SetActive(false);
		}

		public void HideParentMenu()
		{
			if (mParentMenu != null)
			{
				mParentMenu.SetActive(false);
			}
		}
	}
}
