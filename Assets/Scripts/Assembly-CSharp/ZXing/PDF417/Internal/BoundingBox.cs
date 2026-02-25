using System;
using ZXing.Common;

namespace ZXing.PDF417.Internal
{
	public sealed class BoundingBox
	{
		private readonly BitMatrix image;

		public ResultPoint TopLeft { get; private set; }

		public ResultPoint TopRight { get; private set; }

		public ResultPoint BottomLeft { get; private set; }

		public ResultPoint BottomRight { get; private set; }

		public int MinX { get; private set; }

		public int MaxX { get; private set; }

		public int MinY { get; private set; }

		public int MaxY { get; private set; }

		private BoundingBox(BitMatrix image, ResultPoint topLeft, ResultPoint bottomLeft, ResultPoint topRight, ResultPoint bottomRight)
		{
			this.image = image;
			TopLeft = topLeft;
			TopRight = topRight;
			BottomLeft = bottomLeft;
			BottomRight = bottomRight;
			calculateMinMaxValues();
		}

		public static BoundingBox Create(BitMatrix image, ResultPoint topLeft, ResultPoint bottomLeft, ResultPoint topRight, ResultPoint bottomRight)
		{
			if ((topLeft == null && topRight == null) || (bottomLeft == null && bottomRight == null) || (topLeft != null && bottomLeft == null) || (topRight != null && bottomRight == null))
			{
				return null;
			}
			return new BoundingBox(image, topLeft, bottomLeft, topRight, bottomRight);
		}

		public static BoundingBox Create(BoundingBox box)
		{
			return new BoundingBox(box.image, box.TopLeft, box.BottomLeft, box.TopRight, box.BottomRight);
		}

		internal static BoundingBox merge(BoundingBox leftBox, BoundingBox rightBox)
		{
			if (leftBox == null)
			{
				return rightBox;
			}
			if (rightBox == null)
			{
				return leftBox;
			}
			return new BoundingBox(leftBox.image, leftBox.TopLeft, leftBox.BottomLeft, rightBox.TopRight, rightBox.BottomRight);
		}

		public BoundingBox addMissingRows(int missingStartRows, int missingEndRows, bool isLeft)
		{
			ResultPoint topLeft = TopLeft;
			ResultPoint bottomLeft = BottomLeft;
			ResultPoint topRight = TopRight;
			ResultPoint bottomRight = BottomRight;
			if (missingStartRows > 0)
			{
				ResultPoint resultPoint = ((!isLeft) ? TopRight : TopLeft);
				int num = (int)resultPoint.Y - missingStartRows;
				if (num < 0)
				{
					num = 0;
				}
				ResultPoint resultPoint2 = new ResultPoint(resultPoint.X, num);
				if (isLeft)
				{
					topLeft = resultPoint2;
				}
				else
				{
					topRight = resultPoint2;
				}
			}
			if (missingEndRows > 0)
			{
				ResultPoint resultPoint3 = ((!isLeft) ? BottomRight : BottomLeft);
				int num2 = (int)resultPoint3.Y + missingEndRows;
				if (num2 >= image.Height)
				{
					num2 = image.Height - 1;
				}
				ResultPoint resultPoint4 = new ResultPoint(resultPoint3.X, num2);
				if (isLeft)
				{
					bottomLeft = resultPoint4;
				}
				else
				{
					bottomRight = resultPoint4;
				}
			}
			calculateMinMaxValues();
			return new BoundingBox(image, topLeft, bottomLeft, topRight, bottomRight);
		}

		private void calculateMinMaxValues()
		{
			if (TopLeft == null)
			{
				TopLeft = new ResultPoint(0f, TopRight.Y);
				BottomLeft = new ResultPoint(0f, BottomRight.Y);
			}
			else if (TopRight == null)
			{
				TopRight = new ResultPoint(image.Width - 1, TopLeft.Y);
				BottomRight = new ResultPoint(image.Width - 1, TopLeft.Y);
			}
			MinX = (int)Math.Min(TopLeft.X, BottomLeft.X);
			MaxX = (int)Math.Max(TopRight.X, BottomRight.X);
			MinY = (int)Math.Min(TopLeft.Y, TopRight.Y);
			MaxY = (int)Math.Max(BottomLeft.Y, BottomRight.Y);
		}

		internal void SetBottomRight(ResultPoint bottomRight)
		{
			BottomRight = bottomRight;
			calculateMinMaxValues();
		}
	}
}
