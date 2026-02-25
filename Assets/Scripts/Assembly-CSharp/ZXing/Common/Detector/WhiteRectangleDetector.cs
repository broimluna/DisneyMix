namespace ZXing.Common.Detector
{
	public sealed class WhiteRectangleDetector
	{
		private const int INIT_SIZE = 10;

		private const int CORR = 1;

		private readonly BitMatrix image;

		private readonly int height;

		private readonly int width;

		private readonly int leftInit;

		private readonly int rightInit;

		private readonly int downInit;

		private readonly int upInit;

		internal WhiteRectangleDetector(BitMatrix image)
			: this(image, 10, image.Width / 2, image.Height / 2)
		{
		}

		internal WhiteRectangleDetector(BitMatrix image, int initSize, int x, int y)
		{
			this.image = image;
			height = image.Height;
			width = image.Width;
			int num = initSize / 2;
			leftInit = x - num;
			rightInit = x + num;
			upInit = y - num;
			downInit = y + num;
		}

		public static WhiteRectangleDetector Create(BitMatrix image)
		{
			if (image == null)
			{
				return null;
			}
			WhiteRectangleDetector whiteRectangleDetector = new WhiteRectangleDetector(image);
			if (whiteRectangleDetector.upInit < 0 || whiteRectangleDetector.leftInit < 0 || whiteRectangleDetector.downInit >= whiteRectangleDetector.height || whiteRectangleDetector.rightInit >= whiteRectangleDetector.width)
			{
				return null;
			}
			return whiteRectangleDetector;
		}

		public static WhiteRectangleDetector Create(BitMatrix image, int initSize, int x, int y)
		{
			WhiteRectangleDetector whiteRectangleDetector = new WhiteRectangleDetector(image, initSize, x, y);
			if (whiteRectangleDetector.upInit < 0 || whiteRectangleDetector.leftInit < 0 || whiteRectangleDetector.downInit >= whiteRectangleDetector.height || whiteRectangleDetector.rightInit >= whiteRectangleDetector.width)
			{
				return null;
			}
			return whiteRectangleDetector;
		}

		public ResultPoint[] detect()
		{
			int num = leftInit;
			int num2 = rightInit;
			int num3 = upInit;
			int num4 = downInit;
			bool flag = false;
			bool flag2 = true;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			while (flag2)
			{
				flag2 = false;
				bool flag8 = true;
				while ((flag8 || !flag4) && num2 < width)
				{
					flag8 = containsBlackPoint(num3, num4, num2, false);
					if (flag8)
					{
						num2++;
						flag2 = true;
						flag4 = true;
					}
					else if (!flag4)
					{
						num2++;
					}
				}
				if (num2 >= width)
				{
					flag = true;
					break;
				}
				bool flag9 = true;
				while ((flag9 || !flag5) && num4 < height)
				{
					flag9 = containsBlackPoint(num, num2, num4, true);
					if (flag9)
					{
						num4++;
						flag2 = true;
						flag5 = true;
					}
					else if (!flag5)
					{
						num4++;
					}
				}
				if (num4 >= height)
				{
					flag = true;
					break;
				}
				bool flag10 = true;
				while ((flag10 || !flag6) && num >= 0)
				{
					flag10 = containsBlackPoint(num3, num4, num, false);
					if (flag10)
					{
						num--;
						flag2 = true;
						flag6 = true;
					}
					else if (!flag6)
					{
						num--;
					}
				}
				if (num < 0)
				{
					flag = true;
					break;
				}
				bool flag11 = true;
				while ((flag11 || !flag7) && num3 >= 0)
				{
					flag11 = containsBlackPoint(num, num2, num3, true);
					if (flag11)
					{
						num3--;
						flag2 = true;
						flag7 = true;
					}
					else if (!flag7)
					{
						num3--;
					}
				}
				if (num3 < 0)
				{
					flag = true;
					break;
				}
				if (flag2)
				{
					flag3 = true;
				}
			}
			if (!flag && flag3)
			{
				int num5 = num2 - num;
				ResultPoint resultPoint = null;
				for (int i = 1; i < num5; i++)
				{
					resultPoint = getBlackPointOnSegment(num, num4 - i, num + i, num4);
					if (resultPoint != null)
					{
						break;
					}
				}
				if (resultPoint == null)
				{
					return null;
				}
				ResultPoint resultPoint2 = null;
				for (int j = 1; j < num5; j++)
				{
					resultPoint2 = getBlackPointOnSegment(num, num3 + j, num + j, num3);
					if (resultPoint2 != null)
					{
						break;
					}
				}
				if (resultPoint2 == null)
				{
					return null;
				}
				ResultPoint resultPoint3 = null;
				for (int k = 1; k < num5; k++)
				{
					resultPoint3 = getBlackPointOnSegment(num2, num3 + k, num2 - k, num3);
					if (resultPoint3 != null)
					{
						break;
					}
				}
				if (resultPoint3 == null)
				{
					return null;
				}
				ResultPoint resultPoint4 = null;
				for (int l = 1; l < num5; l++)
				{
					resultPoint4 = getBlackPointOnSegment(num2, num4 - l, num2 - l, num4);
					if (resultPoint4 != null)
					{
						break;
					}
				}
				if (resultPoint4 == null)
				{
					return null;
				}
				return centerEdges(resultPoint4, resultPoint, resultPoint3, resultPoint2);
			}
			return null;
		}

		private ResultPoint getBlackPointOnSegment(float aX, float aY, float bX, float bY)
		{
			int num = MathUtils.round(MathUtils.distance(aX, aY, bX, bY));
			float num2 = (bX - aX) / (float)num;
			float num3 = (bY - aY) / (float)num;
			for (int i = 0; i < num; i++)
			{
				int num4 = MathUtils.round(aX + (float)i * num2);
				int num5 = MathUtils.round(aY + (float)i * num3);
				if (image[num4, num5])
				{
					return new ResultPoint(num4, num5);
				}
			}
			return null;
		}

		private ResultPoint[] centerEdges(ResultPoint y, ResultPoint z, ResultPoint x, ResultPoint t)
		{
			float x2 = y.X;
			float y2 = y.Y;
			float x3 = z.X;
			float y3 = z.Y;
			float x4 = x.X;
			float y4 = x.Y;
			float x5 = t.X;
			float y5 = t.Y;
			if (!(x2 < (float)width / 2f))
			{
				return new ResultPoint[4]
				{
					new ResultPoint(x5 + 1f, y5 + 1f),
					new ResultPoint(x3 + 1f, y3 - 1f),
					new ResultPoint(x4 - 1f, y4 + 1f),
					new ResultPoint(x2 - 1f, y2 - 1f)
				};
			}
			return new ResultPoint[4]
			{
				new ResultPoint(x5 - 1f, y5 + 1f),
				new ResultPoint(x3 + 1f, y3 + 1f),
				new ResultPoint(x4 - 1f, y4 - 1f),
				new ResultPoint(x2 + 1f, y2 - 1f)
			};
		}

		private bool containsBlackPoint(int a, int b, int @fixed, bool horizontal)
		{
			if (horizontal)
			{
				for (int i = a; i <= b; i++)
				{
					if (image[i, @fixed])
					{
						return true;
					}
				}
			}
			else
			{
				for (int j = a; j <= b; j++)
				{
					if (image[@fixed, j])
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
