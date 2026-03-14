using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Core.MetaData
{
	public class Row
	{
		private Sheet dataSheet;

		private Sheet masterSheet;

		private int startIndex;

		private List<Row> patchRows;

		private bool hasPatches;

		private bool remapColumns;

		public string Uid { get; private set; }

		public Row(string uid, Sheet sheet, int rowStartIndex)
		{
			Uid = uid;
			dataSheet = sheet;
			masterSheet = sheet;
			startIndex = rowStartIndex;
			patchRows = null;
			hasPatches = false;
			remapColumns = false;
		}

		public void PatchColumns(Row row)
		{
			if (!row.hasPatches)
			{
				if (!hasPatches)
				{
					patchRows = new List<Row>();
					hasPatches = true;
				}
				patchRows.Add(row);
			}
		}

		public void InternalSetMasterSheet(Sheet sheet)
		{
			masterSheet = sheet;
			remapColumns = masterSheet != dataSheet;
		}

		public void Invalidate()
		{
			dataSheet = null;
			masterSheet = null;
			patchRows = null;
		}

		public string TryGetString(int column)
		{
			return TryGetString(column, null);
		}

		public string TryGetString(int column, string fallback)
		{
			string value;
			if (hasPatches)
			{
				for (int num = patchRows.Count - 1; num >= 0; num--)
				{
					Row row = patchRows[num];
					int columnIndex = column;
					if (RemapColumn(row, ref columnIndex) && row.dataSheet.InternalGetString(row.startIndex, columnIndex, out value))
					{
						return value;
					}
				}
			}
			if (remapColumns && !RemapColumn(this, ref column))
			{
				return fallback;
			}
			return (!dataSheet.InternalGetString(startIndex, column, out value)) ? fallback : value;
		}

		public bool TryGetBool(int column)
		{
			bool value;
			if (hasPatches)
			{
				for (int num = patchRows.Count - 1; num >= 0; num--)
				{
					Row row = patchRows[num];
					int columnIndex = column;
					if (RemapColumn(row, ref columnIndex) && row.dataSheet.InternalGetBool(row.startIndex, columnIndex, out value))
					{
						return value;
					}
				}
			}
			if (remapColumns && !RemapColumn(this, ref column))
			{
				return false;
			}
			dataSheet.InternalGetBool(startIndex, column, out value);
			return value;
		}

		public int TryGetInt(int column)
		{
			return TryGetInt(column, 0);
		}

		public int TryGetInt(int column, int fallback)
		{
			int value;
			if (hasPatches)
			{
				for (int num = patchRows.Count - 1; num >= 0; num--)
				{
					Row row = patchRows[num];
					int columnIndex = column;
					if (RemapColumn(row, ref columnIndex) && row.dataSheet.InternalGetInt(row.startIndex, columnIndex, out value))
					{
						return value;
					}
				}
			}
			if (remapColumns && !RemapColumn(this, ref column))
			{
				return fallback;
			}
			return (!dataSheet.InternalGetInt(startIndex, column, out value)) ? fallback : value;
		}

		public uint TryGetUint(int column)
		{
			return TryGetUint(column, 0u);
		}

		public uint TryGetUint(int column, uint fallback)
		{
			uint value;
			if (hasPatches)
			{
				for (int num = patchRows.Count - 1; num >= 0; num--)
				{
					Row row = patchRows[num];
					int columnIndex = column;
					if (RemapColumn(row, ref columnIndex) && row.dataSheet.InternalGetUint(row.startIndex, columnIndex, out value))
					{
						return value;
					}
				}
			}
			if (remapColumns && !RemapColumn(this, ref column))
			{
				return fallback;
			}
			return (!dataSheet.InternalGetUint(startIndex, column, out value)) ? fallback : value;
		}

		public float TryGetFloat(int column)
		{
			return TryGetFloat(column, 0f);
		}

		public float TryGetFloat(int column, float fallback)
		{
			float value;
			if (hasPatches)
			{
				for (int num = patchRows.Count - 1; num >= 0; num--)
				{
					Row row = patchRows[num];
					int columnIndex = column;
					if (RemapColumn(row, ref columnIndex) && row.dataSheet.InternalGetFloat(row.startIndex, columnIndex, out value))
					{
						return value;
					}
				}
			}
			if (remapColumns && !RemapColumn(this, ref column))
			{
				return fallback;
			}
			return (!dataSheet.InternalGetFloat(startIndex, column, out value)) ? fallback : value;
		}

		public string[] TryGetStringArray(int column)
		{
			string[] value;
			if (hasPatches)
			{
				for (int num = patchRows.Count - 1; num >= 0; num--)
				{
					Row row = patchRows[num];
					int columnIndex = column;
					if (RemapColumn(row, ref columnIndex) && row.dataSheet.InternalGetStringArray(row.startIndex, columnIndex, out value))
					{
						return value;
					}
				}
			}
			if (remapColumns && !RemapColumn(this, ref column))
			{
				return null;
			}
			dataSheet.InternalGetStringArray(startIndex, column, out value);
			return value;
		}

		public int[] TryGetIntArray(int column)
		{
			int[] value;
			if (hasPatches)
			{
				for (int num = patchRows.Count - 1; num >= 0; num--)
				{
					Row row = patchRows[num];
					int columnIndex = column;
					if (RemapColumn(row, ref columnIndex) && row.dataSheet.InternalGetIntArray(row.startIndex, columnIndex, out value))
					{
						return value;
					}
				}
			}
			if (remapColumns && !RemapColumn(this, ref column))
			{
				return null;
			}
			dataSheet.InternalGetIntArray(startIndex, column, out value);
			return value;
		}

		public float[] TryGetFloatArray(int column)
		{
			float[] value;
			if (hasPatches)
			{
				for (int num = patchRows.Count - 1; num >= 0; num--)
				{
					Row row = patchRows[num];
					int columnIndex = column;
					if (RemapColumn(row, ref columnIndex) && row.dataSheet.InternalGetFloatArray(row.startIndex, columnIndex, out value))
					{
						return value;
					}
				}
			}
			if (remapColumns && !RemapColumn(this, ref column))
			{
				return null;
			}
			dataSheet.InternalGetFloatArray(startIndex, column, out value);
			return value;
		}

		public int[] TryGetIntArrayFromDelimitedString(int column)
		{
			string text = TryGetString(column);
			if (text == null)
			{
				return null;
			}
			string[] array = text.Split(',');
			int num = array.Length;
			int[] array2 = new int[num];
			for (int i = 0; i < num; i++)
			{
				int result;
				if (int.TryParse(array[i], out result))
				{
					array2[i] = result;
				}
				else
				{
					array2[i] = 0;
				}
			}
			return array2;
		}

		public float[] TryGetFloatArrayFromDelimitedString(int column)
		{
			string text = TryGetString(column);
			if (text == null)
			{
				return null;
			}
			string[] array = text.Split(',');
			int num = array.Length;
			float[] array2 = new float[num];
			for (int i = 0; i < num; i++)
			{
				float result;
				if (float.TryParse(array[i], out result))
				{
					array2[i] = result;
				}
				else
				{
					array2[i] = 0f;
				}
			}
			return array2;
		}

		public Vector3 TryGetVector3(int column)
		{
			float[] array = TryGetFloatArrayFromDelimitedString(column);
			if (array == null || array.Length != 3)
			{
				return Vector3.zero;
			}
			return new Vector3(array[0], array[1], array[2]);
		}

		public List<string> TryGetStringList(int column)
		{
			List<string> list = new List<string>();
			string[] array = TryGetStringArray(column);
			if (array != null)
			{
				list.AddRange(array);
			}
			return list;
		}

		public DateTime GetDateTime(int column)
		{
			string s = TryGetString(column);
			return DateTime.Parse(s, null, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal);
		}

		public ulong GetDateEpoch(int column)
		{
			return (ulong)(DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
		}

		public DateTime GetDateTime(int column, string format)
		{
			string s = TryGetString(column);
			return DateTime.ParseExact(s, format, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal | DateTimeStyles.AssumeUniversal);
		}

		public DateTime TryGetDateTime(int column)
		{
			string text = TryGetString(column);
			if (!string.IsNullOrEmpty(text))
			{
				return DateTime.Parse(text);
			}
			return DateTime.MinValue;
		}

		public Color TryGetColor(int column, bool isBase256)
		{
			float[] array = TryGetFloatArrayFromDelimitedString(column);
			if (array == null || (array.Length != 3 && array.Length != 4))
			{
				return Color.black;
			}
			if (isBase256)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] /= 255f;
				}
			}
			float a = ((array.Length != 4) ? 1f : array[3]);
			return new Color(array[0], array[1], array[2], a);
		}

		public Quaternion TryGetRotation(int column)
		{
			float[] array = TryGetFloatArray(column);
			if (array != null && array.Length >= 3)
			{
				return Quaternion.Euler(array[0], array[1], array[2]);
			}
			return Quaternion.identity;
		}

		private bool RemapColumn(Row row, ref int columnIndex)
		{
			string columnName = masterSheet.GetColumnName(columnIndex);
			columnIndex = row.dataSheet.GetColumnIndex(columnName);
			return columnIndex >= 0;
		}
	}
}
