namespace ZXing.QrCode.Internal
{
	internal sealed class FormatInformation
	{
		private const int FORMAT_INFO_MASK_QR = 21522;

		private static readonly int[][] FORMAT_INFO_DECODE_LOOKUP = new int[32][]
		{
			new int[2] { 21522, 0 },
			new int[2] { 20773, 1 },
			new int[2] { 24188, 2 },
			new int[2] { 23371, 3 },
			new int[2] { 17913, 4 },
			new int[2] { 16590, 5 },
			new int[2] { 20375, 6 },
			new int[2] { 19104, 7 },
			new int[2] { 30660, 8 },
			new int[2] { 29427, 9 },
			new int[2] { 32170, 10 },
			new int[2] { 30877, 11 },
			new int[2] { 26159, 12 },
			new int[2] { 25368, 13 },
			new int[2] { 27713, 14 },
			new int[2] { 26998, 15 },
			new int[2] { 5769, 16 },
			new int[2] { 5054, 17 },
			new int[2] { 7399, 18 },
			new int[2] { 6608, 19 },
			new int[2] { 1890, 20 },
			new int[2] { 597, 21 },
			new int[2] { 3340, 22 },
			new int[2] { 2107, 23 },
			new int[2] { 13663, 24 },
			new int[2] { 12392, 25 },
			new int[2] { 16177, 26 },
			new int[2] { 14854, 27 },
			new int[2] { 9396, 28 },
			new int[2] { 8579, 29 },
			new int[2] { 11994, 30 },
			new int[2] { 11245, 31 }
		};

		private static readonly int[] BITS_SET_IN_HALF_BYTE = new int[16]
		{
			0, 1, 1, 2, 1, 2, 2, 3, 1, 2,
			2, 3, 2, 3, 3, 4
		};

		private readonly ErrorCorrectionLevel errorCorrectionLevel;

		private readonly byte dataMask;

		internal ErrorCorrectionLevel ErrorCorrectionLevel
		{
			get
			{
				return errorCorrectionLevel;
			}
		}

		internal byte DataMask
		{
			get
			{
				return dataMask;
			}
		}

		private FormatInformation(int formatInfo)
		{
			errorCorrectionLevel = ErrorCorrectionLevel.forBits((formatInfo >> 3) & 3);
			dataMask = (byte)(formatInfo & 7);
		}

		internal static int numBitsDiffering(int a, int b)
		{
			a ^= b;
			return BITS_SET_IN_HALF_BYTE[a & 0xF] + BITS_SET_IN_HALF_BYTE[((uint)a >> 4) & 0xF] + BITS_SET_IN_HALF_BYTE[((uint)a >> 8) & 0xF] + BITS_SET_IN_HALF_BYTE[((uint)a >> 12) & 0xF] + BITS_SET_IN_HALF_BYTE[((uint)a >> 16) & 0xF] + BITS_SET_IN_HALF_BYTE[((uint)a >> 20) & 0xF] + BITS_SET_IN_HALF_BYTE[((uint)a >> 24) & 0xF] + BITS_SET_IN_HALF_BYTE[((uint)a >> 28) & 0xF];
		}

		internal static FormatInformation decodeFormatInformation(int maskedFormatInfo1, int maskedFormatInfo2)
		{
			FormatInformation formatInformation = doDecodeFormatInformation(maskedFormatInfo1, maskedFormatInfo2);
			if (formatInformation != null)
			{
				return formatInformation;
			}
			return doDecodeFormatInformation(maskedFormatInfo1 ^ 0x5412, maskedFormatInfo2 ^ 0x5412);
		}

		private static FormatInformation doDecodeFormatInformation(int maskedFormatInfo1, int maskedFormatInfo2)
		{
			int num = int.MaxValue;
			int formatInfo = 0;
			int[][] fORMAT_INFO_DECODE_LOOKUP = FORMAT_INFO_DECODE_LOOKUP;
			foreach (int[] array in fORMAT_INFO_DECODE_LOOKUP)
			{
				int num2 = array[0];
				if (num2 == maskedFormatInfo1 || num2 == maskedFormatInfo2)
				{
					return new FormatInformation(array[1]);
				}
				int num3 = numBitsDiffering(maskedFormatInfo1, num2);
				if (num3 < num)
				{
					formatInfo = array[1];
					num = num3;
				}
				if (maskedFormatInfo1 != maskedFormatInfo2)
				{
					num3 = numBitsDiffering(maskedFormatInfo2, num2);
					if (num3 < num)
					{
						formatInfo = array[1];
						num = num3;
					}
				}
			}
			if (num <= 3)
			{
				return new FormatInformation(formatInfo);
			}
			return null;
		}

		public override int GetHashCode()
		{
			return (errorCorrectionLevel.ordinal() << 3) | dataMask;
		}

		public override bool Equals(object o)
		{
			if (!(o is FormatInformation))
			{
				return false;
			}
			FormatInformation formatInformation = (FormatInformation)o;
			return errorCorrectionLevel == formatInformation.errorCorrectionLevel && dataMask == formatInformation.dataMask;
		}
	}
}
