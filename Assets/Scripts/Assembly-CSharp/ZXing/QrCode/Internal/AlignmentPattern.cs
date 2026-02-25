using System;

namespace ZXing.QrCode.Internal
{
	public sealed class AlignmentPattern : ResultPoint
	{
		private float estimatedModuleSize;

		internal AlignmentPattern(float posX, float posY, float estimatedModuleSize)
			: base(posX, posY)
		{
			this.estimatedModuleSize = estimatedModuleSize;
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

		internal AlignmentPattern combineEstimate(float i, float j, float newModuleSize)
		{
			float posX = (X + j) / 2f;
			float posY = (Y + i) / 2f;
			float num = (estimatedModuleSize + newModuleSize) / 2f;
			return new AlignmentPattern(posX, posY, num);
		}
	}
}
