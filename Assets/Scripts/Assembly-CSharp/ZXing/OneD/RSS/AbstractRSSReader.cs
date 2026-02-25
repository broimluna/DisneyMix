namespace ZXing.OneD.RSS
{
	public abstract class AbstractRSSReader : OneDReader
	{
		private const float MIN_FINDER_PATTERN_RATIO = 19f / 24f;

		private const float MAX_FINDER_PATTERN_RATIO = 25f / 28f;

		private static readonly int MAX_AVG_VARIANCE = (int)((float)OneDReader.PATTERN_MATCH_RESULT_SCALE_FACTOR * 0.2f);

		private static readonly int MAX_INDIVIDUAL_VARIANCE = (int)((float)OneDReader.PATTERN_MATCH_RESULT_SCALE_FACTOR * 0.45f);

		private readonly int[] decodeFinderCounters;

		private readonly int[] dataCharacterCounters;

		private readonly float[] oddRoundingErrors;

		private readonly float[] evenRoundingErrors;

		private readonly int[] oddCounts;

		private readonly int[] evenCounts;

		protected AbstractRSSReader()
		{
			decodeFinderCounters = new int[4];
			dataCharacterCounters = new int[8];
			oddRoundingErrors = new float[4];
			evenRoundingErrors = new float[4];
			oddCounts = new int[dataCharacterCounters.Length / 2];
			evenCounts = new int[dataCharacterCounters.Length / 2];
		}

		protected int[] getDecodeFinderCounters()
		{
			return decodeFinderCounters;
		}

		protected int[] getDataCharacterCounters()
		{
			return dataCharacterCounters;
		}

		protected float[] getOddRoundingErrors()
		{
			return oddRoundingErrors;
		}

		protected float[] getEvenRoundingErrors()
		{
			return evenRoundingErrors;
		}

		protected int[] getOddCounts()
		{
			return oddCounts;
		}

		protected int[] getEvenCounts()
		{
			return evenCounts;
		}

		protected static bool parseFinderValue(int[] counters, int[][] finderPatterns, out int value)
		{
			for (value = 0; value < finderPatterns.Length; value++)
			{
				if (OneDReader.patternMatchVariance(counters, finderPatterns[value], MAX_INDIVIDUAL_VARIANCE) < MAX_AVG_VARIANCE)
				{
					return true;
				}
			}
			return false;
		}

		protected static int count(int[] array)
		{
			int num = 0;
			foreach (int num2 in array)
			{
				num += num2;
			}
			return num;
		}

		protected static void increment(int[] array, float[] errors)
		{
			int num = 0;
			float num2 = errors[0];
			for (int i = 1; i < array.Length; i++)
			{
				if (errors[i] > num2)
				{
					num2 = errors[i];
					num = i;
				}
			}
			array[num]++;
		}

		protected static void decrement(int[] array, float[] errors)
		{
			int num = 0;
			float num2 = errors[0];
			for (int i = 1; i < array.Length; i++)
			{
				if (errors[i] < num2)
				{
					num2 = errors[i];
					num = i;
				}
			}
			array[num]--;
		}

		protected static bool isFinderPattern(int[] counters)
		{
			int num = counters[0] + counters[1];
			int num2 = num + counters[2] + counters[3];
			float num3 = (float)num / (float)num2;
			if (num3 >= 19f / 24f && num3 <= 25f / 28f)
			{
				int num4 = int.MaxValue;
				int num5 = int.MinValue;
				foreach (int num6 in counters)
				{
					if (num6 > num5)
					{
						num5 = num6;
					}
					if (num6 < num4)
					{
						num4 = num6;
					}
				}
				return num5 < 10 * num4;
			}
			return false;
		}
	}
}
