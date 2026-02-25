using System.Collections.Generic;

namespace Mix.Games.Data
{
	public class WouldYouRatherData : MixGameData, IMixGameData
	{
		public class WouldYouRatherOption
		{
			public string Caption { get; set; }

			public string ImageUrl { get; set; }
		}

		public override string GameProcessor
		{
			get
			{
				return "wouldyourather";
			}
		}

		public WouldYouRatherOption Option1 { get; set; }

		public WouldYouRatherOption Option2 { get; set; }

		public List<WouldYouRatherResponse> Responses { get; set; }

		public WouldYouRatherData()
		{
			Option1 = new WouldYouRatherOption();
			Option2 = new WouldYouRatherOption();
			Responses = new List<WouldYouRatherResponse>();
		}

		public List<MixGameResponse> GetResponses()
		{
			List<MixGameResponse> list = new List<MixGameResponse>();
			foreach (WouldYouRatherResponse response in Responses)
			{
				list.Add(response);
			}
			return list;
		}
	}
}
