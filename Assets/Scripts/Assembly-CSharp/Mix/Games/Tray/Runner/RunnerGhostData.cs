using System;
using System.Collections.Generic;
using Mix.Games.Data;
using UnityEngine;

namespace Mix.Games.Tray.Runner
{
	[Serializable]
	public class RunnerGhostData
	{
		public GhostDataType type;

		public int ticks;

		public Vector3 position;

		public RunnerGhostData(GhostDataType aType, int aTicks, Vector3 aPosition)
		{
			type = aType;
			ticks = aTicks;
			position = aPosition;
		}

		public RunnerGhostData()
		{
		}

		public Dictionary<string, object> Formatted()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["type"] = type;
			dictionary["ticks"] = ticks;
			dictionary["position"] = position.ToString();
			return dictionary;
		}

		public void IngestFormattedData(RunnerResponse.GhostData aGhostData)
		{
			type = aGhostData.GhostDataType;
			ticks = aGhostData.Ticks;
			position = StringToVector3(aGhostData.Position);
		}

		public Vector3 StringToVector3(string rString)
		{
			string[] array = rString.Substring(1, rString.Length - 2).Split(',');
			float x = float.Parse(array[0]);
			float y = float.Parse(array[1]);
			float z = float.Parse(array[2]);
			return new Vector3(x, y, z);
		}
	}
}
