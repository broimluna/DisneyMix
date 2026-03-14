using System.Collections.Generic;
using Core.Joe;
using Mix;
using Mix.Assets;
using UnityEngine;

namespace Core.MetaData
{
	public class Catalog
	{
		private Dictionary<string, Sheet> sheets;

		public float parseTime;

		public Catalog()
		{
			sheets = new Dictionary<string, Sheet>();
		}

		public Sheet GetSheet(string sheetName)
		{
			return (!sheets.ContainsKey(sheetName)) ? null : sheets[sheetName];
		}

		public Dictionary<string, Sheet> GetSheets()
		{
			return sheets;
		}

		public void PatchData(string catalogFile, CatalogDelegate completeCallback)
		{
			catalogFile = catalogFile.Replace(".json", ".joe");
			object cookie = new KeyValuePair<string, CatalogDelegate>(catalogFile, completeCallback);
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			byte[] rawFileBytes = MonoSingleton<AssetManager>.Instance.LoadBytes(catalogFile);
			JoeFile joe = new JoeFile(rawFileBytes);
			ProcessJoe(joe, cookie);
			parseTime += Time.realtimeSinceStartup - realtimeSinceStartup;
		}

		public void ParseCatalog(JoeFile joe)
		{
			Sheet[] allSheets = joe.GetAllSheets();
			if (allSheets == null)
			{
				return;
			}
			int i = 0;
			for (int num = allSheets.Length; i < num; i++)
			{
				Sheet sheet = allSheets[i];
				string sheetName = sheet.SheetName;
				if (sheets.ContainsKey(sheetName))
				{
					sheets[sheetName].PatchRows(sheet);
				}
				else
				{
					sheets.Add(sheetName, sheet);
				}
			}
		}

		private void AssetFailure(object cookie)
		{
			ProcessJoe(null, cookie);
		}

		private void ProcessJoe(JoeFile joe, object cookie)
		{
			KeyValuePair<string, CatalogDelegate> keyValuePair = (KeyValuePair<string, CatalogDelegate>)cookie;
			string key = keyValuePair.Key;
			CatalogDelegate value = keyValuePair.Value;
			if (joe != null)
			{
				ParseCatalog(joe);
			}
			if (value != null)
			{
				key = key.Replace(".joe", ".json");
				value(joe != null, key);
			}
		}
	}
}
