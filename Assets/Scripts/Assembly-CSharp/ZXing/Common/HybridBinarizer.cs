namespace ZXing.Common
{
	public sealed class HybridBinarizer : GlobalHistogramBinarizer
	{
		private const int BLOCK_SIZE_POWER = 3;

		private const int BLOCK_SIZE = 8;

		private const int BLOCK_SIZE_MASK = 7;

		private const int MINIMUM_DIMENSION = 40;

		private const int MIN_DYNAMIC_RANGE = 24;

		private BitMatrix matrix;

		public override BitMatrix BlackMatrix
		{
			get
			{
				binarizeEntireImage();
				return matrix;
			}
		}

		public HybridBinarizer(LuminanceSource source)
			: base(source)
		{
		}

		public override Binarizer createBinarizer(LuminanceSource source)
		{
			return new HybridBinarizer(source);
		}

		private void binarizeEntireImage()
		{
			if (matrix != null)
			{
				return;
			}
			LuminanceSource luminanceSource = LuminanceSource;
			int width = luminanceSource.Width;
			int height = luminanceSource.Height;
			if (width >= 40 && height >= 40)
			{
				byte[] array = luminanceSource.Matrix;
				int num = width >> 3;
				if ((width & 7) != 0)
				{
					num++;
				}
				int num2 = height >> 3;
				if ((height & 7) != 0)
				{
					num2++;
				}
				int[][] blackPoints = calculateBlackPoints(array, num, num2, width, height);
				BitMatrix bitMatrix = new BitMatrix(width, height);
				calculateThresholdForBlock(array, num, num2, width, height, blackPoints, bitMatrix);
				matrix = bitMatrix;
			}
			else
			{
				matrix = base.BlackMatrix;
			}
		}

		private static void calculateThresholdForBlock(byte[] luminances, int subWidth, int subHeight, int width, int height, int[][] blackPoints, BitMatrix matrix)
		{
			for (int i = 0; i < subHeight; i++)
			{
				int num = i << 3;
				int num2 = height - 8;
				if (num > num2)
				{
					num = num2;
				}
				for (int j = 0; j < subWidth; j++)
				{
					int num3 = j << 3;
					int num4 = width - 8;
					if (num3 > num4)
					{
						num3 = num4;
					}
					int num5 = cap(j, 2, subWidth - 3);
					int num6 = cap(i, 2, subHeight - 3);
					int num7 = 0;
					for (int k = -2; k <= 2; k++)
					{
						int[] array = blackPoints[num6 + k];
						num7 += array[num5 - 2];
						num7 += array[num5 - 1];
						num7 += array[num5];
						num7 += array[num5 + 1];
						num7 += array[num5 + 2];
					}
					int threshold = num7 / 25;
					thresholdBlock(luminances, num3, num, threshold, width, matrix);
				}
			}
		}

		private static int cap(int value, int min, int max)
		{
			return (value < min) ? min : ((value <= max) ? value : max);
		}

		private static void thresholdBlock(byte[] luminances, int xoffset, int yoffset, int threshold, int stride, BitMatrix matrix)
		{
			int num = yoffset * stride + xoffset;
			int num2 = 0;
			while (num2 < 8)
			{
				for (int i = 0; i < 8; i++)
				{
					int num3 = luminances[num + i] & 0xFF;
					matrix[xoffset + i, yoffset + num2] = num3 <= threshold;
				}
				num2++;
				num += stride;
			}
		}

		private static int[][] calculateBlackPoints(byte[] luminances, int subWidth, int subHeight, int width, int height)
		{
			int[][] array = new int[subHeight][];
			for (int i = 0; i < subHeight; i++)
			{
				array[i] = new int[subWidth];
			}
			for (int j = 0; j < subHeight; j++)
			{
				int num = j << 3;
				int num2 = height - 8;
				if (num > num2)
				{
					num = num2;
				}
				for (int k = 0; k < subWidth; k++)
				{
					int num3 = k << 3;
					int num4 = width - 8;
					if (num3 > num4)
					{
						num3 = num4;
					}
					int num5 = 0;
					int num6 = 255;
					int num7 = 0;
					int num8 = 0;
					int num9 = num * width + num3;
					while (num8 < 8)
					{
						for (int l = 0; l < 8; l++)
						{
							int num10 = luminances[num9 + l] & 0xFF;
							num5 += num10;
							if (num10 < num6)
							{
								num6 = num10;
							}
							if (num10 > num7)
							{
								num7 = num10;
							}
						}
						if (num7 - num6 > 24)
						{
							num8++;
							num9 += width;
							while (num8 < 8)
							{
								for (int m = 0; m < 8; m++)
								{
									num5 += luminances[num9 + m] & 0xFF;
								}
								num8++;
								num9 += width;
							}
						}
						num8++;
						num9 += width;
					}
					int num11 = num5 >> 6;
					if (num7 - num6 <= 24)
					{
						num11 = num6 >> 1;
						if (j > 0 && k > 0)
						{
							int num12 = array[j - 1][k] + 2 * array[j][k - 1] + array[j - 1][k - 1] >> 2;
							if (num6 < num12)
							{
								num11 = num12;
							}
						}
					}
					array[j][k] = num11;
				}
			}
			return array;
		}
	}
}
