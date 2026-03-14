using UnityEngine;

namespace Mix.Ui
{
	public class TransitionHudOut : BaseNavigationTransition
	{
		private float currentTime;

		private float duration = 0.5f;

		private GameObject mHeader;

		private GameObject mFooter;

		private RectTransform mHeaderRT;

		private RectTransform mFooterRT;

		private Vector2 mFooterEndPosition = Vector2.zero;

		private Vector2 mHeaderEndPosition = Vector2.zero;

		private Vector2 mFooterStartPosition = Vector2.zero;

		private Vector2 mHeaderStartPosition = Vector2.zero;

		private ScreenHolder newScreen;

		private ScreenHolder oldScreen;

		public TransitionHudOut(float aDuration = 1.5f)
		{
			duration = aDuration;
		}

		public override void Setup(ScreenHolder aOldScreen)
		{
			mHeader = GameObject.Find("header");
			mFooter = GameObject.Find("ChatBar");
			oldScreen = aOldScreen;
			mHeaderRT = mHeader.GetComponent<RectTransform>();
			mFooterRT = mFooter.GetComponent<RectTransform>();
		}

		public override void Start(ScreenHolder aNewScreen)
		{
			currentTime = 0f;
			newScreen = aNewScreen;
			GameObject gameObject = newScreen.FindGameObjectByName("AvatarLight");
			if (gameObject != null)
			{
				GameObject gameObject2 = oldScreen.FindGameObjectByName("AvatarLight");
				if (gameObject2 != null)
				{
					gameObject2.SetActive(false);
				}
			}
			mHeaderStartPosition = mHeaderRT.anchoredPosition;
			mFooterStartPosition = mFooterRT.anchoredPosition;
			mHeaderEndPosition = mHeaderStartPosition + new Vector2(0f, mHeaderRT.rect.height);
			mFooterEndPosition = mFooterStartPosition - new Vector2(0f, mFooterRT.rect.height);
		}

		public override bool Update()
		{
			bool result = false;
			if (newScreen == null)
			{
				return result;
			}
			mHeaderRT.anchoredPosition = Vector2.Lerp(mHeaderStartPosition, mHeaderEndPosition, currentTime / duration);
			mFooterRT.anchoredPosition = Vector2.Lerp(mFooterStartPosition, mFooterEndPosition, currentTime / duration);
			currentTime += Time.deltaTime;
			if (mFooterRT.anchoredPosition.y <= mFooterEndPosition.y)
			{
				result = true;
				mFooterRT.anchoredPosition = mFooterEndPosition;
			}
			return result;
		}
	}
}
