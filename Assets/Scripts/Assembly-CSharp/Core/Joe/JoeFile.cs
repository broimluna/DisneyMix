using System;
using System.Text;
using Core.MetaData;

namespace Core.Joe
{
	public class JoeFile
	{
		private const int MAX_SUPPORTED_VERSION = 1;

		private const int EXTRA_ARRAYS_VERSION = 1;

		private Sheet[] sheets;

		private byte[] bytes;

		private int pos;

		private int rem;

		private int ver = -1;

		private string[] strings;

		private int[] ints;

		private float[] floats;

		private string[][] stringArrays;

		private int[][] nonNegativeIntArrays;

		private int[][] rawIntArrays;

		private float[][] floatArrays;

		private int stringCount;

		private int intCount;

		private int floatCount;

		private int stringArrayCount;

		private int nonNegativeIntArrayCount;

		private int rawIntArrayCount;

		private int floatArrayCount;

		public JoeFile(byte[] rawFileBytes)
		{
			bytes = rawFileBytes;
			if (bytes != null)
			{
				Parse();
				bytes = null;
			}
		}

		public Sheet GetSheet(int i)
		{
			return (sheets != null && i >= 0 && i < sheets.Length) ? sheets[i] : null;
		}

		public Sheet[] GetAllSheets()
		{
			return sheets;
		}

		private bool FatalError(string error)
		{
			sheets = null;
			bytes = null;
			strings = null;
			ints = null;
			floats = null;
			stringArrays = null;
			nonNegativeIntArrays = null;
			rawIntArrays = null;
			floatArrays = null;
			return false;
		}

		private bool RangeError(string kind, int index, int count)
		{
			return FatalError(string.Format("invalid {0} index: {1}:{2}", kind, index, count));
		}

		private bool Parse()
		{
			int num = bytes.Length;
			pos = 0;
			rem = num;
			ver = -1;
			if (!ParseSignature())
			{
				return FatalError("invalid signature");
			}
			if (!ParseVersion())
			{
				return FatalError("unsupported version: " + ver);
			}
			if (!ParseStringTable())
			{
				return FatalError("unable to parse string table");
			}
			if (!ParseIntegerTable())
			{
				return FatalError("unable to parse integer table");
			}
			if (!ParseFloatTable())
			{
				return FatalError("unable to parse float table");
			}
			if (!ParseStringArrayTable())
			{
				return FatalError("unable to parse string array table");
			}
			if (ver >= 1)
			{
				if (!ParseNonNegativeIntArrayTable())
				{
					return FatalError("unable to parse non-negative int array table");
				}
				if (!ParseRawIntArrayTable())
				{
					return FatalError("unable to parse raw int array table");
				}
				if (!ParseFloatArrayTable())
				{
					return FatalError("unable to parse float array table");
				}
			}
			if (!ParseSheetNames())
			{
				return FatalError("unable to parse sheet names");
			}
			int i = 0;
			for (int num2 = sheets.Length; i < num2; i++)
			{
				Sheet sheet = sheets[i];
				if (!ParseSheetColumns(sheet))
				{
					return FatalError("unable to parse sheet columns: " + sheet.SheetName);
				}
				if (!ParseSheetCells(sheet))
				{
					return FatalError("unable to parse sheet cells: " + sheet.SheetName);
				}
				sheet.SetupComplete();
			}
			if (!ParseFinalByte())
			{
				return FatalError(string.Format("unable to parse final byte {0},{1},{2}", rem, pos, num));
			}
			if (rem != 0 || pos != num)
			{
			}
			return true;
		}

		private bool ParseSignature()
		{
			if ((rem -= 3) < 0)
			{
				return FatalError("not enough bytes for signature");
			}
			if (bytes[pos++] != 74)
			{
				return FatalError("invalid first signature byte");
			}
			if (bytes[pos++] != 79)
			{
				return FatalError("invalid second signature byte");
			}
			if (bytes[pos++] != 69)
			{
				return FatalError("invalid third signature byte");
			}
			return true;
		}

		private bool ParseVersion()
		{
			if (--rem < 0)
			{
				return FatalError("not enough bytes for version");
			}
			ver = bytes[pos++];
			if (ver > 1)
			{
				return FatalError(string.Format("unsupported version {0} > {1}", ver, 1));
			}
			return true;
		}

		private bool ParseStringTable()
		{
			if (!DecodeDword(out stringCount) || stringCount < 0)
			{
				return FatalError("invalid string table size");
			}
			strings = new string[stringCount];
			for (int i = 0; i < stringCount; i++)
			{
				uint val;
				if (!DecodeVariableLength(out val))
				{
					return FatalError("unable to decode string length");
				}
				int num = (int)val;
				if (num < 0)
				{
					return FatalError("invalid string length");
				}
				if ((rem -= num) < 0)
				{
					return FatalError("not enough bytes for string");
				}
				strings[i] = Encoding.UTF8.GetString(bytes, pos, num);
				pos += num;
			}
			return true;
		}

		private bool ParseIntegerTable()
		{
			if (!DecodeDword(out intCount) || intCount < 0)
			{
				return FatalError("invalid integer table size");
			}
			ints = new int[intCount];
			for (int i = 0; i < intCount; i++)
			{
				if ((rem -= 4) < 0)
				{
					return FatalError("not enough bytes for integer");
				}
				ints[i] = BitConverter.ToInt32(bytes, pos);
				pos += 4;
			}
			return true;
		}

		private bool ParseFloatTable()
		{
			if (!DecodeDword(out floatCount) || floatCount < 0)
			{
				return FatalError("invalid float table size");
			}
			floats = new float[floatCount];
			for (int i = 0; i < floatCount; i++)
			{
				if ((rem -= 4) < 0)
				{
					return FatalError("not enough bytes for float");
				}
				floats[i] = BitConverter.ToSingle(bytes, pos);
				pos += 4;
			}
			return true;
		}

		private bool ParseStringArrayTable()
		{
			if (!DecodeDword(out stringArrayCount) || stringArrayCount < 0)
			{
				return FatalError("invalid stringArray table size");
			}
			stringArrays = new string[stringArrayCount][];
			for (int i = 0; i < stringArrayCount; i++)
			{
				uint val;
				if (!DecodeVariableLength(out val))
				{
					return FatalError("unable to decode stringArray length");
				}
				int num = (int)val;
				if (num < 0)
				{
					return FatalError("invalid stringArray length");
				}
				string[] array = new string[num];
				for (int j = 0; j < num; j++)
				{
					if (!DecodeVariableLength(out val))
					{
						return FatalError("unable to decode stringArray string index");
					}
					int num2 = (int)val;
					if (num2 < 0 || num2 > stringCount)
					{
						return RangeError("stringArray string", num2, stringCount);
					}
					array[j] = strings[num2];
				}
				stringArrays[i] = array;
			}
			return true;
		}

		private bool ParseNonNegativeIntArrayTable()
		{
			if (!DecodeDword(out nonNegativeIntArrayCount) || nonNegativeIntArrayCount < 0)
			{
				return FatalError("invalid nonNegativeIntArray table size");
			}
			nonNegativeIntArrays = new int[nonNegativeIntArrayCount][];
			for (int i = 0; i < nonNegativeIntArrayCount; i++)
			{
				uint val;
				if (!DecodeVariableLength(out val))
				{
					return FatalError("unable to decode nonNegativeIntArray length");
				}
				int num = (int)val;
				if (num < 0)
				{
					return FatalError("invalid nonNegativeIntArray length");
				}
				int[] array = new int[num];
				for (int j = 0; j < num; j++)
				{
					if (!DecodeVariableLength(out val))
					{
						return FatalError("unable to decode nonNegativeIntArray int");
					}
					array[j] = (int)val;
				}
				nonNegativeIntArrays[i] = array;
			}
			return true;
		}

		private bool ParseRawIntArrayTable()
		{
			if (!DecodeDword(out rawIntArrayCount) || rawIntArrayCount < 0)
			{
				return FatalError("invalid rawIntArray table size");
			}
			rawIntArrays = new int[rawIntArrayCount][];
			for (int i = 0; i < rawIntArrayCount; i++)
			{
				uint val;
				if (!DecodeVariableLength(out val))
				{
					return FatalError("unable to decode rawIntArray length");
				}
				int num = (int)val;
				if (num < 0)
				{
					return FatalError("invalid rawIntArray length");
				}
				int[] array = new int[num];
				for (int j = 0; j < num; j++)
				{
					if (!DecodeVariableLength(out val))
					{
						return FatalError("unable to decode rawIntArray int index");
					}
					int num2 = (int)val;
					if (num2 < 0 || num2 > intCount)
					{
						return RangeError("rawIntArray int", num2, intCount);
					}
					array[j] = ints[num2];
				}
				rawIntArrays[i] = array;
			}
			return true;
		}

		private bool ParseFloatArrayTable()
		{
			if (!DecodeDword(out floatArrayCount) || floatArrayCount < 0)
			{
				return FatalError("invalid floatArray table size");
			}
			floatArrays = new float[floatArrayCount][];
			for (int i = 0; i < floatArrayCount; i++)
			{
				uint val;
				if (!DecodeVariableLength(out val))
				{
					return FatalError("unable to decode floatArray length");
				}
				int num = (int)val;
				if (num < 0)
				{
					return FatalError("invalid floatArray length");
				}
				float[] array = new float[num];
				for (int j = 0; j < num; j++)
				{
					if (!DecodeVariableLength(out val))
					{
						return FatalError("unable to decode floatArray float index");
					}
					int num2 = (int)val;
					if (num2 < 0 || num2 > floatCount)
					{
						return RangeError("floatArray float", num2, floatCount);
					}
					array[j] = floats[num2];
				}
				floatArrays[i] = array;
			}
			return true;
		}

		private bool ParseSheetNames()
		{
			uint val;
			if (!DecodeVariableLength(out val))
			{
				return FatalError("unable to decode sheet count");
			}
			int num = (int)val;
			if (num < 0)
			{
				return FatalError("invalid sheet count");
			}
			sheets = new Sheet[num];
			for (int i = 0; i < num; i++)
			{
				if (!DecodeVariableLength(out val))
				{
					return FatalError("unable to decode sheet string index");
				}
				int num2 = (int)val;
				if (num2 < 0 || num2 > stringCount)
				{
					return RangeError("sheet string", num2, stringCount);
				}
				string sheetName = strings[num2];
				Sheet sheet = new Sheet(sheetName, strings, floats, stringArrays, nonNegativeIntArrays, rawIntArrays, floatArrays);
				sheets[i] = sheet;
			}
			return true;
		}

		private bool ParseSheetColumns(Sheet sheet)
		{
			uint val;
			if (!DecodeVariableLength(out val))
			{
				return FatalError("unable to decode column count");
			}
			int num = (int)val;
			if (num < 0)
			{
				return FatalError("invalid column count");
			}
			Column[] array = new Column[num];
			for (int i = 0; i < num; i++)
			{
				if (--rem < 0)
				{
					return FatalError("not enough bytes for column type");
				}
				ColumnType colType = (ColumnType)bytes[pos++];
				if (!DecodeVariableLength(out val))
				{
					return FatalError("unable to decode column string index");
				}
				int num2 = (int)val;
				if (num2 < 0 || num2 > stringCount)
				{
					return RangeError("column string", num2, stringCount);
				}
				string colName = strings[num2];
				array[i] = new Column(colName, colType);
			}
			sheet.SetupColumns(array);
			return true;
		}

		private bool ParseSheetCells(Sheet sheet)
		{
			uint val;
			if (!DecodeVariableLength(out val))
			{
				return FatalError("unable to decode cell count");
			}
			int num = (int)val;
			if (num < 0)
			{
				return FatalError("invalid cell count");
			}
			uint[] array = new uint[num];
			Column[] array2 = sheet.InternalGetAllColumns();
			int num2 = array2.Length;
			int num3 = 0;
			if (num2 == 0 || num % num2 != 0)
			{
				return FatalError(string.Format("cell count {0} is not a multiple of column count {1}", num, num2));
			}
			for (int i = 0; i < num; i++)
			{
				if (rem == 0)
				{
					return FatalError("not enough bytes for cell");
				}
				if (bytes[pos] == 0)
				{
					array[i] = 0u;
					pos++;
					rem--;
				}
				else
				{
					ColumnType colType = array2[num3].ColType;
					uint cellValue;
					switch (colType)
					{
					case ColumnType.String:
						if (!GetCellValueForTableLookup(out cellValue, colType, stringCount))
						{
							return false;
						}
						break;
					case ColumnType.Boolean:
						if (--rem < 0)
						{
							return FatalError("not enough bytes for bool cell");
						}
						cellValue = bytes[pos++];
						break;
					case ColumnType.NonNegativeInt:
						if (!DecodeVariableLength(out cellValue))
						{
							return FatalError("unable to decode non-negative int cell");
						}
						break;
					case ColumnType.RawInt:
					{
						if (!DecodeVariableLength(out cellValue))
						{
							return FatalError("unable to decode raw int index");
						}
						int num4 = (int)cellValue;
						if (num4 <= 0 || num4 > intCount)
						{
							return RangeError("raw int", num4, intCount);
						}
						cellValue = (uint)ints[num4 - 1];
						if ((cellValue & 0x80000000u) == 0)
						{
							cellValue++;
						}
						break;
					}
					case ColumnType.Float:
						if (!GetCellValueForTableLookup(out cellValue, colType, floatCount))
						{
							return false;
						}
						break;
					case ColumnType.StringArray:
						if (!GetCellValueForTableLookup(out cellValue, colType, stringArrayCount))
						{
							return false;
						}
						break;
					case ColumnType.NonNegativeIntArray:
						if (!GetCellValueForTableLookup(out cellValue, colType, nonNegativeIntArrayCount))
						{
							return false;
						}
						break;
					case ColumnType.RawIntArray:
						if (!GetCellValueForTableLookup(out cellValue, colType, rawIntArrayCount))
						{
							return false;
						}
						break;
					case ColumnType.FloatArray:
						if (!GetCellValueForTableLookup(out cellValue, colType, floatArrayCount))
						{
							return false;
						}
						break;
					default:
						return FatalError("unsupported column type");
					}
					array[i] = cellValue;
				}
				if (++num3 == num2)
				{
					num3 = 0;
				}
			}
			sheet.SetupCells(array);
			return true;
		}

		private bool GetCellValueForTableLookup(out uint cellValue, ColumnType colType, int count)
		{
			if (!DecodeVariableLength(out cellValue))
			{
				return FatalError(string.Format("unable to decode cell {0} index", colType));
			}
			int num = (int)cellValue;
			if (num <= 0 || num > count)
			{
				return RangeError(string.Format("cell {0}", colType), num, count);
			}
			return true;
		}

		private bool ParseFinalByte()
		{
			if (--rem < 0)
			{
				return FatalError("not enough bytes for final byte");
			}
			if (bytes[pos++] != 0)
			{
				return FatalError("invalid final byte");
			}
			return true;
		}

		private bool DecodeDword(out int val)
		{
			val = 0;
			if ((rem -= 4) < 0)
			{
				return FatalError("not enough bytes for dword");
			}
			val = BitConverter.ToInt32(bytes, pos);
			pos += 4;
			return true;
		}

		private bool DecodeVariableLength(out uint val)
		{
			val = 0u;
			uint num;
			while (true)
			{
				if (--rem < 0)
				{
					return FatalError("not enough bytes for variable-length");
				}
				num = bytes[pos++];
				if ((num & 0x80) == 0)
				{
					break;
				}
				val = (val << 7) | (num & 0x7F);
			}
			val = (val << 7) | num;
			return true;
		}
	}
}
