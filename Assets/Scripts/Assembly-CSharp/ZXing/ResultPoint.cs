using System;
using System.Globalization;
using System.Text;
using ZXing.Common.Detector;

namespace ZXing
{
	public class ResultPoint
	{
		private readonly float x;

		private readonly float y;

		private readonly byte[] bytesX;

		private readonly byte[] bytesY;

		private string toString;

		public virtual float X
		{
			get
			{
				return x;
			}
		}

		public virtual float Y
		{
			get
			{
				return y;
			}
		}

		public ResultPoint()
		{
		}

		public ResultPoint(float x, float y)
		{
			this.x = x;
			this.y = y;
			bytesX = BitConverter.GetBytes(x);
			bytesY = BitConverter.GetBytes(y);
		}

		public override bool Equals(object other)
		{
			ResultPoint resultPoint = other as ResultPoint;
			if (resultPoint == null)
			{
				return false;
			}
			return x == resultPoint.x && y == resultPoint.y;
		}

		public override int GetHashCode()
		{
			return 31 * ((bytesX[0] << 24) + (bytesX[1] << 16) + (bytesX[2] << 8) + bytesX[3]) + (bytesY[0] << 24) + (bytesY[1] << 16) + (bytesY[2] << 8) + bytesY[3];
		}

		public override string ToString()
		{
			if (toString == null)
			{
				StringBuilder stringBuilder = new StringBuilder(25);
				stringBuilder.AppendFormat(CultureInfo.CurrentUICulture, "({0}, {1})", x, y);
				toString = stringBuilder.ToString();
			}
			return toString;
		}

		public static void orderBestPatterns(ResultPoint[] patterns)
		{
			float num = distance(patterns[0], patterns[1]);
			float num2 = distance(patterns[1], patterns[2]);
			float num3 = distance(patterns[0], patterns[2]);
			ResultPoint resultPoint;
			ResultPoint resultPoint2;
			ResultPoint resultPoint3;
			if (num2 >= num && num2 >= num3)
			{
				resultPoint = patterns[0];
				resultPoint2 = patterns[1];
				resultPoint3 = patterns[2];
			}
			else if (num3 >= num2 && num3 >= num)
			{
				resultPoint = patterns[1];
				resultPoint2 = patterns[0];
				resultPoint3 = patterns[2];
			}
			else
			{
				resultPoint = patterns[2];
				resultPoint2 = patterns[0];
				resultPoint3 = patterns[1];
			}
			if (crossProductZ(resultPoint2, resultPoint, resultPoint3) < 0f)
			{
				ResultPoint resultPoint4 = resultPoint2;
				resultPoint2 = resultPoint3;
				resultPoint3 = resultPoint4;
			}
			patterns[0] = resultPoint2;
			patterns[1] = resultPoint;
			patterns[2] = resultPoint3;
		}

		public static float distance(ResultPoint pattern1, ResultPoint pattern2)
		{
			return MathUtils.distance(pattern1.x, pattern1.y, pattern2.x, pattern2.y);
		}

		private static float crossProductZ(ResultPoint pointA, ResultPoint pointB, ResultPoint pointC)
		{
			float num = pointB.x;
			float num2 = pointB.y;
			return (pointC.x - num) * (pointA.y - num2) - (pointC.y - num2) * (pointA.x - num);
		}
	}
}
