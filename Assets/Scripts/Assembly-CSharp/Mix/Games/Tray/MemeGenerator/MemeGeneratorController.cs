using System;
using System.Collections.Generic;
using System.Linq;
using LitJson;
using Mix.Games.Data;
using Mix.Games.Session;

namespace Mix.Games.Tray.MemeGenerator
{
	public class MemeGeneratorController : BaseGameController, IMemeGeneration, IMixGame, IMixGameDataRequest
	{
		public MemeImageSelectionController ImageSelectorController;

		public MemeCaptionCreationController CaptionCreationController;

		protected string mBaseUrl;

		protected MemeGeneratorData mData;

		protected List<MixGameContentData> mMemeGeneratorData;

		MemeGeneratorController IMemeGeneration.GetMemeGeneratorController()
		{
			return this;
		}

		void IMemeGeneration.QuitMemeGeneration()
		{
			CloseGame();
		}

		void IMemeGeneration.SetMemeCaption(Captions aCaptions)
		{
			mData.BottomCaption = aCaptions.BottomCaption;
			mData.TopCaption = aCaptions.TopCaption;
			mData.Status = MixGameSessionStatus.COMPLETE;
			GameOver(mData);
		}

		void IMemeGeneration.SetMemeImage(string aImageUrl)
		{
			mData.ImageUrl = aImageUrl;
			ImageSelectorController.gameObject.SetActive(false);
			ToastPanelController component = CaptionCreationController.gameObject.GetComponent<ToastPanelController>();
			component.Init(BaseGameController.Instance.Session.StatusBarHeight, BaseGameController.Instance.Session.ScreenHeight, BaseGameController.Instance.Session.HeightScale);
			CaptionCreationController.gameObject.SetActive(true);
			CaptionCreationController.Initialize(this, aImageUrl);
		}

		void IMemeGeneration.HideMemeImage()
		{
			ImageSelectorController.gameObject.SetActive(true);
			CaptionCreationController.gameObject.SetActive(false);
		}

		void IMixGame.Initialize(MixGameData aData)
		{
			mData = GetGameData<MemeGeneratorData>();
			if (aData == null)
			{
				if (DebugSceneIndicator.IsDebugScene)
				{
					TestEntitlementGameData entitlement = new TestEntitlementGameData("gogs/meme_generator/game_assets", "meme_generator", false);
					mSession.Entitlement = entitlement;
					mBaseUrl = mSession.Entitlement.GetBaseUrl() + '/';
				}
				else
				{
					mBaseUrl = mSession.Entitlement.GetBaseUrl();
				}
				LoadData(this, "data/Meme_Generator_Data/Meme_Generator_Data.gz", "Meme_Generator_Data.txt", ParseJsonData);
			}
		}

		void IMixGame.Play()
		{
		}

		void IMixGame.Quit()
		{
		}

		void IMixGame.Pause()
		{
		}

		void IMixGame.Resume()
		{
		}

		void IMixGameDataRequest.OnDataLoaded(object aObject, object aUserData)
		{
			ContentObj contentObj = aObject as ContentObj;
			try
			{
				if (contentObj.content.objects.meme.Count == 0)
				{
					PauseOnNetworkError();
				}
				else
				{
					OnMemeDataLoaded(contentObj);
				}
			}
			catch (Exception)
			{
				PauseOnNetworkError();
			}
		}

		public override void SetupGameData()
		{
			mGameData = new MemeGeneratorData();
			mGameData.Type = MixGameDataType.INVITE;
		}

		public override void SetupGameData(MixGameData aData)
		{
			SetupGameData();
		}

		public override void SetupGameData(string aGameDataJson)
		{
			mGameData = JsonMapper.ToObject<MemeGeneratorData>(aGameDataJson);
			mGameData.Type = MixGameDataType.RESULT;
		}

		public override void Initialize(GameSession aSession, string aEntitlementId)
		{
			BaseGameController.mInstance = this;
			SetupGameData();
			mGameData.Entitlement = aEntitlementId;
			mSession = aSession;
			CaptionCreationController.gameObject.SetActive(false);
			MixGame = this;
			MixGame.Initialize();
		}

		private void OnMemeDataLoaded(ContentObj contentData)
		{
			if (mMemeGeneratorData == null)
			{
				mMemeGeneratorData = new List<MixGameContentData>();
				MixGameContentDataHelper.SetVisibleAndOwnedContent(contentData.content.objects.meme.Cast<MixGameContentData>().ToList(), ref mMemeGeneratorData);
				ImageSelectorController.Initialize(this, mMemeGeneratorData.Cast<Meme_Generator_Data>().ToList(), mBaseUrl);
			}
		}

		public object ParseJsonData(string json)
		{
			return JsonMapper.ToObject<ContentObj>(json);
		}
	}
}
