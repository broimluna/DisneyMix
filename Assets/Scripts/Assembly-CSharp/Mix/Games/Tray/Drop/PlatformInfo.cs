using System;
using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	[Serializable]
	public class PlatformInfo
	{
		public Coordinate2D GridCoordinates;

		[Range(-1f, 3f)]
		public int Rotation;

		[Space(10f)]
		public PlatformType Type;

		[Space(10f)]
		public float EnterTime;

		public float Duration;

		[Space(10f)]
		public FenceInfo Fences;

		[Space(10f)]
		public int PathOrder;

		public Platform NextPlatform { get; set; }

		public float ExitTime
		{
			get
			{
				return EnterTime + Duration;
			}
			set
			{
				Duration = value - EnterTime;
			}
		}

		public PlatformInfo()
		{
			GridCoordinates = new Coordinate2D();
			Rotation = -1;
			Type = PlatformType.NONE;
			EnterTime = 0f;
			ExitTime = 0f;
			Fences = new FenceInfo();
		}

		public PlatformInfo(Coordinate2D position, float timeToEnter, float timeToExit, PlatformType type, int pathOrder = -1)
		{
			GridCoordinates = position;
			EnterTime = timeToEnter;
			ExitTime = timeToExit;
			Type = type;
			Rotation = -1;
			PathOrder = pathOrder;
			Fences = new FenceInfo();
		}

		public PlatformInfo(Coordinate2D position, int rotation, PlatformType type, float timeToEnter, float timeToExit, FenceInfo fences, int pathOrder)
		{
			GridCoordinates = position;
			EnterTime = timeToEnter;
			ExitTime = timeToExit;
			Type = type;
			Rotation = rotation;
			Fences = new FenceInfo(fences);
			PathOrder = pathOrder;
		}

		public PlatformInfo(PlatformInfo other)
		{
			GridCoordinates = new Coordinate2D(other.GridCoordinates);
			EnterTime = other.EnterTime;
			ExitTime = other.ExitTime;
			Type = other.Type;
			Rotation = other.Rotation;
			Fences = new FenceInfo(other.Fences);
			PathOrder = other.PathOrder;
		}

		public string GetSerialized()
		{
			string empty = string.Empty;
			empty = string.Concat(GridCoordinates.x, " ", GridCoordinates.y, " ", Rotation, " ", Type, " ", EnterTime, " ", ExitTime - EnterTime);
			if (Fences.IsAnyFenceEnabled)
			{
				empty = empty + " " + Fences.GetBits();
			}
			return empty;
		}

		public static PlatformInfo Rotate(PlatformInfo original)
		{
			float f = (float)Math.PI / 2f;
			Coordinate2D position = new Coordinate2D(Mathf.RoundToInt((float)original.GridCoordinates.x * Mathf.Cos(f) - (float)original.GridCoordinates.y * Mathf.Sin(f)), Mathf.RoundToInt((float)original.GridCoordinates.x * Mathf.Sin(f) + (float)original.GridCoordinates.y * Mathf.Cos(f)));
			int num = original.Rotation;
			if (num >= 0)
			{
				num--;
				if (num < 0)
				{
					num += 4;
				}
			}
			PlatformType rotatedPlatformType = GetRotatedPlatformType(original.Type);
			return new PlatformInfo(position, num, rotatedPlatformType, original.EnterTime, original.ExitTime, FenceInfo.Rotate(original.Fences), original.PathOrder);
		}

		public static PlatformInfo Reflect(PlatformInfo original, bool reflectX, bool reflectY)
		{
			Coordinate2D coordinate2D = new Coordinate2D(original.GridCoordinates);
			int num = original.Rotation;
			if (reflectX)
			{
				coordinate2D.x = -coordinate2D.x;
				switch (num)
				{
				case 0:
					num = 2;
					break;
				case 2:
					num = 0;
					break;
				}
			}
			if (reflectY)
			{
				coordinate2D.y = -coordinate2D.y;
				switch (num)
				{
				case 1:
					num = 3;
					break;
				case 3:
					num = 1;
					break;
				}
			}
			PlatformType reflectedPlatformType = GetReflectedPlatformType(original.Type, reflectX, reflectY);
			return new PlatformInfo(coordinate2D, num, reflectedPlatformType, original.EnterTime, original.ExitTime, FenceInfo.Reflect(original.Fences, reflectX, reflectY), original.PathOrder);
		}

		public static PlatformType GetReflectedPlatformType(PlatformType type, bool reflectX, bool reflectY)
		{
			return type;
		}

		public static PlatformType GetRotatedPlatformType(PlatformType type)
		{
			return type;
		}
	}
}
