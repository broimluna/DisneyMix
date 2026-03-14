using System.Collections.Generic;

namespace Mix.Games.Data
{
	public class SecretWordData : MixGameData, IMixGameData
	{
		public override string GameProcessor
		{
			get
			{
				return "secretword";
			}
		}

		public string Hint { get; set; }

		public string Word { get; set; }

		public List<SecretWordResponse> Responses { get; set; }

		public SecretWordData()
		{
			Responses = new List<SecretWordResponse>();
		}

		public List<MixGameResponse> GetResponses()
		{
			List<MixGameResponse> list = new List<MixGameResponse>();
			foreach (SecretWordResponse response in Responses)
			{
				list.Add(response);
			}
			return list;
		}
	}
}
