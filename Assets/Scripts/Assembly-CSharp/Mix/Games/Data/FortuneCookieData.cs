using System.Collections.Generic;

namespace Mix.Games.Data
{
	public class FortuneCookieData : MixGameData, IMixGameData
	{
		public string Fortune { get; set; }

		public List<FortuneCookieResponse> Responses { get; set; }

		public override string GameProcessor
		{
			get
			{
				return "dailyfortune";
			}
		}

		public FortuneCookieData()
		{
			Responses = new List<FortuneCookieResponse>();
		}

		public List<MixGameResponse> GetResponses()
		{
			List<MixGameResponse> list = new List<MixGameResponse>();
			foreach (FortuneCookieResponse response in Responses)
			{
				list.Add(response);
			}
			return list;
		}
	}
}
