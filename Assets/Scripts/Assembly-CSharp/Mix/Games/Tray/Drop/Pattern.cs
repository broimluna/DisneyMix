using System.Collections.Generic;
using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	public class Pattern : MonoBehaviour
	{
		public List<Platform> Platforms = new List<Platform>();

		public List<Platform> Path = new List<Platform>();

		[Space(10f)]
		public float EndTime;

		private float finishedBuffer = 1f;

		public string TemplateName { get; set; }

		public Platform StartPlatform
		{
			get
			{
				return Path[0];
			}
		}

		public Platform EndPlatform
		{
			get
			{
				return Path[Path.Count - 1];
			}
		}

		private void Awake()
		{
			Platforms.Clear();
			Path.Clear();
		}

		public void AddPlatform(Platform p, bool isCorrectPath = false)
		{
			p.transform.parent = base.transform;
			Platforms.Add(p);
			if (isCorrectPath)
			{
				Path.Add(p);
			}
		}

		public bool IsCoordinateUsedInPattern(Coordinate2D coord)
		{
			for (int i = 0; i < Platforms.Count; i++)
			{
				if (Platforms[i].Configuration.GridCoordinates == coord)
				{
					return true;
				}
			}
			return false;
		}

		public Platform GetPlatformAtCoords(Coordinate2D coord)
		{
			for (int i = 0; i < Platforms.Count; i++)
			{
				if (Platforms[i].Configuration.GridCoordinates == coord)
				{
					return Platforms[i];
				}
			}
			return null;
		}

		public bool IsFinishedAtTime(float time)
		{
			bool flag = true;
			for (int i = 0; i < Platforms.Count; i++)
			{
				flag = flag && Platforms[i].ExitTime + finishedBuffer < time;
			}
			return flag;
		}
	}
}
