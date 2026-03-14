using System.IO;
using System.Text;
using Core.Joe;
using Core.MetaData;
using UnityEditor;
using UnityEngine;

public static class JoeExtractor
{
	[MenuItem("Tools/Joe/Extract contentfile.joe to CSV")]
	public static void ExtractJoeToCsv()
	{
		string joePath = EditorUtility.OpenFilePanel("Select .joe file", "", "joe");
		if (string.IsNullOrEmpty(joePath))
		{
			return;
		}

		string outputDir = EditorUtility.OpenFolderPanel("Select output folder", "", "");
		if (string.IsNullOrEmpty(outputDir))
		{
			return;
		}

		byte[] bytes = File.ReadAllBytes(joePath);
		JoeFile joeFile = new JoeFile(bytes);
		Sheet[] sheets = joeFile.GetAllSheets();

		if (sheets == null || sheets.Length == 0)
		{
			Debug.LogError("Failed to parse .joe file (invalid or unsupported format).");
			return;
		}

		for (int i = 0; i < sheets.Length; i++)
		{
			Sheet sheet = sheets[i];
			Column[] columns = sheet.InternalGetAllColumns();
			var rows = sheet.GetAllRows();

			if (columns == null || rows == null)
			{
				continue;
			}

			StringBuilder sb = new StringBuilder();

			// Header
			for (int c = 0; c < columns.Length; c++)
			{
				if (c > 0)
				{
					sb.Append(",");
				}
				sb.Append(EscapeCsv(columns[c].ColName));
			}
			sb.AppendLine();

			// Data
			foreach (var kv in rows)
			{
				Row row = kv.Value;

				for (int c = 0; c < columns.Length; c++)
				{
					if (c > 0)
					{
						sb.Append(",");
					}

					string value = GetCellValue(row, columns[c], c);
					sb.Append(EscapeCsv(value));
				}

				sb.AppendLine();
			}

			string safeSheetName = MakeSafeFileName(sheet.SheetName);
			string csvPath = Path.Combine(outputDir, safeSheetName + ".csv");
			File.WriteAllText(csvPath, sb.ToString(), Encoding.UTF8);
		}

		AssetDatabase.Refresh();
		Debug.Log("JOE extraction complete.");
	}

	private static string GetCellValue(Row row, Column column, int colIndex)
	{
		switch (column.ColType)
		{
			case ColumnType.StringArray:
			{
				string[] arr = row.TryGetStringArray(colIndex);
				return arr == null ? string.Empty : string.Join("|", arr);
			}
			case ColumnType.NonNegativeIntArray:
			case ColumnType.RawIntArray:
			{
				int[] arr = row.TryGetIntArray(colIndex);
				if (arr == null)
				{
					return string.Empty;
				}

				StringBuilder sb = new StringBuilder();
				for (int i = 0; i < arr.Length; i++)
				{
					if (i > 0)
					{
						sb.Append("|");
					}
					sb.Append(arr[i]);
				}
				return sb.ToString();
			}
			case ColumnType.FloatArray:
			{
				float[] arr = row.TryGetFloatArray(colIndex);
				if (arr == null)
				{
					return string.Empty;
				}

				StringBuilder sb = new StringBuilder();
				for (int i = 0; i < arr.Length; i++)
				{
					if (i > 0)
					{
						sb.Append("|");
					}
					sb.Append(arr[i]);
				}
				return sb.ToString();
			}
			default:
				return row.TryGetString(colIndex, string.Empty);
		}
	}

	private static string EscapeCsv(string value)
	{
		if (value == null)
		{
			return string.Empty;
		}

		bool needsQuotes = value.Contains(",") || value.Contains("\"") || value.Contains("\n") || value.Contains("\r");
		if (!needsQuotes)
		{
			return value;
		}

		return "\"" + value.Replace("\"", "\"\"") + "\"";
	}

	private static string MakeSafeFileName(string fileName)
	{
		if (string.IsNullOrEmpty(fileName))
		{
			return "Sheet";
		}

		char[] invalid = Path.GetInvalidFileNameChars();
		StringBuilder sb = new StringBuilder(fileName.Length);

		for (int i = 0; i < fileName.Length; i++)
		{
			char ch = fileName[i];
			bool isInvalid = false;

			for (int j = 0; j < invalid.Length; j++)
			{
				if (ch == invalid[j])
				{
					isInvalid = true;
					break;
				}
			}

			sb.Append(isInvalid ? '_' : ch);
		}

		return sb.ToString();
	}
}