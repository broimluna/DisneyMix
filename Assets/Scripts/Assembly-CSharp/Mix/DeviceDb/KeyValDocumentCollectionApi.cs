using System;
using System.Collections.Generic;
using DeviceDB;

namespace Mix.DeviceDb
{
	public class KeyValDocumentCollectionApi : MixDocumentCollectionApi<KeyValDocument>, IKeyValDatabaseApi
	{
		public const string deviceDocumentCollectionName = "devicesettings";

		protected override string CollectionName
		{
			get
			{
				return "KeyValDB";
			}
		}

		public override void LogOut()
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, IDocumentCollection<KeyValDocument>> collection in base.collections)
			{
				if (collection.Value != null && collection.Key != "devicesettings")
				{
					collection.Value.Dispose();
					list.Add(collection.Key);
				}
			}
			foreach (string item in list)
			{
				base.collections.Remove(item);
			}
		}

		public void RemoveUserKey(string key)
		{
			IDocumentCollection<KeyValDocument> collection = GetCollection(GetUserId(), "/" + CollectionName + "/" + GetUserId());
			if (collection != null)
			{
				KeyValDocument documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "key", key);
				if (documentByFieldAndKey != null)
				{
					collection.Delete(documentByFieldAndKey.Id);
				}
			}
		}

		public void RemoveDeviceKey(string key)
		{
			IDocumentCollection<KeyValDocument> collection = GetCollection("devicesettings", "/" + CollectionName + "/devicesettings");
			if (collection != null)
			{
				KeyValDocument documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "key", key);
				if (documentByFieldAndKey != null)
				{
					collection.Delete(documentByFieldAndKey.Id);
				}
			}
		}

		public string LoadUserValue(string key)
		{
			return LoadValue(GetUserId(), key);
		}

		public void SaveUserValue(string key, string value)
		{
			SaveValue(GetUserId(), key, value);
		}

		public bool LoadUserValueAsBool(string key, bool defaultVal = false)
		{
			return LoadValueAsBool(GetUserId(), key, defaultVal);
		}

		public void SaveUserValueFromBool(string key, bool value)
		{
			SaveValueFromBool(GetUserId(), key, value);
		}

		public float LoadUserValueAsFloat(string key, float defaultVal = 0f)
		{
			return LoadValueAsFloat(GetUserId(), key, defaultVal);
		}

		public void SaveUserValueFromFloat(string key, float value)
		{
			SaveValueFromFloat(GetUserId(), key, value);
		}

		public int LoadUserValueAsInt(string key, int defaultVal = 0)
		{
			return LoadValueAsInt(GetUserId(), key, defaultVal);
		}

		public void SaveUserValueFromInt(string key, int value)
		{
			SaveValueFromInt(GetUserId(), key, value);
		}

		public string LoadDeviceValue(string key)
		{
			return LoadValue("devicesettings", key);
		}

		public void SaveDeviceValue(string key, string value)
		{
			SaveValue("devicesettings", key, value);
		}

		public bool LoadDeviceValueAsBool(string key, bool defaultVal)
		{
			return LoadValueAsBool("devicesettings", key, defaultVal);
		}

		public void SaveDeviceValueFromBool(string key, bool value)
		{
			SaveValueFromBool("devicesettings", key, value);
		}

		public float LoadDeviceValueAsFloat(string key, float defaultVal)
		{
			return LoadValueAsFloat("devicesettings", key, defaultVal);
		}

		public void SaveDeviceValueFromFloat(string key, float value)
		{
			SaveValueFromFloat("devicesettings", key, value);
		}

		public int LoadDeviceValueAsInt(string key, int defaultVal)
		{
			return LoadValueAsInt("devicesettings", key, defaultVal);
		}

		public void SaveDeviceValueFromInt(string key, int value)
		{
			SaveValueFromInt("devicesettings", key, value);
		}

		private string LoadValue(string collectionName, string key)
		{
			string result = null;
			IDocumentCollection<KeyValDocument> collection = GetCollection(collectionName, "/" + CollectionName + "/" + collectionName);
			if (collection == null)
			{
				return result;
			}
			KeyValDocument documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "key", key);
			if (documentByFieldAndKey != null)
			{
				result = documentByFieldAndKey.value;
			}
			return result;
		}

		private void SaveValue(string collectionName, string key, string value)
		{
			IDocumentCollection<KeyValDocument> collection = GetCollection(collectionName, "/" + CollectionName + "/" + collectionName);
			if (collection != null)
			{
				KeyValDocument documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "key", key);
				if (documentByFieldAndKey == null)
				{
					documentByFieldAndKey = new KeyValDocument();
					documentByFieldAndKey.key = key;
					documentByFieldAndKey.value = value;
					collection.Insert(documentByFieldAndKey);
				}
				else
				{
					documentByFieldAndKey.value = value;
					collection.Update(documentByFieldAndKey);
				}
			}
		}

		private bool LoadValueAsBool(string tableName, string key, bool defaultVal = false)
		{
			bool result = defaultVal;
			int num = (defaultVal ? 1 : 0);
			try
			{
				string value = LoadValue(tableName, key);
				if (!string.IsNullOrEmpty(value))
				{
					num = Convert.ToInt32(value);
				}
			}
			catch (Exception exception)
			{
				Log.Exception(exception);
			}
			switch (num)
			{
			case 1:
				result = true;
				break;
			case 0:
				result = false;
				break;
			}
			return result;
		}

		private void SaveValueFromBool(string tableName, string key, bool value)
		{
			string value2 = string.Empty;
			if (value)
			{
				value2 = "1";
			}
			else if (!value)
			{
				value2 = "0";
			}
			SaveValue(tableName, key, value2);
		}

		private float LoadValueAsFloat(string tableName, string key, float defaultVal = 0f)
		{
			float result = defaultVal;
			string s = LoadValue(tableName, key);
			if (!float.TryParse(s, out result))
			{
				result = defaultVal;
			}
			return result;
		}

		private void SaveValueFromFloat(string tableName, string key, float value)
		{
			string value2 = Convert.ToString(value);
			SaveValue(tableName, key, value2);
		}

		private void SaveValueFromInt(string tableName, string key, int value)
		{
			string value2 = Convert.ToString(value);
			SaveValue(tableName, key, value2);
		}

		private int LoadValueAsInt(string tableName, string key, int defaultVal = 0)
		{
			int result = defaultVal;
			string s = LoadValue(tableName, key);
			if (!int.TryParse(s, out result))
			{
				result = defaultVal;
			}
			return result;
		}
	}
}
