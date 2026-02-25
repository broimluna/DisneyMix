using System;
using System.Text;

namespace ZXing.Common
{
	public sealed class BitArray
	{
		private int[] bits;

		private int size;

		private static readonly int[] _lookup = new int[37]
		{
			32, 0, 1, 26, 2, 23, 27, 0, 3, 16,
			24, 30, 28, 11, 0, 13, 4, 7, 17, 0,
			25, 22, 31, 15, 29, 10, 12, 6, 0, 21,
			14, 9, 5, 20, 8, 19, 18
		};

		public int Size
		{
			get
			{
				return size;
			}
		}

		public int SizeInBytes
		{
			get
			{
				return size + 7 >> 3;
			}
		}

		public bool this[int i]
		{
			get
			{
				return (bits[i >> 5] & (1 << (i & 0x1F))) != 0;
			}
			set
			{
				if (value)
				{
					bits[i >> 5] |= 1 << (i & 0x1F);
				}
			}
		}

		public int[] Array
		{
			get
			{
				return bits;
			}
		}

		public BitArray()
		{
			size = 0;
			bits = new int[1];
		}

		public BitArray(int size)
		{
			if (size < 1)
			{
				throw new ArgumentException("size must be at least 1");
			}
			this.size = size;
			bits = makeArray(size);
		}

		private BitArray(int[] bits, int size)
		{
			this.bits = bits;
			this.size = size;
		}

		private void ensureCapacity(int size)
		{
			if (size > bits.Length << 5)
			{
				int[] destinationArray = makeArray(size);
				System.Array.Copy(bits, 0, destinationArray, 0, bits.Length);
				bits = destinationArray;
			}
		}

		public void flip(int i)
		{
			bits[i >> 5] ^= 1 << (i & 0x1F);
		}

		private static int numberOfTrailingZeros(int num)
		{
			int num2 = (-num & num) % 37;
			if (num2 < 0)
			{
				num2 *= -1;
			}
			return _lookup[num2];
		}

		public int getNextSet(int from)
		{
			if (from >= size)
			{
				return size;
			}
			int num = from >> 5;
			int num2 = bits[num];
			for (num2 &= ~((1 << (from & 0x1F)) - 1); num2 == 0; num2 = bits[num])
			{
				if (++num == bits.Length)
				{
					return size;
				}
			}
			int num3 = (num << 5) + numberOfTrailingZeros(num2);
			return (num3 <= size) ? num3 : size;
		}

		public int getNextUnset(int from)
		{
			if (from >= size)
			{
				return size;
			}
			int num = from >> 5;
			int num2 = ~bits[num];
			for (num2 &= ~((1 << (from & 0x1F)) - 1); num2 == 0; num2 = ~bits[num])
			{
				if (++num == bits.Length)
				{
					return size;
				}
			}
			int num3 = (num << 5) + numberOfTrailingZeros(num2);
			return (num3 <= size) ? num3 : size;
		}

		public void setBulk(int i, int newBits)
		{
			bits[i >> 5] = newBits;
		}

		public void setRange(int start, int end)
		{
			if (end < start)
			{
				throw new ArgumentException();
			}
			if (end == start)
			{
				return;
			}
			end--;
			int num = start >> 5;
			int num2 = end >> 5;
			for (int i = num; i <= num2; i++)
			{
				int num3 = ((i <= num) ? (start & 0x1F) : 0);
				int num4 = ((i >= num2) ? (end & 0x1F) : 31);
				int num5;
				if (num3 == 0 && num4 == 31)
				{
					num5 = -1;
				}
				else
				{
					num5 = 0;
					for (int j = num3; j <= num4; j++)
					{
						num5 |= 1 << j;
					}
				}
				bits[i] |= num5;
			}
		}

		public void clear()
		{
			int num = bits.Length;
			for (int i = 0; i < num; i++)
			{
				bits[i] = 0;
			}
		}

		public bool isRange(int start, int end, bool value)
		{
			if (end < start)
			{
				throw new ArgumentException();
			}
			if (end == start)
			{
				return true;
			}
			end--;
			int num = start >> 5;
			int num2 = end >> 5;
			for (int i = num; i <= num2; i++)
			{
				int num3 = ((i <= num) ? (start & 0x1F) : 0);
				int num4 = ((i >= num2) ? (end & 0x1F) : 31);
				int num5;
				if (num3 == 0 && num4 == 31)
				{
					num5 = -1;
				}
				else
				{
					num5 = 0;
					for (int j = num3; j <= num4; j++)
					{
						num5 |= 1 << j;
					}
				}
				if ((bits[i] & num5) != (value ? num5 : 0))
				{
					return false;
				}
			}
			return true;
		}

		public void appendBit(bool bit)
		{
			ensureCapacity(size + 1);
			if (bit)
			{
				bits[size >> 5] |= 1 << (size & 0x1F);
			}
			size++;
		}

		public void appendBits(int value, int numBits)
		{
			if (numBits < 0 || numBits > 32)
			{
				throw new ArgumentException("Num bits must be between 0 and 32");
			}
			ensureCapacity(size + numBits);
			for (int num = numBits; num > 0; num--)
			{
				appendBit(((value >> num - 1) & 1) == 1);
			}
		}

		public void appendBitArray(BitArray other)
		{
			int num = other.size;
			ensureCapacity(size + num);
			for (int i = 0; i < num; i++)
			{
				appendBit(other[i]);
			}
		}

		public void xor(BitArray other)
		{
			if (bits.Length != other.bits.Length)
			{
				throw new ArgumentException("Sizes don't match");
			}
			for (int i = 0; i < bits.Length; i++)
			{
				bits[i] ^= other.bits[i];
			}
		}

		public void toBytes(int bitOffset, byte[] array, int offset, int numBytes)
		{
			for (int i = 0; i < numBytes; i++)
			{
				int num = 0;
				for (int j = 0; j < 8; j++)
				{
					if (this[bitOffset])
					{
						num |= 1 << ((7 - j) & 0x1F);
					}
					bitOffset++;
				}
				array[offset + i] = (byte)num;
			}
		}

		public void reverse()
		{
			int[] array = new int[bits.Length];
			int num = size - 1 >> 5;
			int num2 = num + 1;
			for (int i = 0; i < num2; i++)
			{
				long num3 = bits[i];
				num3 = ((num3 >> 1) & 0x55555555) | ((num3 & 0x55555555) << 1);
				num3 = ((num3 >> 2) & 0x33333333) | ((num3 & 0x33333333) << 2);
				num3 = ((num3 >> 4) & 0xF0F0F0F) | ((num3 & 0xF0F0F0F) << 4);
				num3 = ((num3 >> 8) & 0xFF00FF) | ((num3 & 0xFF00FF) << 8);
				num3 = ((num3 >> 16) & 0xFFFF) | ((num3 & 0xFFFF) << 16);
				array[num - i] = (int)num3;
			}
			if (size != num2 * 32)
			{
				int num4 = num2 * 32 - size;
				int num5 = 1;
				for (int j = 0; j < 31 - num4; j++)
				{
					num5 = (num5 << 1) | 1;
				}
				int num6 = (array[0] >> num4) & num5;
				for (int k = 1; k < num2; k++)
				{
					int num7 = array[k];
					num6 |= num7 << 32 - num4;
					array[k - 1] = num6;
					num6 = (num7 >> num4) & num5;
				}
				array[num2 - 1] = num6;
			}
			bits = array;
		}

		private static int[] makeArray(int size)
		{
			return new int[size + 31 >> 5];
		}

		public override bool Equals(object o)
		{
			BitArray bitArray = o as BitArray;
			if (bitArray == null)
			{
				return false;
			}
			if (size != bitArray.size)
			{
				return false;
			}
			for (int i = 0; i < size; i++)
			{
				if (bits[i] != bitArray.bits[i])
				{
					return false;
				}
			}
			return true;
		}

		public override int GetHashCode()
		{
			int num = size;
			int[] array = bits;
			foreach (int num2 in array)
			{
				num = 31 * num + num2.GetHashCode();
			}
			return num;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(size);
			for (int i = 0; i < size; i++)
			{
				if ((i & 7) == 0)
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append((!this[i]) ? '.' : 'X');
			}
			return stringBuilder.ToString();
		}

		public object Clone()
		{
			return new BitArray((int[])bits.Clone(), size);
		}
	}
}
