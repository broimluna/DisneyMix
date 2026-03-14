using UnityEngine;

namespace Mix.Ui
{
	public class TransitionAnimations : BaseNavigationTransition
	{
		private ScreenHolder newScreen;

		private ScreenHolder oldScreen;

		private bool crossAnimate;

		private Animator oldScreenAnimator;

		private Animator newScreenAnimator;

		private string introName;

		private string outroName;

		private bool outroComplete;

		private bool introComplete;

		private bool introStarted;

		private bool introShown;

		public TransitionAnimations(bool aCrossAnimate = true, string aIntroName = "Intro", string aOutroName = "Outro")
		{
			introName = aIntroName;
			outroName = aOutroName;
			crossAnimate = aCrossAnimate;
		}

		public override void Setup(ScreenHolder aOldScreen)
		{
			newScreen = null;
			oldScreen = aOldScreen;
			newScreenAnimator = null;
			if (oldScreen == null)
			{
				outroComplete = true;
				return;
			}
			oldScreenAnimator = oldScreen.MainGameObject.GetComponent<Animator>();
			oldScreenAnimator.enabled = true;
			if (!crossAnimate)
			{
				oldScreenAnimator.Play(oldScreen.MainGameObject.name + "_" + outroName);
			}
		}

		public override void Start(ScreenHolder aNewScreen)
		{
			newScreen = aNewScreen;
			if (crossAnimate)
			{
				GameObject gameObject = newScreen.FindGameObjectByName("AvatarLight");
				if (gameObject != null && oldScreen != null)
				{
					GameObject gameObject2 = oldScreen.FindGameObjectByName("AvatarLight");
					if (gameObject2 != null)
					{
						gameObject2.SetActive(false);
					}
				}
			}
			newScreenAnimator = newScreen.MainGameObject.GetComponent<Animator>();
			newScreenAnimator.enabled = true;
			if (crossAnimate)
			{
				if (oldScreenAnimator != null)
				{
					oldScreenAnimator.Play(oldScreen.MainGameObject.name + "_" + outroName);
				}
				newScreenAnimator.Play(newScreen.MainGameObject.name + "_" + introName);
				introStarted = true;
			}
			else
			{
				newScreen.Hide();
			}
		}

		public override bool Update()
		{
			if (crossAnimate)
			{
				return CrossAnimate();
			}
			if (introStarted && !introShown)
			{
				introShown = true;
				if (oldScreen != null)
				{
					oldScreen.Hide();
				}
			}
			return ChainAnimate();
		}

		public override void OnAnimationComplete(string aAnimationState)
		{
			if (aAnimationState.Equals("IntroComplete"))
			{
				introComplete = true;
				if (oldScreen == null)
				{
					outroComplete = true;
				}
			}
			else if (aAnimationState.Equals("OutroComplete"))
			{
				outroComplete = true;
			}
		}

		private bool CrossAnimate()
		{
			if (outroComplete && introComplete)
			{
				if (oldScreenAnimator != null)
				{
					oldScreenAnimator.enabled = false;
				}
				newScreenAnimator.enabled = false;
				return true;
			}
			return false;
		}

		private bool ChainAnimate()
		{
			if (outroComplete && !introStarted)
			{
				GameObject gameObject = newScreen.FindGameObjectByName("AvatarLight");
				if (gameObject != null && oldScreen != null)
				{
					GameObject gameObject2 = oldScreen.FindGameObjectByName("AvatarLight");
					if (gameObject2 != null)
					{
						gameObject2.SetActive(false);
					}
				}
				newScreen.Show();
				newScreenAnimator.Play(newScreen.MainGameObject.name + "_" + introName);
				introStarted = true;
			}
			if (introComplete)
			{
				if (oldScreenAnimator != null)
				{
					oldScreenAnimator.enabled = false;
				}
				newScreenAnimator.enabled = false;
				return true;
			}
			return false;
		}
	}
}
