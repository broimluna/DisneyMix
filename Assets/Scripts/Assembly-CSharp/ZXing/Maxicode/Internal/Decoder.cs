using System;
using System.Collections.Generic;
using ZXing.Common;
using ZXing.Common.ReedSolomon;

namespace ZXing.Maxicode.Internal
{
	public sealed class Decoder
	{
		private const int ALL = 0;

		private const int EVEN = 1;

		private const int ODD = 2;

		private readonly ReedSolomonDecoder rsDecoder;

		public Decoder()
		{
			rsDecoder = new ReedSolomonDecoder(GenericGF.MAXICODE_FIELD_64);
		}

		public DecoderResult decode(BitMatrix bits)
		{
			return decode(bits, null);
		}

		public DecoderResult decode(BitMatrix bits, IDictionary<DecodeHintType, object> hints)
		{
			BitMatrixParser bitMatrixParser = new BitMatrixParser(bits);
			byte[] array = bitMatrixParser.readCodewords();
			if (!correctErrors(array, 0, 10, 10, 0))
			{
				return null;
			}
			int num = array[0] & 0xF;
			byte[] array2;
			switch (num)
			{
			case 2:
			case 3:
			case 4:
				if (!correctErrors(array, 20, 84, 40, 1))
				{
					return null;
				}
				if (!correctErrors(array, 20, 84, 40, 2))
				{
					return null;
				}
				array2 = new byte[94];
				break;
			case 5:
				if (!correctErrors(array, 20, 68, 56, 1))
				{
					return null;
				}
				if (!correctErrors(array, 20, 68, 56, 2))
				{
					return null;
				}
				array2 = new byte[78];
				break;
			default:
				return null;
			}
			Array.Copy(array, 0, array2, 0, 10);
			Array.Copy(array, 20, array2, 10, array2.Length - 10);
			return DecodedBitStreamParser.decode(array2, num);
		}

		private bool correctErrors(byte[] codewordBytes, int start, int dataCodewords, int ecCodewords, int mode)
		{
			int num = dataCodewords + ecCodewords;
			int num2 = ((mode == 0) ? 1 : 2);
			int[] array = new int[num / num2];
			for (int i = 0; i < num; i++)
			{
				if (mode == 0 || i % 2 == mode - 1)
				{
					array[i / num2] = codewordBytes[i + start] & 0xFF;
				}
			}
			if (!rsDecoder.decode(array, ecCodewords / num2))
			{
				return false;
			}
			for (int j = 0; j < dataCodewords; j++)
			{
				if (mode == 0 || j % 2 == mode - 1)
				{
					codewordBytes[j + start] = (byte)array[j / num2];
				}
			}
			return true;
		}
	}
}
