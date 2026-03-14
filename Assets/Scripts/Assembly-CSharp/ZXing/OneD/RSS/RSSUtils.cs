namespace ZXing.OneD.RSS
{
	public static class RSSUtils
	{
		public static int getRSSvalue(int[] widths, int maxWidth, bool noNarrow)
		{
			int num = widths.Length;
			int num2 = 0;
			foreach (int num3 in widths)
			{
				num2 += num3;
			}
			int num4 = 0;
			int num5 = 0;
			for (int j = 0; j < num - 1; j++)
			{
				int num6 = 1;
				num5 |= 1 << j;
				while (num6 < widths[j])
				{
					int num7 = combins(num2 - num6 - 1, num - j - 2);
					if (noNarrow && num5 == 0 && num2 - num6 - (num - j - 1) >= num - j - 1)
					{
						num7 -= combins(num2 - num6 - (num - j), num - j - 2);
					}
					if (num - j - 1 > 1)
					{
						int num8 = 0;
						for (int num9 = num2 - num6 - (num - j - 2); num9 > maxWidth; num9--)
						{
							num8 += combins(num2 - num6 - num9 - 1, num - j - 3);
						}
						num7 -= num8 * (num - 1 - j);
					}
					else if (num2 - num6 > maxWidth)
					{
						num7--;
					}
					num4 += num7;
					num6++;
					num5 &= ~(1 << j);
				}
				num2 -= num6;
			}
			return num4;
		}

		private static int combins(int n, int r)
		{
			int num;
			int num2;
			if (n - r > r)
			{
				num = r;
				num2 = n - r;
			}
			else
			{
				num = n - r;
				num2 = r;
			}
			int num3 = 1;
			int i = 1;
			for (int num4 = n; num4 > num2; num4--)
			{
				num3 *= num4;
				if (i <= num)
				{
					num3 /= i;
					i++;
				}
			}
			for (; i <= num; i++)
			{
				num3 /= i;
			}
			return num3;
		}
	}
}
