using System.Collections.Generic;

namespace Mix.Games.Data
{
	public class TemplateGameData : MixGameData
	{
		public string TestString { get; set; }

		public List<TemplateGameResponse> Responses { get; set; }

		public TemplateGameData()
		{
			Responses = new List<TemplateGameResponse>();
		}
	}
}
