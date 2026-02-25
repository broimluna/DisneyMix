using System.Collections.Generic;
using Mix.Games.Tray.Runner;

namespace Mix.Games.Data
{
	public class RunnerResponse : MixGameResponse
	{
		public class GhostData
		{
			public GhostDataType GhostDataType { get; set; }

			public int Ticks { get; set; }

			public string Position { get; set; }
		}

		public int Checkpoints { get; set; }

		public List<GhostData> Ghost { get; set; }

		public RunnerResponse()
		{
			Ghost = new List<GhostData>();
		}
	}
}
