using System.Collections.Generic;
using System.Collections.ObjectModel;
using ZXing.Common;

namespace ZXing.Aztec.Internal
{
	public sealed class HighLevelEncoder
	{
		internal const int MODE_UPPER = 0;

		internal const int MODE_LOWER = 1;

		internal const int MODE_DIGIT = 2;

		internal const int MODE_MIXED = 3;

		internal const int MODE_PUNCT = 4;

		internal static string[] MODE_NAMES;

		internal static readonly int[][] LATCH_TABLE;

		internal static readonly int[][] CHAR_MAP;

		internal static readonly int[][] SHIFT_TABLE;

		private readonly byte[] text;

		public HighLevelEncoder(byte[] text)
		{
			this.text = text;
		}

		static HighLevelEncoder()
		{
			MODE_NAMES = new string[5] { "UPPER", "LOWER", "DIGIT", "MIXED", "PUNCT" };
			LATCH_TABLE = new int[5][]
			{
				new int[5] { 0, 327708, 327710, 327709, 656318 },
				new int[5] { 590318, 0, 327710, 327709, 656318 },
				new int[5] { 262158, 590300, 0, 590301, 932798 },
				new int[5] { 327709, 327708, 656318, 0, 327710 },
				new int[5] { 327711, 656380, 656382, 656381, 0 }
			};
			CHAR_MAP = new int[5][];
			SHIFT_TABLE = new int[6][];
			CHAR_MAP[0] = new int[256];
			CHAR_MAP[1] = new int[256];
			CHAR_MAP[2] = new int[256];
			CHAR_MAP[3] = new int[256];
			CHAR_MAP[4] = new int[256];
			SHIFT_TABLE[0] = new int[6];
			SHIFT_TABLE[1] = new int[6];
			SHIFT_TABLE[2] = new int[6];
			SHIFT_TABLE[3] = new int[6];
			SHIFT_TABLE[4] = new int[6];
			SHIFT_TABLE[5] = new int[6];
			CHAR_MAP[0][32] = 1;
			for (int i = 65; i <= 90; i++)
			{
				CHAR_MAP[0][i] = i - 65 + 2;
			}
			CHAR_MAP[1][32] = 1;
			for (int j = 97; j <= 122; j++)
			{
				CHAR_MAP[1][j] = j - 97 + 2;
			}
			CHAR_MAP[2][32] = 1;
			for (int k = 48; k <= 57; k++)
			{
				CHAR_MAP[2][k] = k - 48 + 2;
			}
			CHAR_MAP[2][44] = 12;
			CHAR_MAP[2][46] = 13;
			int[] array = new int[28]
			{
				0, 32, 1, 2, 3, 4, 5, 6, 7, 8,
				9, 10, 11, 12, 13, 27, 28, 29, 30, 31,
				64, 92, 94, 95, 96, 124, 126, 127
			};
			for (int l = 0; l < array.Length; l++)
			{
				CHAR_MAP[3][array[l]] = l;
			}
			int[] array2 = new int[31]
			{
				0, 13, 0, 0, 0, 0, 33, 39, 35, 36,
				37, 38, 39, 40, 41, 42, 43, 44, 45, 46,
				47, 58, 59, 60, 61, 62, 63, 91, 93, 123,
				125
			};
			for (int m = 0; m < array2.Length; m++)
			{
				if (array2[m] > 0)
				{
					CHAR_MAP[4][array2[m]] = m;
				}
			}
			int[][] sHIFT_TABLE = SHIFT_TABLE;
			foreach (int[] array3 in sHIFT_TABLE)
			{
				SupportClass.Fill(array3, -1);
			}
			SHIFT_TABLE[0][4] = 0;
			SHIFT_TABLE[1][4] = 0;
			SHIFT_TABLE[1][0] = 28;
			SHIFT_TABLE[3][4] = 0;
			SHIFT_TABLE[2][4] = 0;
			SHIFT_TABLE[2][0] = 15;
		}

		public BitArray encode()
		{
			ICollection<State> collection = new Collection<State>();
			collection.Add(State.INITIAL_STATE);
			for (int i = 0; i < text.Length; i++)
			{
				int num = ((i + 1 < text.Length) ? text[i + 1] : 0);
				int num2;
				switch (text[i])
				{
				case 13:
					num2 = ((num == 10) ? 2 : 0);
					break;
				case 46:
					num2 = ((num == 32) ? 3 : 0);
					break;
				case 44:
					num2 = ((num == 32) ? 4 : 0);
					break;
				case 58:
					num2 = ((num == 32) ? 5 : 0);
					break;
				default:
					num2 = 0;
					break;
				}
				if (num2 > 0)
				{
					collection = updateStateListForPair(collection, i, num2);
					i++;
				}
				else
				{
					collection = updateStateListForChar(collection, i);
				}
			}
			State state = null;
			foreach (State item in collection)
			{
				if (state == null)
				{
					state = item;
				}
				else if (item.BitCount < state.BitCount)
				{
					state = item;
				}
			}
			return state.toBitArray(text);
		}

		private ICollection<State> updateStateListForChar(IEnumerable<State> states, int index)
		{
			LinkedList<State> linkedList = new LinkedList<State>();
			foreach (State state in states)
			{
				updateStateForChar(state, index, linkedList);
			}
			return simplifyStates(linkedList);
		}

		private void updateStateForChar(State state, int index, ICollection<State> result)
		{
			char c = (char)(text[index] & 0xFF);
			bool flag = CHAR_MAP[state.Mode][(uint)c] > 0;
			State state2 = null;
			for (int i = 0; i <= 4; i++)
			{
				int num = CHAR_MAP[i][(uint)c];
				if (num > 0)
				{
					if (state2 == null)
					{
						state2 = state.endBinaryShift(index);
					}
					if (!flag || i == state.Mode || i == 2)
					{
						State item = state2.latchAndAppend(i, num);
						result.Add(item);
					}
					if (!flag && SHIFT_TABLE[state.Mode][i] >= 0)
					{
						State item2 = state2.shiftAndAppend(i, num);
						result.Add(item2);
					}
				}
			}
			if (state.BinaryShiftByteCount > 0 || CHAR_MAP[state.Mode][(uint)c] == 0)
			{
				State item3 = state.addBinaryShiftChar(index);
				result.Add(item3);
			}
		}

		private static ICollection<State> updateStateListForPair(IEnumerable<State> states, int index, int pairCode)
		{
			LinkedList<State> linkedList = new LinkedList<State>();
			foreach (State state in states)
			{
				updateStateForPair(state, index, pairCode, linkedList);
			}
			return simplifyStates(linkedList);
		}

		private static void updateStateForPair(State state, int index, int pairCode, ICollection<State> result)
		{
			State state2 = state.endBinaryShift(index);
			result.Add(state2.latchAndAppend(4, pairCode));
			if (state.Mode != 4)
			{
				result.Add(state2.shiftAndAppend(4, pairCode));
			}
			if (pairCode == 3 || pairCode == 4)
			{
				State item = state2.latchAndAppend(2, 16 - pairCode).latchAndAppend(2, 1);
				result.Add(item);
			}
			if (state.BinaryShiftByteCount > 0)
			{
				State item2 = state.addBinaryShiftChar(index).addBinaryShiftChar(index + 1);
				result.Add(item2);
			}
		}

		private static ICollection<State> simplifyStates(IEnumerable<State> states)
		{
			LinkedList<State> linkedList = new LinkedList<State>();
			List<State> list = new List<State>();
			foreach (State state in states)
			{
				bool flag = true;
				list.Clear();
				foreach (State item in linkedList)
				{
					if (item.isBetterThanOrEqualTo(state))
					{
						flag = false;
						break;
					}
					if (state.isBetterThanOrEqualTo(item))
					{
						list.Add(item);
					}
				}
				if (flag)
				{
					linkedList.AddLast(state);
				}
				foreach (State item2 in list)
				{
					linkedList.Remove(item2);
				}
			}
			return linkedList;
		}
	}
}
