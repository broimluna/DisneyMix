using System.Collections.Generic;
using Mix.Games.Data;

namespace Mix.Games.Tray.Drop
{
	public class DropData : MixGameData, IMixGameData
	{
		public override string GameProcessor
		{
			get
			{
				return "winterwaltz";
			}
		}

		public int LevelGenerationSeed { get; set; }

		public List<DropResponse> Responses { get; set; }

		public DropData()
		{
			Responses = new List<DropResponse>();
			LevelGenerationSeed = 0;
		}

		public List<MixGameResponse> GetResponses()
		{
			List<MixGameResponse> list = new List<MixGameResponse>();
			foreach (DropResponse response in Responses)
			{
				list.Add(response);
			}
			return list;
		}
	}
}
