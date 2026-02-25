using System;

namespace ZXing.OneD
{
	public sealed class CodaBarWriter : OneDimensionalCodeWriter
	{
		private static readonly char[] START_END_CHARS = new char[4] { 'A', 'B', 'C', 'D' };

		private static readonly char[] ALT_START_END_CHARS = new char[4] { 'T', 'N', '*', 'E' };

		private static readonly char[] CHARS_WHICH_ARE_TEN_LENGTH_EACH_AFTER_DECODED = new char[4] { '/', ':', '+', '.' };

		public override bool[] encode(string contents)
		{
			if (contents.Length < 2)
			{
				throw new ArgumentException("Codabar should start/end with start/stop symbols");
			}
			char key = char.ToUpper(contents[0]);
			char key2 = char.ToUpper(contents[contents.Length - 1]);
			bool flag = CodaBarReader.arrayContains(START_END_CHARS, key) && CodaBarReader.arrayContains(START_END_CHARS, key2);
			bool flag2 = CodaBarReader.arrayContains(ALT_START_END_CHARS, key) && CodaBarReader.arrayContains(ALT_START_END_CHARS, key2);
			if (!flag && !flag2)
			{
				throw new ArgumentException("Codabar should start/end with " + SupportClass.Join(", ", START_END_CHARS) + ", or start/end with " + SupportClass.Join(", ", ALT_START_END_CHARS));
			}
			int num = 20;
			for (int i = 1; i < contents.Length - 1; i++)
			{
				if (char.IsDigit(contents[i]) || contents[i] == '-' || contents[i] == '$')
				{
					num += 9;
					continue;
				}
				if (CodaBarReader.arrayContains(CHARS_WHICH_ARE_TEN_LENGTH_EACH_AFTER_DECODED, contents[i]))
				{
					num += 10;
					continue;
				}
				throw new ArgumentException("Cannot encode : '" + contents[i] + '\'');
			}
			num += contents.Length - 1;
			bool[] array = new bool[num];
			int num2 = 0;
			for (int j = 0; j < contents.Length; j++)
			{
				char c = char.ToUpper(contents[j]);
				if (j == 0 || j == contents.Length - 1)
				{
					switch (c)
					{
					case 'T':
						c = 'A';
						break;
					case 'N':
						c = 'B';
						break;
					case '*':
						c = 'C';
						break;
					case 'E':
						c = 'D';
						break;
					}
				}
				int num3 = 0;
				for (int k = 0; k < CodaBarReader.ALPHABET.Length; k++)
				{
					if (c == CodaBarReader.ALPHABET[k])
					{
						num3 = CodaBarReader.CHARACTER_ENCODINGS[k];
						break;
					}
				}
				bool flag3 = true;
				int num4 = 0;
				int num5 = 0;
				while (num5 < 7)
				{
					array[num2] = flag3;
					num2++;
					if (((num3 >> 6 - num5) & 1) == 0 || num4 == 1)
					{
						flag3 = !flag3;
						num5++;
						num4 = 0;
					}
					else
					{
						num4++;
					}
				}
				if (j < contents.Length - 1)
				{
					array[num2] = false;
					num2++;
				}
			}
			return array;
		}
	}
}
