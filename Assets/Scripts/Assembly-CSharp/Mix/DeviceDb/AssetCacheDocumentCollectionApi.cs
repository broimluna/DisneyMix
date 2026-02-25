using System.Collections.Generic;
using System.Diagnostics;
using DeviceDB;
using Mix.Assets;
using UnityEngine;

namespace Mix.DeviceDb
{
	public class AssetCacheDocumentCollectionApi : MixDocumentCollectionApi<AssetCacheDocument>, IAssetDatabaseApi
	{
		protected override string CollectionName
		{
			get
			{
				return "AssetDB";
			}
		}

		public IDocumentCollection<AssetCacheDocument> collection { get; private set; }

		public AssetCacheDocumentCollectionApi()
		{
			collection = GetCollection(CollectionName, "/" + CollectionName);
		}

		public List<uint> GetIdsByUseCountOrdered(float aPercentToReturn = 1f)
		{
			return GetDocumentIdsGreaterThenInt("accessCount", -10, collection);
		}

		public void RemoveRecordByIndex(uint aIndex)
		{
			if (collection != null)
			{
				collection.Delete(aIndex);
			}
		}

		public Record GetRecordByIndex(uint index)
		{
			if (collection == null)
			{
				return null;
			}
			AssetCacheDocument assetCacheDocument = FindDocById(collection, index);
			if (assetCacheDocument != null)
			{
				return new Record(assetCacheDocument.sha, Record.ParseDictionary(assetCacheDocument.header), assetCacheDocument.path, assetCacheDocument.accessCount, assetCacheDocument.insertTime, assetCacheDocument.CpipeManifestVersion);
			}
			return null;
		}

		public void ClearAssets()
		{
		}

		public void CreateAssetsTable()
		{
		}

		public void CreateAssetsUniqueIndex()
		{
		}

		public bool CheckForRecord(string aSha)
		{
			if (collection == null)
			{
				return false;
			}
			AssetCacheDocument documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "sha", aSha);
			if (documentByFieldAndKey != null)
			{
				return true;
			}
			return false;
		}

		public Dictionary<string, string> GetHeader(string aSha)
		{
			if (collection == null)
			{
				return null;
			}
			AssetCacheDocument documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "sha", aSha);
			if (documentByFieldAndKey == null || documentByFieldAndKey.header == null)
			{
				return null;
			}
			return Record.ParseDictionary(documentByFieldAndKey.header);
		}

		public long GetAccessCount(string aSha)
		{
			if (collection == null)
			{
				return -1L;
			}
			Record record = GetRecord(aSha);
			if (record == null)
			{
				return -1L;
			}
			return record.AccessCount;
		}

		public int GetCpipeManifestVersion(string aSha)
		{
			if (collection == null)
			{
				return -1;
			}
			Record record = GetRecord(aSha);
			if (record == null)
			{
				return -1;
			}
			return record.CpipeManifestVersion;
		}

		public int IncrementAccessCount(string aSha)
		{
			Record record = GetRecord(aSha);
			if (record == null)
			{
				return 0;
			}
			record.AccessCount = Stopwatch.GetTimestamp();
			UpdateRecord(record);
			return 1;
		}

		public void AddRecord(Record aRecord)
		{
			if (collection != null)
			{
				AssetCacheDocument documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "sha", aRecord.Sha);
				if (documentByFieldAndKey != null)
				{
					UpdateRecord(aRecord);
				}
				else if (aRecord != null)
				{
					documentByFieldAndKey = new AssetCacheDocument();
					documentByFieldAndKey.sha = aRecord.Sha;
					documentByFieldAndKey.path = aRecord.Path;
					documentByFieldAndKey.header = aRecord.GetHeaderString();
					documentByFieldAndKey.accessCount = Stopwatch.GetTimestamp();
					documentByFieldAndKey.insertTime = aRecord.InsertTime;
					documentByFieldAndKey.CpipeManifestVersion = aRecord.CpipeManifestVersion;
					collection.Insert(documentByFieldAndKey);
				}
			}
		}

		public Record GetRecord(string aSha)
		{
			if (collection == null)
			{
				return null;
			}
			AssetCacheDocument documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "sha", aSha);
			if (documentByFieldAndKey != null)
			{
				return new Record(documentByFieldAndKey.sha, Record.ParseDictionary(documentByFieldAndKey.header), documentByFieldAndKey.path, documentByFieldAndKey.accessCount, documentByFieldAndKey.insertTime, documentByFieldAndKey.CpipeManifestVersion);
			}
			return null;
		}

		public void UpdateRecord(Record aRecord)
		{
			if (collection == null || aRecord == null || string.IsNullOrEmpty(aRecord.Sha))
			{
				return;
			}
			AssetCacheDocument documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "sha", aRecord.Sha);
			if (documentByFieldAndKey != null)
			{
				documentByFieldAndKey.accessCount = Stopwatch.GetTimestamp();
				if (aRecord.Header != null)
				{
					documentByFieldAndKey.header = aRecord.GetHeaderString();
				}
				if (!string.IsNullOrEmpty(aRecord.InsertTime))
				{
					documentByFieldAndKey.insertTime = aRecord.InsertTime;
				}
				if (!string.IsNullOrEmpty(aRecord.Path))
				{
					documentByFieldAndKey.path = aRecord.Path;
				}
				documentByFieldAndKey.CpipeManifestVersion = aRecord.CpipeManifestVersion;
				collection.Update(documentByFieldAndKey);
			}
		}

		public bool DeleteRecordBySha(string aSha)
		{
			bool result = false;
			try
			{
				AssetCacheDocument documentByFieldAndKey = GetDocumentByFieldAndKey(collection, "sha", aSha);
				if (documentByFieldAndKey == null)
				{
					return false;
				}
				RemoveRecordByIndex(documentByFieldAndKey.Id);
				result = MonoSingleton<AssetManager>.Instance.DeleteFile(string.Empty, documentByFieldAndKey.path);
			}
			catch (UnityException exception)
			{
				Log.Exception(exception);
			}
			return result;
		}
	}
}
