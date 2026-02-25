using System;
using System.Collections.Generic;
using Mix.Games.Data;

namespace Mix.Games.Tray.Runner
{
	[Serializable]
	public class RunnerRecorder
	{
		public List<RunnerGhostData> ghostData;

		public int score;

		public string playerId;

		public RunnerRecorder()
		{
			ghostData = new List<RunnerGhostData>();
		}

		public void AddData(RunnerGhostData aData)
		{
			ghostData.Add(aData);
		}

		public RunnerGhostData GetByIndex(int index)
		{
			return ghostData[index];
		}

		public int GetLength()
		{
			return ghostData.Count;
		}

		public List<RunnerResponse.GhostData> Formatted()
		{
			List<RunnerResponse.GhostData> list = new List<RunnerResponse.GhostData>();
			foreach (RunnerGhostData ghostDatum in this.ghostData)
			{
				RunnerResponse.GhostData ghostData = new RunnerResponse.GhostData();
				ghostData.GhostDataType = ghostDatum.type;
				ghostData.Ticks = ghostDatum.ticks;
				ghostData.Position = ghostDatum.position.ToString();
				list.Add(ghostData);
			}
			return list;
		}

		public void IngestFormattedData(List<RunnerResponse.GhostData> aGhostPlayer)
		{
			foreach (RunnerResponse.GhostData item in aGhostPlayer)
			{
				RunnerGhostData runnerGhostData = new RunnerGhostData();
				runnerGhostData.IngestFormattedData(item);
				AddData(runnerGhostData);
			}
		}
	}
}
