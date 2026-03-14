using System.Collections.Generic;
using System.Text;
using ZXing.Common;
using ZXing.Common.ReedSolomon;

namespace ZXing.Aztec.Internal
{
	public sealed class Decoder
	{
		private enum Table
		{
			UPPER = 0,
			LOWER = 1,
			MIXED = 2,
			DIGIT = 3,
			PUNCT = 4,
			BINARY = 5
		}

		private static readonly string[] UPPER_TABLE = new string[32]
		{
			"CTRL_PS", " ", "A", "B", "C", "D", "E", "F", "G", "H",
			"I", "J", "K", "L", "M", "N", "O", "P", "Q", "R",
			"S", "T", "U", "V", "W", "X", "Y", "Z", "CTRL_LL", "CTRL_ML",
			"CTRL_DL", "CTRL_BS"
		};

		private static readonly string[] LOWER_TABLE = new string[32]
		{
			"CTRL_PS", " ", "a", "b", "c", "d", "e", "f", "g", "h",
			"i", "j", "k", "l", "m", "n", "o", "p", "q", "r",
			"s", "t", "u", "v", "w", "x", "y", "z", "CTRL_US", "CTRL_ML",
			"CTRL_DL", "CTRL_BS"
		};

		private static readonly string[] MIXED_TABLE = new string[32]
		{
			"CTRL_PS", " ", "\u0001", "\u0002", "\u0003", "\u0004", "\u0005", "\u0006", "\a", "\b",
			"\t", "\n", "\r", "\f", "\r", "!", "\"", "#", "$", "%",
			"@", "\\", "^", "_", "`", "|", "~", "±", "CTRL_LL", "CTRL_UL",
			"CTRL_PL", "CTRL_BS"
		};

		private static readonly string[] PUNCT_TABLE = new string[32]
		{
			string.Empty,
			"\r",
			"\r\n",
			". ",
			", ",
			": ",
			"!",
			"\"",
			"#",
			"$",
			"%",
			"&",
			"'",
			"(",
			")",
			"*",
			"+",
			",",
			"-",
			".",
			"/",
			":",
			";",
			"<",
			"=",
			">",
			"?",
			"[",
			"]",
			"{",
			"}",
			"CTRL_UL"
		};

		private static readonly string[] DIGIT_TABLE = new string[16]
		{
			"CTRL_PS", " ", "0", "1", "2", "3", "4", "5", "6", "7",
			"8", "9", ",", ".", "CTRL_UL", "CTRL_US"
		};

		private static readonly IDictionary<Table, string[]> codeTables = new Dictionary<Table, string[]>
		{
			{
				Table.UPPER,
				UPPER_TABLE
			},
			{
				Table.LOWER,
				LOWER_TABLE
			},
			{
				Table.MIXED,
				MIXED_TABLE
			},
			{
				Table.PUNCT,
				PUNCT_TABLE
			},
			{
				Table.DIGIT,
				DIGIT_TABLE
			},
			{
				Table.BINARY,
				null
			}
		};

		private static readonly IDictionary<char, Table> codeTableMap = new Dictionary<char, Table>
		{
			{
				'U',
				Table.UPPER
			},
			{
				'L',
				Table.LOWER
			},
			{
				'M',
				Table.MIXED
			},
			{
				'P',
				Table.PUNCT
			},
			{
				'D',
				Table.DIGIT
			},
			{
				'B',
				Table.BINARY
			}
		};

		private AztecDetectorResult ddata;

		public DecoderResult decode(AztecDetectorResult detectorResult)
		{
			ddata = detectorResult;
			BitMatrix bits = detectorResult.Bits;
			bool[] array = extractBits(bits);
			if (array == null)
			{
				return null;
			}
			bool[] array2 = correctBits(array);
			if (array2 == null)
			{
				return null;
			}
			string encodedData = getEncodedData(array2);
			if (encodedData == null)
			{
				return null;
			}
			return new DecoderResult(null, encodedData, null, null);
		}

		public static string highLevelDecode(bool[] correctedBits)
		{
			return getEncodedData(correctedBits);
		}

		private static string getEncodedData(bool[] correctedBits)
		{
			int num = correctedBits.Length;
			Table table = Table.UPPER;
			Table table2 = Table.UPPER;
			string[] table3 = UPPER_TABLE;
			StringBuilder stringBuilder = new StringBuilder(20);
			int num2 = 0;
			while (num2 < num)
			{
				if (table2 == Table.BINARY)
				{
					if (num - num2 < 5)
					{
						break;
					}
					int num3 = readCode(correctedBits, num2, 5);
					num2 += 5;
					if (num3 == 0)
					{
						if (num - num2 < 11)
						{
							break;
						}
						num3 = readCode(correctedBits, num2, 11) + 31;
						num2 += 11;
					}
					for (int i = 0; i < num3; i++)
					{
						if (num - num2 < 8)
						{
							num2 = num;
							break;
						}
						int num4 = readCode(correctedBits, num2, 8);
						stringBuilder.Append((char)num4);
						num2 += 8;
					}
					table2 = table;
					table3 = codeTables[table2];
					continue;
				}
				int num5 = ((table2 != Table.DIGIT) ? 5 : 4);
				if (num - num2 < num5)
				{
					break;
				}
				int code = readCode(correctedBits, num2, num5);
				num2 += num5;
				string character = getCharacter(table3, code);
				if (character.StartsWith("CTRL_"))
				{
					table2 = getTable(character[5]);
					table3 = codeTables[table2];
					if (character[6] == 'L')
					{
						table = table2;
					}
				}
				else
				{
					stringBuilder.Append(character);
					table2 = table;
					table3 = codeTables[table2];
				}
			}
			return stringBuilder.ToString();
		}

		private static Table getTable(char t)
		{
			if (!codeTableMap.ContainsKey(t))
			{
				return codeTableMap['U'];
			}
			return codeTableMap[t];
		}

		private static string getCharacter(string[] table, int code)
		{
			return table[code];
		}

		private bool[] correctBits(bool[] rawbits)
		{
			int num;
			GenericGF field;
			if (ddata.NbLayers <= 2)
			{
				num = 6;
				field = GenericGF.AZTEC_DATA_6;
			}
			else if (ddata.NbLayers <= 8)
			{
				num = 8;
				field = GenericGF.AZTEC_DATA_8;
			}
			else if (ddata.NbLayers <= 22)
			{
				num = 10;
				field = GenericGF.AZTEC_DATA_10;
			}
			else
			{
				num = 12;
				field = GenericGF.AZTEC_DATA_12;
			}
			int nbDatablocks = ddata.NbDatablocks;
			int num2 = rawbits.Length / num;
			int num3 = rawbits.Length % num;
			int twoS = num2 - nbDatablocks;
			int[] array = new int[num2];
			int num4 = 0;
			while (num4 < num2)
			{
				array[num4] = readCode(rawbits, num3, num);
				num4++;
				num3 += num;
			}
			ReedSolomonDecoder reedSolomonDecoder = new ReedSolomonDecoder(field);
			if (!reedSolomonDecoder.decode(array, twoS))
			{
				return null;
			}
			int num5 = (1 << num) - 1;
			int num6 = 0;
			for (int i = 0; i < nbDatablocks; i++)
			{
				int num7 = array[i];
				if (num7 == 0 || num7 == num5)
				{
					return null;
				}
				if (num7 == 1 || num7 == num5 - 1)
				{
					num6++;
				}
			}
			bool[] array2 = new bool[nbDatablocks * num - num6];
			int num8 = 0;
			for (int j = 0; j < nbDatablocks; j++)
			{
				int num9 = array[j];
				if (num9 == 1 || num9 == num5 - 1)
				{
					SupportClass.Fill(array2, num8, num8 + num - 1, num9 > 1);
					num8 += num - 1;
					continue;
				}
				for (int num10 = num - 1; num10 >= 0; num10--)
				{
					array2[num8++] = (num9 & (1 << num10)) != 0;
				}
			}
			if (num8 != array2.Length)
			{
				return null;
			}
			return array2;
		}

		private bool[] extractBits(BitMatrix matrix)
		{
			bool compact = ddata.Compact;
			int nbLayers = ddata.NbLayers;
			int num = ((!compact) ? (14 + nbLayers * 4) : (11 + nbLayers * 4));
			int[] array = new int[num];
			bool[] array2 = new bool[totalBitsInLayer(nbLayers, compact)];
			if (compact)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = i;
				}
			}
			else
			{
				int num2 = num + 1 + 2 * ((num / 2 - 1) / 15);
				int num3 = num / 2;
				int num4 = num2 / 2;
				for (int j = 0; j < num3; j++)
				{
					int num5 = j + j / 15;
					array[num3 - j - 1] = num4 - num5 - 1;
					array[num3 + j] = num4 + num5 + 1;
				}
			}
			int k = 0;
			int num6 = 0;
			for (; k < nbLayers; k++)
			{
				int num7 = ((!compact) ? ((nbLayers - k) * 4 + 12) : ((nbLayers - k) * 4 + 9));
				int num8 = k * 2;
				int num9 = num - 1 - num8;
				for (int l = 0; l < num7; l++)
				{
					int num10 = l * 2;
					for (int m = 0; m < 2; m++)
					{
						array2[num6 + num10 + m] = matrix[array[num8 + m], array[num8 + l]];
						array2[num6 + 2 * num7 + num10 + m] = matrix[array[num8 + l], array[num9 - m]];
						array2[num6 + 4 * num7 + num10 + m] = matrix[array[num9 - m], array[num9 - l]];
						array2[num6 + 6 * num7 + num10 + m] = matrix[array[num9 - l], array[num8 + m]];
					}
				}
				num6 += num7 * 8;
			}
			return array2;
		}

		private static int readCode(bool[] rawbits, int startIndex, int length)
		{
			int num = 0;
			for (int i = startIndex; i < startIndex + length; i++)
			{
				num <<= 1;
				if (rawbits[i])
				{
					num++;
				}
			}
			return num;
		}

		private static int totalBitsInLayer(int layers, bool compact)
		{
			return (((!compact) ? 112 : 88) + 16 * layers) * layers;
		}
	}
}
