using System.Collections.Generic;

namespace Mix.Games.Tray.Drop.PatternEditor
{
	public class PlatformInfoOrderSorter : IComparer<PlatformInfo>
	{
		public int Compare(PlatformInfo a, PlatformInfo b)
		{
			return a.PathOrder - b.PathOrder;
		}
	}
}
