using System.Collections.Generic;
using System.Reflection;

namespace Core.MetaData
{
	public class Sheet
	{
		private const int COLUMN_UID = 0;

		private const string COLUMN_PREFIX = "COLUMN_";

		private Column[] columns;

		private Dictionary<string, int> columnIndexes;

		private List<string> extraColumnNames;

		private string[] strings;

		private float[] floats;

		private string[][] stringArrays;

		private int[][] nonNegativeIntArrays;

		private int[][] rawIntArrays;

		private float[][] floatArrays;

		private uint[] cells;

		private int rowCount;

		private int columnCount;

		private int stringCount;

		private int floatCount;

		private int stringArrayCount;

		private int nonNegativeIntArrayCount;

		private int rawIntArrayCount;

		private int floatArrayCount;

		private int cellCount;

		private Dictionary<string, Row> rows;

		public string SheetName { get; private set; }

		public Sheet(string sheetName, string[] strings, float[] floats, string[][] stringArrays, int[][] nonNegativeIntArrays, int[][] rawIntArrays, float[][] floatArrays)
		{
			SheetName = sheetName;
			this.strings = strings;
			this.floats = floats;
			this.stringArrays = stringArrays;
			this.nonNegativeIntArrays = nonNegativeIntArrays;
			this.rawIntArrays = rawIntArrays;
			this.floatArrays = floatArrays;
			stringCount = strings.Length;
			floatCount = floats.Length;
			stringArrayCount = stringArrays.Length;
			nonNegativeIntArrayCount = nonNegativeIntArrays.Length;
			rawIntArrayCount = rawIntArrays.Length;
			floatArrayCount = floatArrays.Length;
		}

		public void SetupColumns(Column[] columns)
		{
			this.columns = columns;
			columnCount = columns.Length;
			columnIndexes = new Dictionary<string, int>(columnCount);
			for (int i = 0; i < columnCount; i++)
			{
				columnIndexes.Add(columns[i].ColName, i);
			}
		}

		public void SetupCells(uint[] cells)
		{
			this.cells = cells;
			cellCount = cells.Length;
		}

		public void SetupComplete()
		{
			rowCount = ((columnCount != 0) ? (cellCount / columnCount) : 0);
			rows = new Dictionary<string, Row>(rowCount);
			for (int i = 0; i < rowCount; i++)
			{
				int rowStartIndex = i * columnCount;
				string value;
				if (InternalGetString(rowStartIndex, 0, out value) && !string.IsNullOrEmpty(value) && !rows.ContainsKey(value))
				{
					Row value2 = new Row(value, this, rowStartIndex);
					rows.Add(value, value2);
				}
			}
		}

		public void PatchRows(Sheet sheet)
		{
			if (!VerifyValid())
			{
				return;
			}
			Dictionary<string, Row> allRows = sheet.GetAllRows();
			if (allRows == null)
			{
				return;
			}
			foreach (KeyValuePair<string, Row> item in allRows)
			{
				string key = item.Key;
				Row value = item.Value;
				value.InternalSetMasterSheet(this);
				if (rows.ContainsKey(key))
				{
					rows[key].PatchColumns(value);
				}
				else
				{
					rows.Add(key, value);
				}
			}
		}

		public void Invalidate()
		{
			columns = null;
			columnIndexes = null;
			extraColumnNames = null;
			strings = null;
			floats = null;
			stringArrays = null;
			nonNegativeIntArrays = null;
			rawIntArrays = null;
			floatArrays = null;
			cells = null;
			rows = null;
		}

		public Dictionary<string, Row> GetAllRows()
		{
			if (!VerifyValid())
			{
				return null;
			}
			return rows;
		}

		public Column[] InternalGetAllColumns()
		{
			return columns;
		}

		public string GetColumnName(int columnIndex)
		{
			if (columnIndex >= 0)
			{
				if (columnIndex < columnCount)
				{
					return columns[columnIndex].ColName;
				}
				if (extraColumnNames != null)
				{
					columnIndex -= columnCount;
					if (columnIndex < extraColumnNames.Count)
					{
						return extraColumnNames[columnIndex];
					}
				}
			}
			return null;
		}

		public int GetColumnIndex(string columnName)
		{
			return (columnName == null || !columnIndexes.ContainsKey(columnName)) ? (-1) : columnIndexes[columnName];
		}

		public void SetupColumnIndexes<T>() where T : IValueObject
		{
			if (!VerifyValid())
			{
				return;
			}
			BindingFlags bindingAttr = BindingFlags.Static | BindingFlags.Public;
			PropertyInfo[] properties = typeof(T).GetProperties(bindingAttr);
			int i = 0;
			for (int num = properties.Length; i < num; i++)
			{
				PropertyInfo propertyInfo = properties[i];
				if (propertyInfo.PropertyType != typeof(int))
				{
					continue;
				}
				string name = propertyInfo.Name;
				if (!name.StartsWith("COLUMN_"))
				{
					continue;
				}
				string text = name.Substring("COLUMN_".Length);
				int num2;
				if (columnIndexes.ContainsKey(text))
				{
					num2 = columnIndexes[text];
				}
				else
				{
					if (extraColumnNames == null)
					{
						extraColumnNames = new List<string>();
					}
					num2 = columnCount + extraColumnNames.Count;
					columnIndexes.Add(text, num2);
					extraColumnNames.Add(text);
				}
				propertyInfo.SetValue(null, num2, null);
			}
		}

		public bool InternalGetString(int rowStartIndex, int columnIndex, out string value)
		{
			value = null;
			if (columnIndex < 0 || columnIndex >= columnCount)
			{
				return false;
			}
			int num = rowStartIndex + columnIndex;
			if (num < 0 || num >= cellCount)
			{
				return false;
			}
			uint num2 = cells[num];
			if (num2 == 0)
			{
				return false;
			}
			Column column = columns[columnIndex];
			switch (column.ColType)
			{
			case ColumnType.String:
			{
				int num5 = (int)(num2 - 1);
				if (num5 < 0 || num5 >= stringCount)
				{
					return false;
				}
				value = strings[num5];
				return true;
			}
			case ColumnType.Boolean:
			{
				uint num4 = num2 - 1;
				value = (num4 != 0).ToString();
				return true;
			}
			case ColumnType.NonNegativeInt:
			case ColumnType.RawInt:
				value = ((int)(((num2 & 0x80000000u) != 0) ? num2 : (num2 - 1))).ToString();
				return true;
			case ColumnType.Float:
			{
				int num3 = (int)(num2 - 1);
				if (num3 < 0 || num3 >= floatCount)
				{
					return false;
				}
				value = floats[num3].ToString();
				return true;
			}
			case ColumnType.StringArray:
			case ColumnType.NonNegativeIntArray:
			case ColumnType.RawIntArray:
			case ColumnType.FloatArray:
				return false;
			default:
				return false;
			}
		}

		public bool InternalGetStringArray(int rowStartIndex, int columnIndex, out string[] value)
		{
			value = null;
			if (columnIndex < 0 || columnIndex >= columnCount)
			{
				return false;
			}
			int num = rowStartIndex + columnIndex;
			if (num < 0 || num >= cellCount)
			{
				return false;
			}
			uint num2 = cells[num];
			if (num2 == 0)
			{
				return false;
			}
			Column column = columns[columnIndex];
			ColumnType colType = column.ColType;
			if (colType == ColumnType.StringArray)
			{
				int num3 = (int)(num2 - 1);
				if (num3 < 0 || num3 >= stringArrayCount)
				{
					return false;
				}
				value = stringArrays[num3];
				return true;
			}
			return false;
		}

		public bool InternalGetIntArray(int rowStartIndex, int columnIndex, out int[] value)
		{
			value = null;
			if (columnIndex < 0 || columnIndex >= columnCount)
			{
				return false;
			}
			int num = rowStartIndex + columnIndex;
			if (num < 0 || num >= cellCount)
			{
				return false;
			}
			uint num2 = cells[num];
			if (num2 == 0)
			{
				return false;
			}
			Column column = columns[columnIndex];
			switch (column.ColType)
			{
			case ColumnType.NonNegativeIntArray:
			{
				int num4 = (int)(num2 - 1);
				if (num4 < 0 || num4 >= nonNegativeIntArrayCount)
				{
					return false;
				}
				value = nonNegativeIntArrays[num4];
				return true;
			}
			case ColumnType.RawIntArray:
			{
				int num3 = (int)(num2 - 1);
				if (num3 < 0 || num3 >= rawIntArrayCount)
				{
					return false;
				}
				value = rawIntArrays[num3];
				return true;
			}
			default:
				return false;
			}
		}

		public bool InternalGetFloatArray(int rowStartIndex, int columnIndex, out float[] value)
		{
			value = null;
			if (columnIndex < 0 || columnIndex >= columnCount)
			{
				return false;
			}
			int num = rowStartIndex + columnIndex;
			if (num < 0 || num >= cellCount)
			{
				return false;
			}
			uint num2 = cells[num];
			if (num2 == 0)
			{
				return false;
			}
			Column column = columns[columnIndex];
			ColumnType colType = column.ColType;
			if (colType == ColumnType.FloatArray)
			{
				int num3 = (int)(num2 - 1);
				if (num3 < 0 || num3 >= floatArrayCount)
				{
					return false;
				}
				value = floatArrays[num3];
				return true;
			}
			return false;
		}

		public bool InternalGetBool(int rowStartIndex, int columnIndex, out bool value)
		{
			value = false;
			if (columnIndex < 0 || columnIndex >= columnCount)
			{
				return false;
			}
			int num = rowStartIndex + columnIndex;
			if (num < 0 || num >= cellCount)
			{
				return false;
			}
			uint num2 = cells[num];
			if (num2 == 0)
			{
				return false;
			}
			Column column = columns[columnIndex];
			switch (column.ColType)
			{
			case ColumnType.String:
			case ColumnType.StringArray:
			case ColumnType.NonNegativeIntArray:
			case ColumnType.RawIntArray:
			case ColumnType.FloatArray:
				value = true;
				return true;
			case ColumnType.Boolean:
			case ColumnType.NonNegativeInt:
			{
				uint num4 = num2 - 1;
				value = num4 != 0;
				return true;
			}
			case ColumnType.RawInt:
				value = (((num2 & 0x80000000u) != 0) ? num2 : (num2 - 1)) != 0;
				return true;
			case ColumnType.Float:
			{
				int num3 = (int)(num2 - 1);
				if (num3 < 0 || num3 >= floatCount)
				{
					return false;
				}
				value = floats[num3] != 0f;
				return true;
			}
			default:
				return false;
			}
		}

		public bool InternalGetInt(int rowStartIndex, int columnIndex, out int value)
		{
			value = 0;
			if (columnIndex < 0 || columnIndex >= columnCount)
			{
				return false;
			}
			int num = rowStartIndex + columnIndex;
			if (num < 0 || num >= cellCount)
			{
				return false;
			}
			uint num2 = cells[num];
			if (num2 == 0)
			{
				return false;
			}
			Column column = columns[columnIndex];
			switch (column.ColType)
			{
			case ColumnType.String:
			{
				int num4 = (int)(num2 - 1);
				if (num4 < 0 || num4 >= stringCount)
				{
					return false;
				}
				return int.TryParse(strings[num4], out value);
			}
			case ColumnType.Boolean:
			case ColumnType.NonNegativeInt:
			case ColumnType.RawInt:
				value = (int)(((num2 & 0x80000000u) != 0) ? num2 : (num2 - 1));
				return true;
			case ColumnType.Float:
			{
				int num3 = (int)(num2 - 1);
				if (num3 < 0 || num3 >= floatCount)
				{
					return false;
				}
				value = (int)floats[num3];
				return true;
			}
			case ColumnType.StringArray:
			case ColumnType.NonNegativeIntArray:
			case ColumnType.RawIntArray:
			case ColumnType.FloatArray:
				return false;
			default:
				return false;
			}
		}

		public bool InternalGetUint(int rowStartIndex, int columnIndex, out uint value)
		{
			value = 0u;
			if (columnIndex < 0 || columnIndex >= columnCount)
			{
				return false;
			}
			int num = rowStartIndex + columnIndex;
			if (num < 0 || num >= cellCount)
			{
				return false;
			}
			uint num2 = cells[num];
			if (num2 == 0)
			{
				return false;
			}
			Column column = columns[columnIndex];
			switch (column.ColType)
			{
			case ColumnType.String:
			{
				int num4 = (int)(num2 - 1);
				if (num4 < 0 || num4 >= stringCount)
				{
					return false;
				}
				return uint.TryParse(strings[num4], out value);
			}
			case ColumnType.Boolean:
			case ColumnType.NonNegativeInt:
			case ColumnType.RawInt:
				value = (((num2 & 0x80000000u) != 0) ? num2 : (num2 - 1));
				return true;
			case ColumnType.Float:
			{
				int num3 = (int)(num2 - 1);
				if (num3 < 0 || num3 >= floatCount)
				{
					return false;
				}
				value = (uint)floats[num3];
				return true;
			}
			case ColumnType.StringArray:
			case ColumnType.NonNegativeIntArray:
			case ColumnType.RawIntArray:
			case ColumnType.FloatArray:
				return false;
			default:
				return false;
			}
		}

		public bool InternalGetFloat(int rowStartIndex, int columnIndex, out float value)
		{
			value = 0f;
			if (columnIndex < 0 || columnIndex >= columnCount)
			{
				return false;
			}
			int num = rowStartIndex + columnIndex;
			if (num < 0 || num >= cellCount)
			{
				return false;
			}
			uint num2 = cells[num];
			if (num2 == 0)
			{
				return false;
			}
			Column column = columns[columnIndex];
			switch (column.ColType)
			{
			case ColumnType.String:
			{
				int num4 = (int)(num2 - 1);
				if (num4 < 0 || num4 >= stringCount)
				{
					return false;
				}
				return float.TryParse(strings[num4], out value);
			}
			case ColumnType.Boolean:
			case ColumnType.NonNegativeInt:
			case ColumnType.RawInt:
				value = (int)(((num2 & 0x80000000u) != 0) ? num2 : (num2 - 1));
				return true;
			case ColumnType.Float:
			{
				int num3 = (int)(num2 - 1);
				if (num3 < 0 || num3 >= floatCount)
				{
					return false;
				}
				value = floats[num3];
				return true;
			}
			case ColumnType.StringArray:
			case ColumnType.NonNegativeIntArray:
			case ColumnType.RawIntArray:
			case ColumnType.FloatArray:
				return false;
			default:
				return false;
			}
		}

		private bool VerifyValid()
		{
			if (rows == null)
			{
				return false;
			}
			return true;
		}
	}
}
