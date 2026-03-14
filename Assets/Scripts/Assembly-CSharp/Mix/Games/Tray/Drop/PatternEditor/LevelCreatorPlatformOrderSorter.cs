using System.Collections.Generic;

namespace Mix.Games.Tray.Drop.PatternEditor
{
	public class LevelCreatorPlatformOrderSorter : IComparer<LevelCreatorPlatform>
	{
		public int Compare(LevelCreatorPlatform a, LevelCreatorPlatform b)
		{
			return a.PlatformConfiguration.PathOrder - b.PlatformConfiguration.PathOrder;
		}
	}
}
