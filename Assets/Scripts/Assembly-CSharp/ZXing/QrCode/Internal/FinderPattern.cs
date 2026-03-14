using System;

namespace ZXing.QrCode.Internal
{
	public sealed class FinderPattern : ResultPoint
	{
		private readonly float estimatedModuleSize;

		private int count;

		public float EstimatedModuleSize
		{
			get
			{
				return estimatedModuleSize;
			}
		}

		internal int Count
		{
			get
			{
				return count;
			}
		}

		internal FinderPattern(float posX, float posY, float estimatedModuleSize)
			: this(posX, posY, estimatedModuleSize, 1)
		{
			this.estimatedModuleSize = estimatedModuleSize;
			count = 1;
		}

		internal FinderPattern(float posX, float posY, float estimatedModuleSize, int count)
			: base(posX, posY)
		{
			this.estimatedModuleSize = estimatedModuleSize;
			this.count = count;
		}

		internal bool aboutEquals(float moduleSize, float i, float j)
		{
			if (Math.Abs(i - Y) <= moduleSize && Math.Abs(j - X) <= moduleSize)
			{
				float num = Math.Abs(moduleSize - estimatedModuleSize);
				return num <= 1f || num <= estimatedModuleSize;
			}
			return false;
		}

		internal FinderPattern combineEstimate(float i, float j, float newModuleSize)
		{
			int num = count + 1;
			float posX = ((float)count * X + j) / (float)num;
			float posY = ((float)count * Y + i) / (float)num;
			float num2 = ((float)count * estimatedModuleSize + newModuleSize) / (float)num;
			return new FinderPattern(posX, posY, num2, num);
		}
	}
}
