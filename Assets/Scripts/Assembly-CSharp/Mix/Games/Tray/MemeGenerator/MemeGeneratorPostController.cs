using LitJson;
using Mix.Games.Chat;
using Mix.Games.Data;
using Mix.Games.Session;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.MemeGenerator
{
	public class MemeGeneratorPostController : BaseGameChatController, IGameAsset
	{
		public Text BottomCaption;

		public Text TopCaption;

		public RawImage MemeImage;

		void IGameAsset.OnGameSessionAssetLoaded(object aUserData)
		{
			if (this.IsNullOrDisposed() || mSession == null || base.Loader.IsNullOrDisposed())
			{
				return;
			}
			MemeGeneratorData memeGeneratorData = (MemeGeneratorData)mGameData;
			Object bundleInstance = mSession.GetBundleInstance(memeGeneratorData.ImageUrl);
			if (bundleInstance != null)
			{
				base.Loader.SetActive(false);
				if (bundleInstance is Texture2D)
				{
					Texture2D texture = (Texture2D)bundleInstance;
					MemeImage.texture = texture;
				}
				else if (bundleInstance is GameObject)
				{
					GameObject gameObject = (GameObject)bundleInstance;
					Sprite sprite = gameObject.GetComponent<Image>().sprite;
					MemeImage.texture = sprite.texture;
				}
			}
		}

		public override void SetupGameData(string aGameDataJson)
		{
			mGameData = JsonMapper.ToObject<MemeGeneratorData>(aGameDataJson);
		}

		protected override void SetupView()
		{
			base.Loader.SetActive(true);
			MemeGeneratorData memeGeneratorData = (MemeGeneratorData)mGameData;
			BottomCaption.text = memeGeneratorData.BottomCaption;
			TopCaption.text = memeGeneratorData.TopCaption;
			if (!mSession.WillBundleLoadFromWeb(memeGeneratorData.ImageUrl))
			{
				base.Loader.SetActive(false);
			}
			LoadAsset(this, memeGeneratorData.ImageUrl);
			mHideFooter = true;
			mHideTail = true;
		}
	}
}
