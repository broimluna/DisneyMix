using System.Collections.Generic;
using Mix.Games.Data;

namespace Mix.Games.Tray.Friendzy
{
	public class FriendzyData : MixGameData, IMixGameData
	{
		public override string GameProcessor
		{
			get
			{
				return "quizpals";
			}
		}

		public string Category { get; set; }

		public string Quiz { get; set; }

		public string QuizTitle { get; set; }

		public List<FriendzyResponse> Responses { get; set; }

		public FriendzyData()
		{
			Responses = new List<FriendzyResponse>();
		}

		public List<MixGameResponse> GetResponses()
		{
			List<MixGameResponse> list = new List<MixGameResponse>();
			foreach (FriendzyResponse response in Responses)
			{
				list.Add(response);
			}
			return list;
		}
	}
}
