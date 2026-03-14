using System.Collections.Generic;

namespace Mix.Games.Tray.MemeGenerator
{
	public class MemeGeneratorDataSorter : IComparer<Meme_Generator_Data>
	{
		public int Compare(Meme_Generator_Data a, Meme_Generator_Data b)
		{
			if (a.order == b.order)
			{
				return a.uid.CompareTo(b.uid);
			}
			return a.order - b.order;
		}
	}
}
