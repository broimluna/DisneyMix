using System.Collections.Generic;

namespace Mix.Games.Tray.Runner
{
	public class ChunkDifficultyComparer : IComparer<ChunkController>
	{
		public int Compare(ChunkController a, ChunkController b)
		{
			return a.difficulty - b.difficulty;
		}
	}
}
