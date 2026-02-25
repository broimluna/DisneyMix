using System.Collections.Generic;

namespace Mix.Games.Tray.Runner
{
	public class RunnerRecorderReverseComparer : IComparer<RunnerRecorder>
	{
		public int Compare(RunnerRecorder a, RunnerRecorder b)
		{
			return b.score - a.score;
		}
	}
}
