using UnityEngine;

namespace Mix.Ui
{
	public class TransitionInLeft : BaseNavigationTransition
	{
		private Vector2 newStartPosition = Vector2.zero;

		private Vector2 oldEndPosition = Vector2.zero;

		private float currentTime;

		private float duration = 0.5f;

		private ScreenHolder newScreen;

		private ScreenHolder oldScreen;

		public TransitionInLeft(float aDuration = 0.5f)
		{
			duration = aDuration;
		}

		public override void Setup(ScreenHolder aOldScreen)
		{
			newScreen = null;
			oldScreen = aOldScreen;
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
			newStartPosition = new Vector2(MixConstants.CANVAS_WIDTH, 0f);
			oldEndPosition = new Vector2(0f - MixConstants.CANVAS_WIDTH, 0f);
		}

		public override bool Update()
		{
			bool result = false;
			if (newScreen == null)
			{
				return result;
			}
			newScreen.Position = Vector2.Lerp(newStartPosition, Vector2.zero, currentTime / duration);
			oldScreen.Position = Vector2.Lerp(Vector2.zero, oldEndPosition, currentTime / duration);
			currentTime += Time.deltaTime;
			if (newScreen.Position.x <= 0f)
			{
				result = true;
				newScreen.Position = Vector2.zero;
			}
			return result;
		}
	}
}
