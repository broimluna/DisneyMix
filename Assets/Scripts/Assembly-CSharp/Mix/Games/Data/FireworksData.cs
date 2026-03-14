using System.Collections.Generic;

namespace Mix.Games.Data
{
	public class FireworksData : MixGameData
	{
		public class GestureData
		{
			public int Time;

			public string Pos;
		}

		public class Gesture
		{
			public List<int> Time;

			public List<string> Pos;

			public Gesture()
			{
				Time = new List<int>();
				Pos = new List<string>();
			}
		}

		public override string GameProcessor
		{
			get
			{
				return "fireworks";
			}
		}

		public int Scene { get; set; }

		public int Song { get; set; }

		public string Origin { get; set; }

		public string VRight { get; set; }

		public int NumFireworksLaunched { get; set; }

		public string Message { get; set; }

		public int[] FireworksLaunched { get; set; }

		public int[] TimeStamps { get; set; }

		public string[] FireworksLocations { get; set; }

		public List<Gesture> Gestures { get; set; }

		public List<FireworksResponse> Responses { get; set; }

		public FireworksData()
		{
			Responses = new List<FireworksResponse>();
		}
	}
}
