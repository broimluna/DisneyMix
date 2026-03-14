using ZXing.Common;

namespace ZXing.Aztec.Internal
{
	public sealed class SimpleToken : Token
	{
		private readonly short value;

		private readonly short bitCount;

		public SimpleToken(Token previous, int value, int bitCount)
			: base(previous)
		{
			this.value = (short)value;
			this.bitCount = (short)bitCount;
		}

		public override void appendTo(BitArray bitArray, byte[] text)
		{
			bitArray.appendBits(value, bitCount);
		}

		public override string ToString()
		{
			int num = value & ((1 << (int)bitCount) - 1);
			num |= 1 << (bitCount & 0x1F);
			return '<' + SupportClass.ToBinaryString(num | (1 << (int)bitCount)).Substring(1) + '>';
		}
	}
}
