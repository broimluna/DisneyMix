using System.Collections.Generic;
using System.Diagnostics;
using Mix.Assets;
using Mix.DeviceDb;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Mix.Test
{
	public class AssetCacheDocumentCollectionTests : MonoBehaviour
	{
		private void Update()
		{
		}

		private void Start()
		{
			Application.TestingDirectory = "/AssetCacheDocumentCollectionTests/";
			Singleton<MixDocumentCollections>.Instance.ClearAssetCacheApiAndCollection();
			AssetCacheDocumentCollectionApi assetCacheDocumentCollectionApi = Singleton<MixDocumentCollections>.Instance.assetCacheDocumentCollectionApi;
			Debug.Log("Add new blank record with sha = 'sha'");
			assetCacheDocumentCollectionApi.AddRecord(new Record("sha"));
			if (!assetCacheDocumentCollectionApi.CheckForRecord("sha"))
			{
				FailTest("CheckForRecord returned false " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
				return;
			}
			Record record = assetCacheDocumentCollectionApi.GetRecord("sha");
			if (record == null)
			{
				FailTest("GetRecord returned null " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
				return;
			}
			if (record.Sha != "sha")
			{
				FailTest("GetRecord sha doesnt match " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
				return;
			}
			if (record.AccessCount < 1)
			{
				FailTest("AccessCount was not set to timestamp when adding new record. accesscount = " + record.AccessCount + " " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
				return;
			}
			if (!string.IsNullOrEmpty(record.Path))
			{
				Debug.Log("Path is: " + record.Path);
				FailTest("record's path was not null or empty string " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
				return;
			}
			if (!string.IsNullOrEmpty(record.InsertTime))
			{
				FailTest("record's inserttime was not null or empty string " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
				return;
			}
			if (record.Header != null && record.Header.Count > 0)
			{
				FailTest("record's header was not null or empty" + record.Header.ToString() + " " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
				return;
			}
			Debug.Log("Update Record's sha");
			record.Sha = "updateSha";
			assetCacheDocumentCollectionApi.UpdateRecord(record);
			record = assetCacheDocumentCollectionApi.GetRecord("updateSha");
			if (record != null)
			{
				FailTest("GetRecord returned for sha 'updateSha' but it overwrote the existing sha for record with sha = 'sha' " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
				return;
			}
			if (assetCacheDocumentCollectionApi.CheckForRecord("updateSha"))
			{
				FailTest("record shouldn't exist " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
				return;
			}
			record = assetCacheDocumentCollectionApi.GetRecord("sha");
			if (record == null)
			{
				FailTest("GetRecord returned null " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
				return;
			}
			if (record.Sha != "sha")
			{
				FailTest("GetRecord sha doesnt match " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
				return;
			}
			if (record.AccessCount < 1)
			{
				FailTest("AccessCount was not set to timestamp when adding new record " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
				return;
			}
			if (!string.IsNullOrEmpty(record.Path))
			{
				FailTest("record's path was not null or empty string " + record.Path + " " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
				return;
			}
			if (!string.IsNullOrEmpty(record.InsertTime))
			{
				FailTest("record's inserttime was not null or empty string " + record.InsertTime + " " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
				return;
			}
			if (record.Header != null && record.Header.Count > 0)
			{
				FailTest("record's header was not null or empty " + record.Header.ToString() + " " + (record.Header == null) + " " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
				return;
			}
			if (record == null)
			{
				FailTest("getUpdateRecord is null, propbably because the sha was changed when it shoulnd't have been. shas shouldn't be updatedable thru the api. " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
				return;
			}
			Debug.Log("Udpate Record");
			record.Path = "new path";
			record.Header = new Dictionary<string, string>();
			record.Header.Add("headerKey1", "headerValue1");
			record.Header.Add("headerKey2", "headerValue2");
			record.InsertTime = "insertTime";
			assetCacheDocumentCollectionApi.UpdateRecord(record);
			record = assetCacheDocumentCollectionApi.GetRecord("sha");
			if (record.Path != "new path")
			{
				FailTest("record's path was not updated properly " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
				return;
			}
			if (record.GetHeaderString() == null)
			{
				FailTest("record's header was not updated properly " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
				return;
			}
			if (Record.ParseDictionary(record.GetHeaderString()) == null)
			{
				FailTest("record's header was not updated properly " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
				return;
			}
			if (!record.Header.ContainsKey("headerKey1"))
			{
				FailTest("record's header was not updated properly " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
				return;
			}
			if (record.Header["headerKey1"] != "headerValue1")
			{
				FailTest("record's header was not updated properly " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
				return;
			}
			if (!record.Header.ContainsKey("headerKey2"))
			{
				FailTest("record's header was not updated properly " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
				return;
			}
			if (record.Header["headerKey2"] != "headerValue2")
			{
				FailTest("record's header was not updated properly " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
				return;
			}
			if (record.InsertTime != "insertTime")
			{
				FailTest("record's inserttime was not updated properly " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
				return;
			}
			Dictionary<string, string> header = assetCacheDocumentCollectionApi.GetHeader(record.Sha);
			if (!header.ContainsKey("headerKey1"))
			{
				FailTest("record's header was not updated properly " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
				return;
			}
			if (header["headerKey1"] != "headerValue1")
			{
				FailTest("record's header was not updated properly " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
				return;
			}
			if (!header.ContainsKey("headerKey2"))
			{
				FailTest("record's header was not updated properly " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
				return;
			}
			if (header["headerKey2"] != "headerValue2")
			{
				FailTest("record's header was not updated properly " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
				return;
			}
			long accessCount = assetCacheDocumentCollectionApi.GetAccessCount(record.Sha);
			if (accessCount != record.AccessCount)
			{
				FailTest("GetAccessCount retunred incorrect value " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
				return;
			}
			assetCacheDocumentCollectionApi.IncrementAccessCount(record.Sha);
			record = assetCacheDocumentCollectionApi.GetRecord(record.Sha);
			if (record.AccessCount < accessCount)
			{
				FailTest("accesscount value is less than before calling IncrementAccessCount " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			}
			else if (record.AccessCount == accessCount)
			{
				FailTest("IncrementAccessCount didnt change record's accesscount value " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			}
			else
			{
				PassTest();
			}
		}

		public void FailTest(string reason)
		{
			Singleton<MixDocumentCollections>.Instance.ClearAssetCacheApiAndCollection();
			Application.TestingDirectory = string.Empty;
			IntegrationTest.Fail(base.gameObject, reason);
		}

		public void PassTest()
		{
			Singleton<MixDocumentCollections>.Instance.ClearAssetCacheApiAndCollection();
			Application.TestingDirectory = string.Empty;
			IntegrationTest.Pass(base.gameObject);
		}
	}
}
