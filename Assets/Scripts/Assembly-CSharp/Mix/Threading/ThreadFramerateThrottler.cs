namespace Mix.Threading
{
	public class ThreadFramerateThrottler : Singleton<ThreadFramerateThrottler>
	{
		private const int FRAMERATE_THROTTLE_FPS = 20;

		private int numThreads;

		private bool isThrottled;

		private bool preventThrottling;

		public void EnterThrottlingSection()
		{
			numThreads++;
			CheckForThrottling();
		}

		public void ExitThrottlingSection()
		{
			numThreads--;
			CheckForThrottling();
		}

		public void PreventThrottling()
		{
			preventThrottling = true;
			CheckForThrottling();
		}

		public void AllowThrottling()
		{
			preventThrottling = false;
			CheckForThrottling();
		}

		private void CheckForThrottling()
		{
			if (!Util.IsLowEndDevice())
			{
				return;
			}
			if (isThrottled && (preventThrottling || numThreads == 0))
			{
				isThrottled = false;
			}
			else
			{
				if (isThrottled || preventThrottling || numThreads <= 0)
				{
					return;
				}
				isThrottled = true;
			}
			if (isThrottled)
			{
				Singleton<SettingsManager>.Instance.SetTargetFramerate(20);
			}
			else
			{
				Singleton<SettingsManager>.Instance.SetTargetFramerate(Singleton<SettingsManager>.Instance.GetDefaultFPS());
			}
		}
	}
}
