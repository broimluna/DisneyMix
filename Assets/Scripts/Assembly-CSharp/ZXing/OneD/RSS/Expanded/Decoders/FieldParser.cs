using System.Collections.Generic;

namespace ZXing.OneD.RSS.Expanded.Decoders
{
	internal static class FieldParser
	{
		private static readonly object VARIABLE_LENGTH;

		private static readonly IDictionary<string, object[]> TWO_DIGIT_DATA_LENGTH;

		private static readonly IDictionary<string, object[]> THREE_DIGIT_DATA_LENGTH;

		private static readonly IDictionary<string, object[]> THREE_DIGIT_PLUS_DIGIT_DATA_LENGTH;

		private static readonly IDictionary<string, object[]> FOUR_DIGIT_DATA_LENGTH;

		static FieldParser()
		{
			VARIABLE_LENGTH = new object();
			TWO_DIGIT_DATA_LENGTH = new Dictionary<string, object[]>
			{
				{
					"00",
					new object[1] { 18 }
				},
				{
					"01",
					new object[1] { 14 }
				},
				{
					"02",
					new object[1] { 14 }
				},
				{
					"10",
					new object[2] { VARIABLE_LENGTH, 20 }
				},
				{
					"11",
					new object[1] { 6 }
				},
				{
					"12",
					new object[1] { 6 }
				},
				{
					"13",
					new object[1] { 6 }
				},
				{
					"15",
					new object[1] { 6 }
				},
				{
					"17",
					new object[1] { 6 }
				},
				{
					"20",
					new object[1] { 2 }
				},
				{
					"21",
					new object[2] { VARIABLE_LENGTH, 20 }
				},
				{
					"22",
					new object[2] { VARIABLE_LENGTH, 29 }
				},
				{
					"30",
					new object[2] { VARIABLE_LENGTH, 8 }
				},
				{
					"37",
					new object[2] { VARIABLE_LENGTH, 8 }
				},
				{
					"90",
					new object[2] { VARIABLE_LENGTH, 30 }
				},
				{
					"91",
					new object[2] { VARIABLE_LENGTH, 30 }
				},
				{
					"92",
					new object[2] { VARIABLE_LENGTH, 30 }
				},
				{
					"93",
					new object[2] { VARIABLE_LENGTH, 30 }
				},
				{
					"94",
					new object[2] { VARIABLE_LENGTH, 30 }
				},
				{
					"95",
					new object[2] { VARIABLE_LENGTH, 30 }
				},
				{
					"96",
					new object[2] { VARIABLE_LENGTH, 30 }
				},
				{
					"97",
					new object[2] { VARIABLE_LENGTH, 30 }
				},
				{
					"98",
					new object[2] { VARIABLE_LENGTH, 30 }
				},
				{
					"99",
					new object[2] { VARIABLE_LENGTH, 30 }
				}
			};
			THREE_DIGIT_DATA_LENGTH = new Dictionary<string, object[]>
			{
				{
					"240",
					new object[2] { VARIABLE_LENGTH, 30 }
				},
				{
					"241",
					new object[2] { VARIABLE_LENGTH, 30 }
				},
				{
					"242",
					new object[2] { VARIABLE_LENGTH, 6 }
				},
				{
					"250",
					new object[2] { VARIABLE_LENGTH, 30 }
				},
				{
					"251",
					new object[2] { VARIABLE_LENGTH, 30 }
				},
				{
					"253",
					new object[2] { VARIABLE_LENGTH, 17 }
				},
				{
					"254",
					new object[2] { VARIABLE_LENGTH, 20 }
				},
				{
					"400",
					new object[2] { VARIABLE_LENGTH, 30 }
				},
				{
					"401",
					new object[2] { VARIABLE_LENGTH, 30 }
				},
				{
					"402",
					new object[1] { 17 }
				},
				{
					"403",
					new object[2] { VARIABLE_LENGTH, 30 }
				},
				{
					"410",
					new object[1] { 13 }
				},
				{
					"411",
					new object[1] { 13 }
				},
				{
					"412",
					new object[1] { 13 }
				},
				{
					"413",
					new object[1] { 13 }
				},
				{
					"414",
					new object[1] { 13 }
				},
				{
					"420",
					new object[2] { VARIABLE_LENGTH, 20 }
				},
				{
					"421",
					new object[2] { VARIABLE_LENGTH, 15 }
				},
				{
					"422",
					new object[1] { 3 }
				},
				{
					"423",
					new object[2] { VARIABLE_LENGTH, 15 }
				},
				{
					"424",
					new object[1] { 3 }
				},
				{
					"425",
					new object[1] { 3 }
				},
				{
					"426",
					new object[1] { 3 }
				}
			};
			THREE_DIGIT_PLUS_DIGIT_DATA_LENGTH = new Dictionary<string, object[]>
			{
				{
					"310",
					new object[1] { 6 }
				},
				{
					"311",
					new object[1] { 6 }
				},
				{
					"312",
					new object[1] { 6 }
				},
				{
					"313",
					new object[1] { 6 }
				},
				{
					"314",
					new object[1] { 6 }
				},
				{
					"315",
					new object[1] { 6 }
				},
				{
					"316",
					new object[1] { 6 }
				},
				{
					"320",
					new object[1] { 6 }
				},
				{
					"321",
					new object[1] { 6 }
				},
				{
					"322",
					new object[1] { 6 }
				},
				{
					"323",
					new object[1] { 6 }
				},
				{
					"324",
					new object[1] { 6 }
				},
				{
					"325",
					new object[1] { 6 }
				},
				{
					"326",
					new object[1] { 6 }
				},
				{
					"327",
					new object[1] { 6 }
				},
				{
					"328",
					new object[1] { 6 }
				},
				{
					"329",
					new object[1] { 6 }
				},
				{
					"330",
					new object[1] { 6 }
				},
				{
					"331",
					new object[1] { 6 }
				},
				{
					"332",
					new object[1] { 6 }
				},
				{
					"333",
					new object[1] { 6 }
				},
				{
					"334",
					new object[1] { 6 }
				},
				{
					"335",
					new object[1] { 6 }
				},
				{
					"336",
					new object[1] { 6 }
				},
				{
					"340",
					new object[1] { 6 }
				},
				{
					"341",
					new object[1] { 6 }
				},
				{
					"342",
					new object[1] { 6 }
				},
				{
					"343",
					new object[1] { 6 }
				},
				{
					"344",
					new object[1] { 6 }
				},
				{
					"345",
					new object[1] { 6 }
				},
				{
					"346",
					new object[1] { 6 }
				},
				{
					"347",
					new object[1] { 6 }
				},
				{
					"348",
					new object[1] { 6 }
				},
				{
					"349",
					new object[1] { 6 }
				},
				{
					"350",
					new object[1] { 6 }
				},
				{
					"351",
					new object[1] { 6 }
				},
				{
					"352",
					new object[1] { 6 }
				},
				{
					"353",
					new object[1] { 6 }
				},
				{
					"354",
					new object[1] { 6 }
				},
				{
					"355",
					new object[1] { 6 }
				},
				{
					"356",
					new object[1] { 6 }
				},
				{
					"357",
					new object[1] { 6 }
				},
				{
					"360",
					new object[1] { 6 }
				},
				{
					"361",
					new object[1] { 6 }
				},
				{
					"362",
					new object[1] { 6 }
				},
				{
					"363",
					new object[1] { 6 }
				},
				{
					"364",
					new object[1] { 6 }
				},
				{
					"365",
					new object[1] { 6 }
				},
				{
					"366",
					new object[1] { 6 }
				},
				{
					"367",
					new object[1] { 6 }
				},
				{
					"368",
					new object[1] { 6 }
				},
				{
					"369",
					new object[1] { 6 }
				},
				{
					"390",
					new object[2] { VARIABLE_LENGTH, 15 }
				},
				{
					"391",
					new object[2] { VARIABLE_LENGTH, 18 }
				},
				{
					"392",
					new object[2] { VARIABLE_LENGTH, 15 }
				},
				{
					"393",
					new object[2] { VARIABLE_LENGTH, 18 }
				},
				{
					"703",
					new object[2] { VARIABLE_LENGTH, 30 }
				}
			};
			FOUR_DIGIT_DATA_LENGTH = new Dictionary<string, object[]>
			{
				{
					"7001",
					new object[1] { 13 }
				},
				{
					"7002",
					new object[2] { VARIABLE_LENGTH, 30 }
				},
				{
					"7003",
					new object[1] { 10 }
				},
				{
					"8001",
					new object[1] { 14 }
				},
				{
					"8002",
					new object[2] { VARIABLE_LENGTH, 20 }
				},
				{
					"8003",
					new object[2] { VARIABLE_LENGTH, 30 }
				},
				{
					"8004",
					new object[2] { VARIABLE_LENGTH, 30 }
				},
				{
					"8005",
					new object[1] { 6 }
				},
				{
					"8006",
					new object[1] { 18 }
				},
				{
					"8007",
					new object[2] { VARIABLE_LENGTH, 30 }
				},
				{
					"8008",
					new object[2] { VARIABLE_LENGTH, 12 }
				},
				{
					"8018",
					new object[1] { 18 }
				},
				{
					"8020",
					new object[2] { VARIABLE_LENGTH, 25 }
				},
				{
					"8100",
					new object[1] { 6 }
				},
				{
					"8101",
					new object[1] { 10 }
				},
				{
					"8102",
					new object[1] { 2 }
				},
				{
					"8110",
					new object[2] { VARIABLE_LENGTH, 70 }
				},
				{
					"8200",
					new object[2] { VARIABLE_LENGTH, 70 }
				}
			};
		}

		internal static string parseFieldsInGeneralPurpose(string rawInformation)
		{
			if (string.IsNullOrEmpty(rawInformation))
			{
				return null;
			}
			if (rawInformation.Length < 2)
			{
				return null;
			}
			string key = rawInformation.Substring(0, 2);
			if (TWO_DIGIT_DATA_LENGTH.ContainsKey(key))
			{
				object[] array = TWO_DIGIT_DATA_LENGTH[key];
				if (array[0] == VARIABLE_LENGTH)
				{
					return processVariableAI(2, (int)array[1], rawInformation);
				}
				return processFixedAI(2, (int)array[0], rawInformation);
			}
			if (rawInformation.Length < 3)
			{
				return null;
			}
			string key2 = rawInformation.Substring(0, 3);
			if (THREE_DIGIT_DATA_LENGTH.ContainsKey(key2))
			{
				object[] array2 = THREE_DIGIT_DATA_LENGTH[key2];
				if (array2[0] == VARIABLE_LENGTH)
				{
					return processVariableAI(3, (int)array2[1], rawInformation);
				}
				return processFixedAI(3, (int)array2[0], rawInformation);
			}
			if (THREE_DIGIT_PLUS_DIGIT_DATA_LENGTH.ContainsKey(key2))
			{
				object[] array3 = THREE_DIGIT_PLUS_DIGIT_DATA_LENGTH[key2];
				if (array3[0] == VARIABLE_LENGTH)
				{
					return processVariableAI(4, (int)array3[1], rawInformation);
				}
				return processFixedAI(4, (int)array3[0], rawInformation);
			}
			if (rawInformation.Length < 4)
			{
				return null;
			}
			string key3 = rawInformation.Substring(0, 4);
			if (FOUR_DIGIT_DATA_LENGTH.ContainsKey(key3))
			{
				object[] array4 = FOUR_DIGIT_DATA_LENGTH[key3];
				if (array4[0] == VARIABLE_LENGTH)
				{
					return processVariableAI(4, (int)array4[1], rawInformation);
				}
				return processFixedAI(4, (int)array4[0], rawInformation);
			}
			return null;
		}

		private static string processFixedAI(int aiSize, int fieldSize, string rawInformation)
		{
			if (rawInformation.Length < aiSize)
			{
				return null;
			}
			string text = rawInformation.Substring(0, aiSize);
			if (rawInformation.Length < aiSize + fieldSize)
			{
				return null;
			}
			string text2 = rawInformation.Substring(aiSize, fieldSize);
			string rawInformation2 = rawInformation.Substring(aiSize + fieldSize);
			string text3 = '(' + text + ')' + text2;
			string text4 = parseFieldsInGeneralPurpose(rawInformation2);
			return (text4 != null) ? (text3 + text4) : text3;
		}

		private static string processVariableAI(int aiSize, int variableFieldSize, string rawInformation)
		{
			string text = rawInformation.Substring(0, aiSize);
			int num = ((rawInformation.Length >= aiSize + variableFieldSize) ? (aiSize + variableFieldSize) : rawInformation.Length);
			string text2 = rawInformation.Substring(aiSize, num - aiSize);
			string rawInformation2 = rawInformation.Substring(num);
			string text3 = '(' + text + ')' + text2;
			string text4 = parseFieldsInGeneralPurpose(rawInformation2);
			return (text4 != null) ? (text3 + text4) : text3;
		}
	}
}
