using System;
using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	[Serializable]
	public class Coordinate2D
	{
		public int x;

		public int y;

		public Coordinate2D(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public Coordinate2D()
		{
			x = 0;
			y = 0;
		}

		public Coordinate2D(Coordinate2D other)
		{
			x = other.x;
			y = other.y;
		}

		public override bool Equals(object obj)
		{
			Coordinate2D coordinate2D = obj as Coordinate2D;
			if ((object)coordinate2D == null)
			{
				return false;
			}
			return coordinate2D.x == x && coordinate2D.y == y;
		}

		public override int GetHashCode()
		{
			return x ^ y;
		}

		public override string ToString()
		{
			return "(" + x + "," + y + ")";
		}

		public static Coordinate2D operator +(Coordinate2D a, Coordinate2D b)
		{
			return new Coordinate2D(a.x + b.x, a.y + b.y);
		}

		public static Coordinate2D operator -(Coordinate2D a, Coordinate2D b)
		{
			return new Coordinate2D(a.x - b.x, a.y - b.y);
		}

		public static Vector2 operator *(Coordinate2D a, float scalar)
		{
			return new Vector2((float)a.x * scalar, (float)a.y * scalar);
		}

		public static bool operator ==(Coordinate2D a, Coordinate2D b)
		{
			if (object.ReferenceEquals(a, b))
			{
				return true;
			}
			if ((object)a == null || (object)b == null)
			{
				return false;
			}
			return a.x == b.x && a.y == b.y;
		}

		public static bool operator !=(Coordinate2D a, Coordinate2D b)
		{
			return !(a == b);
		}

		public static implicit operator Vector2(Coordinate2D a)
		{
			return new Vector2(a.x, a.y);
		}
	}
}
