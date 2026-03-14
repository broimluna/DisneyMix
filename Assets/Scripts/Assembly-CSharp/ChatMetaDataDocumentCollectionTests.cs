using Mix;
using Mix.DeviceDb;
using UnityEngine;

public class ChatMetaDataDocumentCollectionTests : MonoBehaviour
{
	private void Start()
	{
		Mix.Application.TestingDirectory = "/ChatMetaDataDocumentCollectionTests/";
		Singleton<MixDocumentCollections>.Instance.ClearChatMetaDataAndCollection();
		ChatMetaDataDocumentCollectionApi chatMetaDataDocumentCollectionApi = Singleton<MixDocumentCollections>.Instance.chatMetaDataDocumentCollectionApi;
		chatMetaDataDocumentCollectionApi.AddHeight("float1", 1.035f);
		chatMetaDataDocumentCollectionApi.AddHeight("float2", 2345.232f);
		float height = chatMetaDataDocumentCollectionApi.GetHeight("float1");
		float height2 = chatMetaDataDocumentCollectionApi.GetHeight("float2");
		if (height != 1.035f)
		{
			Singleton<MixDocumentCollections>.Instance.ClearChatMetaDataAndCollection();
			Mix.Application.TestingDirectory = string.Empty;
			IntegrationTest.Fail("float 1 not equal");
			return;
		}
		if (height2 != 2345.232f)
		{
			Singleton<MixDocumentCollections>.Instance.ClearChatMetaDataAndCollection();
			Mix.Application.TestingDirectory = string.Empty;
			IntegrationTest.Fail("float 2 not equal");
			return;
		}
		chatMetaDataDocumentCollectionApi.AddHeight("float1", 1.1f);
		height = chatMetaDataDocumentCollectionApi.GetHeight("float1");
		if (height != 1.1f)
		{
			Singleton<MixDocumentCollections>.Instance.ClearChatMetaDataAndCollection();
			Mix.Application.TestingDirectory = string.Empty;
			IntegrationTest.Fail("float 1 not updated or equal");
			return;
		}
		chatMetaDataDocumentCollectionApi.LogOut();
		if (chatMetaDataDocumentCollectionApi.collections.Count > 0)
		{
			Singleton<MixDocumentCollections>.Instance.ClearChatMetaDataAndCollection();
			Mix.Application.TestingDirectory = string.Empty;
			IntegrationTest.Fail("log out didnt remove all collections.");
		}
		else
		{
			Singleton<MixDocumentCollections>.Instance.ClearChatMetaDataAndCollection();
			Mix.Application.TestingDirectory = string.Empty;
			IntegrationTest.Pass(base.gameObject);
		}
	}

	private void Update()
	{
	}
}
