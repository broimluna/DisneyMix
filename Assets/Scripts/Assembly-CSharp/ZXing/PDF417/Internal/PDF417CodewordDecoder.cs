namespace ZXing.PDF417.Internal
{
	public static class PDF417CodewordDecoder
	{
		private static readonly float[][] RATIOS_TABLE;

		static PDF417CodewordDecoder()
		{
			RATIOS_TABLE = new float[PDF417Common.SYMBOL_TABLE.Length][];
			for (int i = 0; i < RATIOS_TABLE.Length; i++)
			{
				RATIOS_TABLE[i] = new float[PDF417Common.BARS_IN_MODULE];
			}
			for (int j = 0; j < PDF417Common.SYMBOL_TABLE.Length; j++)
			{
				int num = PDF417Common.SYMBOL_TABLE[j];
				int num2 = num & 1;
				for (int k = 0; k < PDF417Common.BARS_IN_MODULE; k++)
				{
					float num3 = 0f;
					while ((num & 1) == num2)
					{
						num3 += 1f;
						num >>= 1;
					}
					num2 = num & 1;
					RATIOS_TABLE[j][PDF417Common.BARS_IN_MODULE - k - 1] = num3 / (float)PDF417Common.MODULES_IN_CODEWORD;
				}
			}
		}

		public static int getDecodedValue(int[] moduleBitCount)
		{
			int decodedCodewordValue = getDecodedCodewordValue(sampleBitCounts(moduleBitCount));
			if (decodedCodewordValue != PDF417Common.INVALID_CODEWORD)
			{
				return decodedCodewordValue;
			}
			return getClosestDecodedValue(moduleBitCount);
		}

		private static int[] sampleBitCounts(int[] moduleBitCount)
		{
			float num = PDF417Common.getBitCountSum(moduleBitCount);
			int[] array = new int[PDF417Common.BARS_IN_MODULE];
			int num2 = 0;
			int num3 = 0;
			for (int i = 0; i < PDF417Common.MODULES_IN_CODEWORD; i++)
			{
				float num4 = num / (float)(2 * PDF417Common.MODULES_IN_CODEWORD) + (float)i * num / (float)PDF417Common.MODULES_IN_CODEWORD;
				if ((float)(num3 + moduleBitCount[num2]) <= num4)
				{
					num3 += moduleBitCount[num2];
					num2++;
				}
				array[num2]++;
			}
			return array;
		}

		private static int getDecodedCodewordValue(int[] moduleBitCount)
		{
			int bitValue = getBitValue(moduleBitCount);
			return (PDF417Common.getCodeword(bitValue) != PDF417Common.INVALID_CODEWORD) ? bitValue : PDF417Common.INVALID_CODEWORD;
		}

		private static int getBitValue(int[] moduleBitCount)
		{
			ulong num = 0uL;
			for (ulong num2 = 0uL; num2 < (ulong)moduleBitCount.Length; num2++)
			{
				for (int i = 0; i < moduleBitCount[num2]; i++)
				{
					num = (num << 1) | (ulong)((num2 % 2 != 0L) ? 0 : 1);
				}
			}
			return (int)num;
		}

		private static int getClosestDecodedValue(int[] moduleBitCount)
		{
			int bitCountSum = PDF417Common.getBitCountSum(moduleBitCount);
			float[] array = new float[PDF417Common.BARS_IN_MODULE];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = (float)moduleBitCount[i] / (float)bitCountSum;
			}
			float num = float.MaxValue;
			int result = PDF417Common.INVALID_CODEWORD;
			for (int j = 0; j < RATIOS_TABLE.Length; j++)
			{
				float num2 = 0f;
				float[] array2 = RATIOS_TABLE[j];
				for (int k = 0; k < PDF417Common.BARS_IN_MODULE; k++)
				{
					float num3 = array2[k] - array[k];
					num2 += num3 * num3;
					if (num2 >= num)
					{
						break;
					}
				}
				if (num2 < num)
				{
					num = num2;
					result = PDF417Common.SYMBOL_TABLE[j];
				}
			}
			return result;
		}
	}
}
