using UnityEngine;

namespace Mix.Ui
{
	public class TransitionSlideUp : BaseNavigationTransition
	{
		private Vector2 oldEndPosition = Vector2.zero;

		private float currentTime;

		private float speed;

		private ScreenHolder newScreen;

		private ScreenHolder oldScreen;

		private GameObject newScreenBottomNav;

		private GameObject oldScreenBottomNav;

		private float change;

		private bool lockCurrent;

		public TransitionSlideUp(bool aLockCurrent = false, float aSpeed = 10f)
		{
			lockCurrent = aLockCurrent;
			speed = aSpeed;
		}

		public override void Setup(ScreenHolder aOldScreen)
		{
			newScreen = null;
			oldScreen = aOldScreen;
			oldScreenBottomNav = oldScreen.FindGameObjectByName("BottomNav");
		}

		public override void Start(ScreenHolder aNewScreen)
		{
			currentTime = 0f;
			newScreen = aNewScreen;
			newScreenBottomNav = newScreen.FindGameObjectByName("BottomNav");
			GameObject gameObject = newScreen.FindGameObjectByName("AvatarLight");
			if (gameObject != null)
			{
				GameObject gameObject2 = oldScreen.FindGameObjectByName("AvatarLight");
				if (gameObject2 != null)
				{
					gameObject2.SetActive(false);
				}
			}
			newScreen.Position = new Vector2(0f, 0f - MixConstants.CANVAS_HEIGHT);
			oldEndPosition = new Vector2(0f, MixConstants.CANVAS_HEIGHT);
			if (oldScreenBottomNav != null)
			{
				oldScreenBottomNav.SetActive(false);
			}
		}

		public override bool Update()
		{
			bool result = false;
			if (newScreen == null)
			{
				return result;
			}
			float y = oldScreen.Position.y;
			newScreen.Position = Util.Vector2Update(newScreen.Position, Vector2.zero, speed);
			if (!lockCurrent)
			{
				oldScreen.Position = Util.Vector2Update(oldScreen.Position, oldEndPosition, speed);
			}
			change += newScreen.Position.y - y;
			if (newScreenBottomNav != null && oldScreenBottomNav != null)
			{
				newScreenBottomNav.GetComponent<RectTransform>().anchoredPosition = new Vector2(oldScreenBottomNav.GetComponent<RectTransform>().anchoredPosition.x, oldScreenBottomNav.GetComponent<RectTransform>().anchoredPosition.y - change + MixConstants.CANVAS_HEIGHT);
			}
			currentTime += Time.deltaTime;
			if (newScreen.Position.y >= -5f)
			{
				result = true;
				newScreen.Position = Vector2.zero;
				if (newScreenBottomNav != null)
				{
					newScreenBottomNav.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, newScreenBottomNav.GetComponent<RectTransform>().anchoredPosition.y);
				}
			}
			return result;
		}
	}
}
