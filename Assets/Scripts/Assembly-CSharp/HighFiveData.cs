using System.Collections.Generic;
using Mix.Games.Data;

public class HighFiveData : MixGameData, IMixGameData
{
	public int RandomSeed { get; set; }

	public override string GameProcessor
	{
		get
		{
			return "highfive";
		}
	}

	public List<HighFiveResponse> Responses { get; set; }

	public HighFiveData()
	{
		Responses = new List<HighFiveResponse>();
	}

	public List<MixGameResponse> GetResponses()
	{
		List<MixGameResponse> list = new List<MixGameResponse>();
		foreach (HighFiveResponse response in Responses)
		{
			list.Add(response);
		}
		return list;
	}
}
