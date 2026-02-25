using System;
using System.Collections.Generic;

namespace Mix.Assets
{
	public class Record
	{
		public const string KEY_VALUE_DELIMITER = "||||";

		public const string ENTRY_DELIMITER = "####";

		public string Sha { get; set; }

		public Dictionary<string, string> Header { get; set; }

		public string Path { get; set; }

		public long AccessCount { get; set; }

		public string InsertTime { get; set; }

		public int CpipeManifestVersion { get; set; }

		public Record(string aSha, Dictionary<string, string> aHeader = null, string aPath = null, long aAccessCount = 0, string aInsertTime = null, int aCpipeManifestVersion = 0)
		{
			Header = null;
			Path = null;
			AccessCount = -1L;
			InsertTime = null;
			Sha = aSha;
			Header = aHeader;
			Path = aPath;
			AccessCount = aAccessCount;
			InsertTime = aInsertTime;
			CpipeManifestVersion = aCpipeManifestVersion;
		}

		public static Dictionary<string, string> ParseDictionary(string s)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (string.IsNullOrEmpty(s))
			{
				return dictionary;
			}
			string[] separator = new string[1] { "####" };
			string[] array = s.Split(separator, StringSplitOptions.RemoveEmptyEntries);
			string[] array2 = array;
			foreach (string text in array2)
			{
				string[] separator2 = new string[1] { "||||" };
				string[] array3 = text.Split(separator2, StringSplitOptions.RemoveEmptyEntries);
				int num = 0;
				string key = string.Empty;
				string value = string.Empty;
				string[] array4 = array3;
				foreach (string text2 in array4)
				{
					switch (num)
					{
					case 0:
						if (!string.IsNullOrEmpty(text2))
						{
							key = text2;
							num++;
						}
						break;
					case 1:
						if (!string.IsNullOrEmpty(text2))
						{
							value = text2;
							num++;
						}
						break;
					}
				}
				if (num == 2)
				{
					dictionary.Add(key, value);
				}
			}
			return dictionary;
		}

		public string GetHeaderString()
		{
			string text = null;
			if (Header != null)
			{
				text = string.Empty;
				int num = 0;
				foreach (string key in Header.Keys)
				{
					text = text + key + "||||" + Header[key];
					num++;
					if (num < Header.Count)
					{
						text += "####";
					}
				}
			}
			return text;
		}
	}
}
