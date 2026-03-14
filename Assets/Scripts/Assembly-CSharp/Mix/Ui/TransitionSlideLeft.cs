using UnityEngine;

namespace Mix.Ui
{
	public class TransitionSlideLeft : BaseNavigationTransition
	{
		private Vector2 oldEndPosition = Vector2.zero;

		private float currentTime;

		private float speed;

		private ScreenHolder newScreen;

		private ScreenHolder oldScreen;

		private GameObject newScreenBottomNav;

		private GameObject oldScreenBottomNav;

		private float change;

		public TransitionSlideLeft(float aSpeed = 10f)
		{
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
			if (oldScreenBottomNav != null && newScreenBottomNav != null)
			{
				oldScreenBottomNav.SetActive(false);
			}
			GameObject gameObject = newScreen.FindGameObjectByName("AvatarLight");
			if (gameObject != null)
			{
				GameObject gameObject2 = oldScreen.FindGameObjectByName("AvatarLight");
				if (gameObject2 != null)
				{
					gameObject2.SetActive(false);
				}
			}
			newScreen.Position = new Vector2(MixConstants.CANVAS_WIDTH, 0f);
			oldEndPosition = new Vector2(0f - MixConstants.CANVAS_WIDTH, 0f);
		}

		public override bool Update()
		{
			bool result = false;
			if (newScreen == null)
			{
				return result;
			}
			float x = oldScreen.Position.x;
			newScreen.Position = Util.Vector2Update(newScreen.Position, Vector2.zero, speed);
			oldScreen.Position = Util.Vector2Update(oldScreen.Position, oldEndPosition, speed);
			change += oldScreen.Position.x - x;
			if (newScreenBottomNav != null && oldScreenBottomNav != null)
			{
				newScreenBottomNav.GetComponent<RectTransform>().anchoredPosition = new Vector2(oldScreenBottomNav.GetComponent<RectTransform>().anchoredPosition.x - change - MixConstants.CANVAS_WIDTH, oldScreenBottomNav.GetComponent<RectTransform>().anchoredPosition.y);
			}
			currentTime += Time.deltaTime;
			if (newScreen.Position.x <= 5f)
			{
				result = true;
				newScreen.Position = Vector2.zero;
				if (newScreenBottomNav != null)
				{
					if (oldScreenBottomNav != null)
					{
						oldScreenBottomNav.SetActive(true);
					}
					newScreenBottomNav.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, newScreenBottomNav.GetComponent<RectTransform>().anchoredPosition.y);
				}
			}
			return result;
		}
	}
}
