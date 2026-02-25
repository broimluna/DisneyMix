using System;
using System.Collections.Generic;
using System.Text;

namespace ZXing
{
	public static class SupportClass
	{
		public static void GetCharsFromString(string sourceString, int sourceStart, int sourceEnd, char[] destinationArray, int destinationStart)
		{
			int num = sourceStart;
			int num2 = destinationStart;
			while (num < sourceEnd)
			{
				destinationArray[num2] = sourceString[num];
				num++;
				num2++;
			}
		}

		public static void SetCapacity<T>(IList<T> vector, int newCapacity) where T : new()
		{
			while (newCapacity > vector.Count)
			{
				vector.Add(new T());
			}
			while (newCapacity < vector.Count)
			{
				vector.RemoveAt(vector.Count - 1);
			}
		}

		public static string[] toStringArray(ICollection<string> strings)
		{
			string[] array = new string[strings.Count];
			strings.CopyTo(array, 0);
			return array;
		}

		public static string Join<T>(string separator, IEnumerable<T> values)
		{
			StringBuilder stringBuilder = new StringBuilder();
			separator = separator ?? string.Empty;
			if (values != null)
			{
				foreach (T value in values)
				{
					stringBuilder.Append(value);
					stringBuilder.Append(separator);
				}
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Length -= separator.Length;
				}
			}
			return stringBuilder.ToString();
		}

		public static void Fill<T>(T[] array, T value)
		{
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = value;
			}
		}

		public static void Fill<T>(T[] array, int startIndex, int endIndex, T value)
		{
			for (int i = startIndex; i < endIndex; i++)
			{
				array[i] = value;
			}
		}

		public static string ToBinaryString(int x)
		{
			char[] array = new char[32];
			int length = 0;
			while (x != 0)
			{
				array[length++] = (((x & 1) != 1) ? '0' : '1');
				x >>= 1;
			}
			Array.Reverse(array, 0, length);
			return new string(array);
		}

		public static int bitCount(int n)
		{
			int num = 0;
			while (n != 0)
			{
				n &= n - 1;
				num++;
			}
			return num;
		}

		public static T GetValue<T>(IDictionary<DecodeHintType, object> hints, DecodeHintType hintType, T @default)
		{
			if (hints == null)
			{
				return @default;
			}
			if (!hints.ContainsKey(hintType))
			{
				return @default;
			}
			return (T)hints[hintType];
		}
	}
}
