using ZXing.Common;

namespace ZXing.Aztec.Internal
{
	public abstract class Token
	{
		public static Token EMPTY = new SimpleToken(null, 0, 0);

		private readonly Token previous;

		public Token Previous
		{
			get
			{
				return previous;
			}
		}

		protected Token(Token previous)
		{
			this.previous = previous;
		}

		public Token add(int value, int bitCount)
		{
			return new SimpleToken(this, value, bitCount);
		}

		public Token addBinaryShift(int start, int byteCount)
		{
			return new BinaryShiftToken(this, start, byteCount);
		}

		public abstract void appendTo(BitArray bitArray, byte[] text);
	}
}
