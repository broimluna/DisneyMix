using System.Collections;
using System.Collections.Generic;
using Mix.Assets;
using Mix.SequenceOperations;
using UnityEngine;

namespace Mix.Test
{
	public class UpdateAssetHelper : SequenceOperation, ITextAssetObject
	{
		public MonoBehaviour monoEngine;

		public string OTATextFile = "data/ota2.txt";

		public string OABot = "official_accounts/waltdisneyrecords/en_US/bots/waltdisneyrecords_bot_hd.unity3d";

		public string OABg = "official_accounts/waltdisneyrecords/en_US/waltdisneyrecords_background_hd.unity3d";

		public string OAIcon = "official_accounts/waltdisneyrecords/en_US/waltdisneyrecords_icon_hd.unity3d";

		public string OAThumb = "official_accounts/waltdisneyrecords/en_US/waltdisneyrecords_thumb.unity3d";

		public string OTAStickerPackThumb = "stickerpack_thumbs/en_US/ota2_thumb.unity3d";

		public string OTAStickerThumb1 = "stickers/static/ota2/ota2_aj_lame/en_US/ota2_aj_lame_thumb.unity3d";

		public string OTASticker1 = "stickers/static/ota2/ota2_aj_lame/en_US/ota2_aj_lame_hd.unity3d";

		public string OTAStickerThumb2 = "stickers/static/ota2/ota2_bobby_doyousmellthat/en_US/ota2_bobby_doyousmellthat_thumb.unity3d";

		public string OTASticker2 = "stickers/static/ota2/ota2_bobby_doyousmellthat/en_US/ota2_bobby_doyousmellthat_hd.unity3d";

		public string OTAStickerThumb3 = "stickers/static/ota2/ota2_bobby_imcreating/en_US/ota2_bobby_imcreating_thumb.unity3d";

		public string OTASticker3 = "stickers/static/ota2/ota2_bobby_imcreating/en_US/ota2_bobby_imcreating_hd.unity3d";

		public List<AssetData> AssetDataList = new List<AssetData>();

		public UpdateAssetHelper(IOperationCompleteHandler aCaller, MonoBehaviour aMonoEngine)
			: base(aCaller)
		{
			monoEngine = aMonoEngine;
		}

		public void SetInitialAssetData()
		{
			AssetDataList.Add(new AssetData(OTATextFile, false));
			AssetDataList.Add(new AssetData(OABot, true));
			AssetDataList.Add(new AssetData(OABg, true));
			AssetDataList.Add(new AssetData(OAIcon, true));
			AssetDataList.Add(new AssetData(OAThumb, true));
			AssetDataList.Add(new AssetData(OTAStickerPackThumb, false));
			AssetDataList.Add(new AssetData(OTAStickerThumb1, false));
			AssetDataList.Add(new AssetData(OTASticker1, false));
			AssetDataList.Add(new AssetData(OTAStickerThumb2, false));
			AssetDataList.Add(new AssetData(OTASticker2, false));
			AssetDataList.Add(new AssetData(OTAStickerThumb3, false));
			AssetDataList.Add(new AssetData(OTASticker3, false));
		}

		public bool CheckFileExistienceInManifest()
		{
			foreach (AssetData assetData in AssetDataList)
			{
				if (assetData.ShouldExist)
				{
					if (MonoSingleton<AssetManager>.Instance.cpipeManager.cpipe.GetFileVersion(assetData.ManifestPath) == 0)
					{
						IntegrationTest.Fail("file path " + MonoSingleton<AssetManager>.Instance.GetCpipePrefix(assetData.EntitlementPath) + " should exist in manifest");
						return false;
					}
				}
				else if (MonoSingleton<AssetManager>.Instance.cpipeManager.cpipe.GetFileVersion(assetData.ManifestPath) > 0)
				{
					IntegrationTest.Fail("file path " + MonoSingleton<AssetManager>.Instance.GetCpipePrefix(assetData.EntitlementPath) + " should not exist in manifest");
					return false;
				}
			}
			return true;
		}

		public override void StartOperation()
		{
			BaseStartOperation();
			SetInitialAssetData();
			CheckManifest();
		}

		public void CheckManifest()
		{
			if (CheckFileExistienceInManifest())
			{
				TestingUtils.SetClientVersion("2.1.0-183088+CI");
				monoEngine.StartCoroutine(WaitForManifestLoad());
			}
		}

		public bool CheckFileVerison()
		{
			foreach (AssetData assetData in AssetDataList)
			{
				if (assetData.ShouldExist && assetData.FileVersion == MonoSingleton<AssetManager>.Instance.cpipeManager.cpipe.GetFileVersion(assetData.ManifestPath))
				{
					IntegrationTest.Fail("Version of " + assetData.ManifestPath + " should not equal current manifest version");
					return false;
				}
			}
			return true;
		}

		private IEnumerator WaitForManifestLoad()
		{
			yield return new WaitForSeconds(65f);
			if (!CheckFileVerison())
			{
				yield break;
			}
			foreach (AssetData data in AssetDataList)
			{
				data.UpdateData();
				data.ShouldExist = true;
			}
			if (CheckFileExistienceInManifest())
			{
				LoadParams lp = new LoadParams("OTATextFile_sha", OTATextFile);
				MonoSingleton<AssetManager>.Instance.LoadText(this, lp, "OTA1");
			}
		}

		public void TextAssetObjectComplete(string aText, object aUserData)
		{
			if (((string)aUserData).Equals("OTA1"))
			{
				if (!aText.Equals("OTA1"))
				{
					IntegrationTest.Fail("OTA text should == OTA1");
					return;
				}
				TestingUtils.SetClientVersion("2.1.0-183225+CI");
				monoEngine.StartCoroutine(WaitForSecondManifestLoad());
			}
			if (((string)aUserData).Equals("OTA2"))
			{
				if (!aText.Equals("OTA2"))
				{
					IntegrationTest.Fail("OTA text should == OTA2");
				}
				else
				{
					IntegrationTest.Pass();
				}
			}
		}

		private IEnumerator WaitForSecondManifestLoad()
		{
			yield return new WaitForSeconds(70f);
			if (!CheckFileVerison())
			{
				yield break;
			}
			foreach (AssetData data in AssetDataList)
			{
				data.UpdateData();
				data.ShouldExist = true;
			}
			if (CheckFileExistienceInManifest())
			{
				LoadParams lp = new LoadParams("OTATextFile_sha", OTATextFile);
				MonoSingleton<AssetManager>.Instance.LoadText(this, lp, "OTA2");
			}
		}
	}
}
