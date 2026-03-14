using System;

namespace Mix.Games.Tray.Drop
{
	[Serializable]
	public class FenceInfo
	{
		public bool North;

		public bool South;

		public bool East;

		public bool West;

		public bool IsAnyFenceEnabled
		{
			get
			{
				return North || East || South || West;
			}
		}

		public FenceInfo()
		{
			North = false;
			East = false;
			South = false;
			West = false;
		}

		public FenceInfo(FenceInfo other)
		{
			North = other.North;
			East = other.East;
			South = other.South;
			West = other.West;
		}

		public FenceInfo(bool north, bool east, bool south, bool west)
		{
			North = north;
			East = east;
			South = south;
			West = west;
		}

		public string GetBits()
		{
			string empty = string.Empty;
			empty += ((!North) ? "0" : "1");
			empty += ((!East) ? "0" : "1");
			empty += ((!South) ? "0" : "1");
			return empty + ((!West) ? "0" : "1");
		}

		public void SetFromBits(string code)
		{
			North = code[0] == '1';
			East = code[1] == '1';
			South = code[2] == '1';
			West = code[3] == '1';
		}

		public static FenceInfo Reflect(FenceInfo original, bool reflectX, bool reflectY)
		{
			FenceInfo fenceInfo = new FenceInfo(original);
			if (reflectX)
			{
				bool west = fenceInfo.West;
				fenceInfo.West = fenceInfo.East;
				fenceInfo.East = west;
			}
			if (reflectY)
			{
				bool north = fenceInfo.North;
				fenceInfo.North = fenceInfo.South;
				fenceInfo.South = north;
			}
			return fenceInfo;
		}

		public static FenceInfo Rotate(FenceInfo original)
		{
			FenceInfo fenceInfo = new FenceInfo(original);
			bool north = fenceInfo.North;
			fenceInfo.North = fenceInfo.East;
			fenceInfo.East = fenceInfo.South;
			fenceInfo.South = fenceInfo.West;
			fenceInfo.West = north;
			return fenceInfo;
		}
	}
}
