namespace Mix.Ui
{
	public class BaseNavigationTransition
	{
		public virtual void Setup(ScreenHolder aOldScreen)
		{
		}

		public virtual void Start(ScreenHolder aNewScreen)
		{
		}

		public virtual bool Update()
		{
			return true;
		}

		public virtual void OnAnimationComplete(string aAnimationState)
		{
		}
	}
}
