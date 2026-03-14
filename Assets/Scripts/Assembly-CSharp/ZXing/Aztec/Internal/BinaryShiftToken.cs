using System;
using ZXing.Common;

namespace ZXing.Aztec.Internal
{
	public sealed class BinaryShiftToken : Token
	{
		private readonly short binaryShiftStart;

		private readonly short binaryShiftByteCount;

		public BinaryShiftToken(Token previous, int binaryShiftStart, int binaryShiftByteCount)
			: base(previous)
		{
			this.binaryShiftStart = (short)binaryShiftStart;
			this.binaryShiftByteCount = (short)binaryShiftByteCount;
		}

		public override void appendTo(BitArray bitArray, byte[] text)
		{
			for (int i = 0; i < binaryShiftByteCount; i++)
			{
				if (i == 0 || (i == 31 && binaryShiftByteCount <= 62))
				{
					bitArray.appendBits(31, 5);
					if (binaryShiftByteCount > 62)
					{
						bitArray.appendBits(binaryShiftByteCount - 31, 16);
					}
					else if (i == 0)
					{
						bitArray.appendBits(Math.Min(binaryShiftByteCount, (short)31), 5);
					}
					else
					{
						bitArray.appendBits(binaryShiftByteCount - 31, 5);
					}
				}
				bitArray.appendBits(text[binaryShiftStart + i], 8);
			}
		}

		public override string ToString()
		{
			return "<" + binaryShiftStart + "::" + (binaryShiftStart + binaryShiftByteCount - 1) + '>';
		}
	}
}
