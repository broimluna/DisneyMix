using System.Diagnostics;
using Mix.DeviceDb;
using UnityEngine;

namespace Mix.Test
{
	public class AvatarSnapshotDocumentCollectionTests : MonoBehaviour
	{
		private void Start()
		{
			Application.TestingDirectory = "/AvatarDocumentCollectionTests/";
			Singleton<MixDocumentCollections>.Instance.ClearAvatarSnapshotDocumentCollection();
			AvatarSnapshotDocumentCollectionApi avatarSnapshotDocumentCollectionApi = Singleton<MixDocumentCollections>.Instance.avatarSnapshotDocumentCollectionApi;
			avatarSnapshotDocumentCollectionApi.AddAvatarSnapshotData("avatar1", "path1", true, true, 512, 100f);
			AvatarSnapshotDocument avatarSnapshotData = avatarSnapshotDocumentCollectionApi.GetAvatarSnapshotData("avatar1");
			if (avatarSnapshotData.path != "path1" || !avatarSnapshotData.isHd || !avatarSnapshotData.hasNormals || avatarSnapshotData.size != 512 || avatarSnapshotData.loadPercentage != 100f)
			{
				Singleton<MixDocumentCollections>.Instance.ClearAvatarSnapshotDocumentCollection();
				Application.TestingDirectory = string.Empty;
				IntegrationTest.Fail("string value not correct " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
				return;
			}
			avatarSnapshotDocumentCollectionApi.RemoveAvatarSnapshotData("avatar1");
			avatarSnapshotData = avatarSnapshotDocumentCollectionApi.GetAvatarSnapshotData("avatar1");
			if (avatarSnapshotData != null)
			{
				Singleton<MixDocumentCollections>.Instance.ClearAvatarSnapshotDocumentCollection();
				Application.TestingDirectory = string.Empty;
				IntegrationTest.Fail("Avatar not deleted " + new StackTrace(true).GetFrame(0).GetFileLineNumber());
			}
			else
			{
				Singleton<MixDocumentCollections>.Instance.ClearAvatarSnapshotDocumentCollection();
				Application.TestingDirectory = string.Empty;
				IntegrationTest.Pass(base.gameObject);
			}
		}

		private void Update()
		{
		}
	}
}
