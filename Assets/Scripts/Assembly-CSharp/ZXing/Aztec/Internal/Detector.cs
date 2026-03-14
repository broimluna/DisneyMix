using ZXing.Common;
using ZXing.Common.Detector;
using ZXing.Common.ReedSolomon;

namespace ZXing.Aztec.Internal
{
	public sealed class Detector
	{
		internal sealed class Point
		{
			public int X { get; private set; }

			public int Y { get; private set; }

			internal Point(int x, int y)
			{
				X = x;
				Y = y;
			}

			public ResultPoint toResultPoint()
			{
				return new ResultPoint(X, Y);
			}

			public override string ToString()
			{
				return "<" + X + ' ' + Y + '>';
			}
		}

		private readonly BitMatrix image;

		private bool compact;

		private int nbLayers;

		private int nbDataBlocks;

		private int nbCenterLayers;

		private int shift;

		private static readonly int[] EXPECTED_CORNER_BITS = new int[4] { 3808, 476, 2107, 1799 };

		public Detector(BitMatrix image)
		{
			this.image = image;
		}

		public AztecDetectorResult detect()
		{
			return detect(false);
		}

		public AztecDetectorResult detect(bool isMirror)
		{
			Point matrixCenter = getMatrixCenter();
			if (matrixCenter == null)
			{
				return null;
			}
			ResultPoint[] bullsEyeCorners = getBullsEyeCorners(matrixCenter);
			if (bullsEyeCorners == null)
			{
				return null;
			}
			if (isMirror)
			{
				ResultPoint resultPoint = bullsEyeCorners[0];
				bullsEyeCorners[0] = bullsEyeCorners[2];
				bullsEyeCorners[2] = resultPoint;
			}
			if (!extractParameters(bullsEyeCorners))
			{
				return null;
			}
			BitMatrix bitMatrix = sampleGrid(image, bullsEyeCorners[shift % 4], bullsEyeCorners[(shift + 1) % 4], bullsEyeCorners[(shift + 2) % 4], bullsEyeCorners[(shift + 3) % 4]);
			if (bitMatrix == null)
			{
				return null;
			}
			ResultPoint[] matrixCornerPoints = getMatrixCornerPoints(bullsEyeCorners);
			if (matrixCornerPoints == null)
			{
				return null;
			}
			return new AztecDetectorResult(bitMatrix, matrixCornerPoints, compact, nbDataBlocks, nbLayers);
		}

		private bool extractParameters(ResultPoint[] bullsEyeCorners)
		{
			if (!isValid(bullsEyeCorners[0]) || !isValid(bullsEyeCorners[1]) || !isValid(bullsEyeCorners[2]) || !isValid(bullsEyeCorners[3]))
			{
				return false;
			}
			int num = 2 * nbCenterLayers;
			int[] array = new int[4]
			{
				sampleLine(bullsEyeCorners[0], bullsEyeCorners[1], num),
				sampleLine(bullsEyeCorners[1], bullsEyeCorners[2], num),
				sampleLine(bullsEyeCorners[2], bullsEyeCorners[3], num),
				sampleLine(bullsEyeCorners[3], bullsEyeCorners[0], num)
			};
			shift = getRotation(array, num);
			if (shift < 0)
			{
				return false;
			}
			long num2 = 0L;
			for (int i = 0; i < 4; i++)
			{
				int num3 = array[(shift + i) % 4];
				if (compact)
				{
					num2 <<= 7;
					num2 += (num3 >> 1) & 0x7F;
				}
				else
				{
					num2 <<= 10;
					num2 += ((num3 >> 2) & 0x3E0) + ((num3 >> 1) & 0x1F);
				}
			}
			int correctedParameterData = getCorrectedParameterData(num2, compact);
			if (correctedParameterData < 0)
			{
				return false;
			}
			if (compact)
			{
				nbLayers = (correctedParameterData >> 6) + 1;
				nbDataBlocks = (correctedParameterData & 0x3F) + 1;
			}
			else
			{
				nbLayers = (correctedParameterData >> 11) + 1;
				nbDataBlocks = (correctedParameterData & 0x7FF) + 1;
			}
			return true;
		}

		private static int getRotation(int[] sides, int length)
		{
			int num = 0;
			foreach (int num2 in sides)
			{
				int num3 = (num2 >> length - 2 << 1) + (num2 & 1);
				num = (num << 3) + num3;
			}
			num = ((num & 1) << 11) + (num >> 1);
			for (int j = 0; j < 4; j++)
			{
				if (SupportClass.bitCount(num ^ EXPECTED_CORNER_BITS[j]) <= 2)
				{
					return j;
				}
			}
			return -1;
		}

		private static int getCorrectedParameterData(long parameterData, bool compact)
		{
			int num;
			int num2;
			if (compact)
			{
				num = 7;
				num2 = 2;
			}
			else
			{
				num = 10;
				num2 = 4;
			}
			int twoS = num - num2;
			int[] array = new int[num];
			for (int num3 = num - 1; num3 >= 0; num3--)
			{
				array[num3] = (int)parameterData & 0xF;
				parameterData >>= 4;
			}
			ReedSolomonDecoder reedSolomonDecoder = new ReedSolomonDecoder(GenericGF.AZTEC_PARAM);
			if (!reedSolomonDecoder.decode(array, twoS))
			{
				return -1;
			}
			int num4 = 0;
			for (int i = 0; i < num2; i++)
			{
				num4 = (num4 << 4) + array[i];
			}
			return num4;
		}

		private ResultPoint[] getBullsEyeCorners(Point pCenter)
		{
			Point point = pCenter;
			Point point2 = pCenter;
			Point point3 = pCenter;
			Point point4 = pCenter;
			bool flag = true;
			for (nbCenterLayers = 1; nbCenterLayers < 9; nbCenterLayers++)
			{
				Point firstDifferent = getFirstDifferent(point, flag, 1, -1);
				Point firstDifferent2 = getFirstDifferent(point2, flag, 1, 1);
				Point firstDifferent3 = getFirstDifferent(point3, flag, -1, 1);
				Point firstDifferent4 = getFirstDifferent(point4, flag, -1, -1);
				if (nbCenterLayers > 2)
				{
					float num = distance(firstDifferent4, firstDifferent) * (float)nbCenterLayers / (distance(point4, point) * (float)(nbCenterLayers + 2));
					if ((double)num < 0.75 || (double)num > 1.25 || !isWhiteOrBlackRectangle(firstDifferent, firstDifferent2, firstDifferent3, firstDifferent4))
					{
						break;
					}
				}
				point = firstDifferent;
				point2 = firstDifferent2;
				point3 = firstDifferent3;
				point4 = firstDifferent4;
				flag = !flag;
			}
			if (nbCenterLayers != 5 && nbCenterLayers != 7)
			{
				return null;
			}
			compact = nbCenterLayers == 5;
			ResultPoint resultPoint = new ResultPoint((float)point.X + 0.5f, (float)point.Y - 0.5f);
			ResultPoint resultPoint2 = new ResultPoint((float)point2.X + 0.5f, (float)point2.Y + 0.5f);
			ResultPoint resultPoint3 = new ResultPoint((float)point3.X - 0.5f, (float)point3.Y + 0.5f);
			ResultPoint resultPoint4 = new ResultPoint((float)point4.X - 0.5f, (float)point4.Y - 0.5f);
			return expandSquare(new ResultPoint[4] { resultPoint, resultPoint2, resultPoint3, resultPoint4 }, 2 * nbCenterLayers - 3, 2 * nbCenterLayers);
		}

		private Point getMatrixCenter()
		{
			WhiteRectangleDetector whiteRectangleDetector = WhiteRectangleDetector.Create(image);
			if (whiteRectangleDetector == null)
			{
				return null;
			}
			ResultPoint[] array = whiteRectangleDetector.detect();
			ResultPoint resultPoint;
			ResultPoint resultPoint2;
			ResultPoint resultPoint3;
			ResultPoint resultPoint4;
			int num;
			int num2;
			if (array != null)
			{
				resultPoint = array[0];
				resultPoint2 = array[1];
				resultPoint3 = array[2];
				resultPoint4 = array[3];
			}
			else
			{
				num = image.Width / 2;
				num2 = image.Height / 2;
				resultPoint = getFirstDifferent(new Point(num + 7, num2 - 7), false, 1, -1).toResultPoint();
				resultPoint2 = getFirstDifferent(new Point(num + 7, num2 + 7), false, 1, 1).toResultPoint();
				resultPoint3 = getFirstDifferent(new Point(num - 7, num2 + 7), false, -1, 1).toResultPoint();
				resultPoint4 = getFirstDifferent(new Point(num - 7, num2 - 7), false, -1, -1).toResultPoint();
			}
			num = MathUtils.round((resultPoint.X + resultPoint4.X + resultPoint2.X + resultPoint3.X) / 4f);
			num2 = MathUtils.round((resultPoint.Y + resultPoint4.Y + resultPoint2.Y + resultPoint3.Y) / 4f);
			whiteRectangleDetector = WhiteRectangleDetector.Create(image, 15, num, num2);
			if (whiteRectangleDetector == null)
			{
				return null;
			}
			array = whiteRectangleDetector.detect();
			if (array != null)
			{
				resultPoint = array[0];
				resultPoint2 = array[1];
				resultPoint3 = array[2];
				resultPoint4 = array[3];
			}
			else
			{
				resultPoint = getFirstDifferent(new Point(num + 7, num2 - 7), false, 1, -1).toResultPoint();
				resultPoint2 = getFirstDifferent(new Point(num + 7, num2 + 7), false, 1, 1).toResultPoint();
				resultPoint3 = getFirstDifferent(new Point(num - 7, num2 + 7), false, -1, 1).toResultPoint();
				resultPoint4 = getFirstDifferent(new Point(num - 7, num2 - 7), false, -1, -1).toResultPoint();
			}
			num = MathUtils.round((resultPoint.X + resultPoint4.X + resultPoint2.X + resultPoint3.X) / 4f);
			num2 = MathUtils.round((resultPoint.Y + resultPoint4.Y + resultPoint2.Y + resultPoint3.Y) / 4f);
			return new Point(num, num2);
		}

		private ResultPoint[] getMatrixCornerPoints(ResultPoint[] bullsEyeCorners)
		{
			return expandSquare(bullsEyeCorners, 2 * nbCenterLayers, getDimension());
		}

		private BitMatrix sampleGrid(BitMatrix image, ResultPoint topLeft, ResultPoint topRight, ResultPoint bottomRight, ResultPoint bottomLeft)
		{
			GridSampler instance = GridSampler.Instance;
			int dimension = getDimension();
			float num = (float)dimension / 2f - (float)nbCenterLayers;
			float num2 = (float)dimension / 2f + (float)nbCenterLayers;
			return instance.sampleGrid(image, dimension, dimension, num, num, num2, num, num2, num2, num, num2, topLeft.X, topLeft.Y, topRight.X, topRight.Y, bottomRight.X, bottomRight.Y, bottomLeft.X, bottomLeft.Y);
		}

		private int sampleLine(ResultPoint p1, ResultPoint p2, int size)
		{
			int num = 0;
			float num2 = distance(p1, p2);
			float num3 = num2 / (float)size;
			float x = p1.X;
			float y = p1.Y;
			float num4 = num3 * (p2.X - p1.X) / num2;
			float num5 = num3 * (p2.Y - p1.Y) / num2;
			for (int i = 0; i < size; i++)
			{
				if (image[MathUtils.round(x + (float)i * num4), MathUtils.round(y + (float)i * num5)])
				{
					num |= 1 << ((size - i - 1) & 0x1F);
				}
			}
			return num;
		}

		private bool isWhiteOrBlackRectangle(Point p1, Point p2, Point p3, Point p4)
		{
			p1 = new Point(p1.X - 3, p1.Y + 3);
			p2 = new Point(p2.X - 3, p2.Y - 3);
			p3 = new Point(p3.X + 3, p3.Y - 3);
			p4 = new Point(p4.X + 3, p4.Y + 3);
			int color = getColor(p4, p1);
			if (color == 0)
			{
				return false;
			}
			int color2 = getColor(p1, p2);
			if (color2 != color)
			{
				return false;
			}
			color2 = getColor(p2, p3);
			if (color2 != color)
			{
				return false;
			}
			color2 = getColor(p3, p4);
			return color2 == color;
		}

		private int getColor(Point p1, Point p2)
		{
			float num = distance(p1, p2);
			float num2 = (float)(p2.X - p1.X) / num;
			float num3 = (float)(p2.Y - p1.Y) / num;
			int num4 = 0;
			float num5 = p1.X;
			float num6 = p1.Y;
			bool flag = image[p1.X, p1.Y];
			for (int i = 0; (float)i < num; i++)
			{
				num5 += num2;
				num6 += num3;
				if (image[MathUtils.round(num5), MathUtils.round(num6)] != flag)
				{
					num4++;
				}
			}
			float num7 = (float)num4 / num;
			if (num7 > 0.1f && num7 < 0.9f)
			{
				return 0;
			}
			return (num7 <= 0.1f == flag) ? 1 : (-1);
		}

		private Point getFirstDifferent(Point init, bool color, int dx, int dy)
		{
			int num = init.X + dx;
			int i;
			for (i = init.Y + dy; isValid(num, i) && image[num, i] == color; i += dy)
			{
				num += dx;
			}
			num -= dx;
			for (i -= dy; isValid(num, i) && image[num, i] == color; num += dx)
			{
			}
			for (num -= dx; isValid(num, i) && image[num, i] == color; i += dy)
			{
			}
			i -= dy;
			return new Point(num, i);
		}

		private static ResultPoint[] expandSquare(ResultPoint[] cornerPoints, float oldSide, float newSide)
		{
			float num = newSide / (2f * oldSide);
			float num2 = cornerPoints[0].X - cornerPoints[2].X;
			float num3 = cornerPoints[0].Y - cornerPoints[2].Y;
			float num4 = (cornerPoints[0].X + cornerPoints[2].X) / 2f;
			float num5 = (cornerPoints[0].Y + cornerPoints[2].Y) / 2f;
			ResultPoint resultPoint = new ResultPoint(num4 + num * num2, num5 + num * num3);
			ResultPoint resultPoint2 = new ResultPoint(num4 - num * num2, num5 - num * num3);
			num2 = cornerPoints[1].X - cornerPoints[3].X;
			num3 = cornerPoints[1].Y - cornerPoints[3].Y;
			num4 = (cornerPoints[1].X + cornerPoints[3].X) / 2f;
			num5 = (cornerPoints[1].Y + cornerPoints[3].Y) / 2f;
			ResultPoint resultPoint3 = new ResultPoint(num4 + num * num2, num5 + num * num3);
			ResultPoint resultPoint4 = new ResultPoint(num4 - num * num2, num5 - num * num3);
			return new ResultPoint[4] { resultPoint, resultPoint3, resultPoint2, resultPoint4 };
		}

		private bool isValid(int x, int y)
		{
			return x >= 0 && x < image.Width && y > 0 && y < image.Height;
		}

		private bool isValid(ResultPoint point)
		{
			int x = MathUtils.round(point.X);
			int y = MathUtils.round(point.Y);
			return isValid(x, y);
		}

		private static float distance(Point a, Point b)
		{
			return MathUtils.distance(a.X, a.Y, b.X, b.Y);
		}

		private static float distance(ResultPoint a, ResultPoint b)
		{
			return MathUtils.distance(a.X, a.Y, b.X, b.Y);
		}

		private int getDimension()
		{
			if (compact)
			{
				return 4 * nbLayers + 11;
			}
			if (nbLayers <= 4)
			{
				return 4 * nbLayers + 15;
			}
			return 4 * nbLayers + 2 * ((nbLayers - 4) / 8 + 1) + 15;
		}
	}
}
