using System.Collections.Generic;
using ZXing.Common;

namespace ZXing.Aztec.Internal
{
	internal sealed class State
	{
		public static readonly State INITIAL_STATE = new State(Token.EMPTY, 0, 0, 0);

		private readonly int mode;

		private readonly Token token;

		private readonly int binaryShiftByteCount;

		private readonly int bitCount;

		public int Mode
		{
			get
			{
				return mode;
			}
		}

		public Token Token
		{
			get
			{
				return token;
			}
		}

		public int BinaryShiftByteCount
		{
			get
			{
				return binaryShiftByteCount;
			}
		}

		public int BitCount
		{
			get
			{
				return bitCount;
			}
		}

		public State(Token token, int mode, int binaryBytes, int bitCount)
		{
			this.token = token;
			this.mode = mode;
			binaryShiftByteCount = binaryBytes;
			this.bitCount = bitCount;
		}

		public State latchAndAppend(int mode, int value)
		{
			int num = bitCount;
			Token token = this.token;
			if (mode != this.mode)
			{
				int num2 = HighLevelEncoder.LATCH_TABLE[this.mode][mode];
				token = token.add(num2 & 0xFFFF, num2 >> 16);
				num += num2 >> 16;
			}
			int num3 = ((mode != 2) ? 5 : 4);
			token = token.add(value, num3);
			return new State(token, mode, 0, num + num3);
		}

		public State shiftAndAppend(int mode, int value)
		{
			Token token = this.token;
			int num = ((this.mode != 2) ? 5 : 4);
			token = token.add(HighLevelEncoder.SHIFT_TABLE[this.mode][mode], num);
			token = token.add(value, 5);
			return new State(token, this.mode, 0, bitCount + num + 5);
		}

		public State addBinaryShiftChar(int index)
		{
			Token token = this.token;
			int num = mode;
			int num2 = bitCount;
			if (mode == 4 || mode == 2)
			{
				int num3 = HighLevelEncoder.LATCH_TABLE[num][0];
				token = token.add(num3 & 0xFFFF, num3 >> 16);
				num2 += num3 >> 16;
				num = 0;
			}
			int num4 = ((binaryShiftByteCount == 0 || binaryShiftByteCount == 31) ? 18 : ((binaryShiftByteCount != 62) ? 8 : 9));
			State state = new State(token, num, binaryShiftByteCount + 1, num2 + num4);
			if (state.binaryShiftByteCount == 2078)
			{
				state = state.endBinaryShift(index + 1);
			}
			return state;
		}

		public State endBinaryShift(int index)
		{
			if (binaryShiftByteCount == 0)
			{
				return this;
			}
			Token token = this.token;
			token = token.addBinaryShift(index - binaryShiftByteCount, binaryShiftByteCount);
			return new State(token, mode, 0, bitCount);
		}

		public bool isBetterThanOrEqualTo(State other)
		{
			int num = bitCount + (HighLevelEncoder.LATCH_TABLE[mode][other.mode] >> 16);
			if (other.binaryShiftByteCount > 0 && (binaryShiftByteCount == 0 || binaryShiftByteCount > other.binaryShiftByteCount))
			{
				num += 10;
			}
			return num <= other.bitCount;
		}

		public BitArray toBitArray(byte[] text)
		{
			LinkedList<Token> linkedList = new LinkedList<Token>();
			for (Token previous = endBinaryShift(text.Length).token; previous != null; previous = previous.Previous)
			{
				linkedList.AddFirst(previous);
			}
			BitArray bitArray = new BitArray();
			foreach (Token item in linkedList)
			{
				item.appendTo(bitArray, text);
			}
			return bitArray;
		}

		public override string ToString()
		{
			return string.Format("{0} bits={1} bytes={2}", HighLevelEncoder.MODE_NAMES[mode], bitCount, binaryShiftByteCount);
		}
	}
}
