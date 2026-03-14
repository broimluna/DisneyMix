using System.Collections.Generic;

namespace Mix.Games.Data
{
	public class RunnerData : MixGameData, IMixGameData
	{
		public List<string> LevelData { get; set; }

		public int Difficulty { get; set; }

		public override string GameProcessor
		{
			get
			{
				return "runner";
			}
		}

		public int GameCreatorGemCount { get; set; }

		public List<RunnerResponse> Responses { get; set; }

		public RunnerData()
		{
			Responses = new List<RunnerResponse>();
		}

		public List<MixGameResponse> GetResponses()
		{
			List<MixGameResponse> list = new List<MixGameResponse>();
			foreach (RunnerResponse response in Responses)
			{
				list.Add(response);
			}
			return list;
		}
	}
}
