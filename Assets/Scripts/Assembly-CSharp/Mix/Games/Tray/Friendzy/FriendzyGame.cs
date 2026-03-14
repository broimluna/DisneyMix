using System;
using DG.Tweening;
using LitJson;
using Mix.Games.Data;
using Mix.Games.Tray.Common;
using Mix.Games.Tray.Friendzy.BILog;
using Mix.Games.Tray.Friendzy.Camera;
using Mix.Games.Tray.Friendzy.Data;
using Mix.Games.Tray.Friendzy.FSM;
using Mix.Games.Tray.Friendzy.FriendzyQuiz;
using Mix.Games.Tray.Friendzy.LoadAsset;
using Mix.Games.Tray.Friendzy.Menu;
using Mix.Games.Tray.Friendzy.ResultShow;
using UnityEngine;

namespace Mix.Games.Tray.Friendzy
{
	public class FriendzyGame : MonoBehaviour, IMixGame, IMixGameDataRequest
	{
		private const float ZOOM_DURATION = 1.5f;

		private const float TIME_TO_STOP_RENDER_GEOMETRY_AFTER_MENU = 1f;

		private FiniteStateMachine mFiniteStateMachine;

		private PlayerOneState mStatePlayerOneState;

		private QuizState mStateQuizState;

		private ResultState mStateResultState;

		private string mSCategory;

		private string mSQuiz;

		private DataStoreAccessor mDataStore;

		public MenuController FriendzyMenuController;

		public MenuSystem FriendzyMenuSystem;

		private MenuMessage mMenuMessage;

		public QuizController FriendzyQuizController;

		public QuizShow FriendzyQuizShow;

		private ResultController mFriendzyResultController;

		public ResultStage FriendzyResultStage;

		private Loader mLoader;

		public bool Player_1;

		public CameraController CamController;

		public GameObject WorldGeometry;

		public GameObject PlayerOneAvatar;

		public GameObject PlayerTwoAvatar;

		public static string SOUND_PREFIX = "Friendly/SFX/";

		public static string MUSIC_CUE_NAME = "Friendly/MUS/Main";

		public FriendzyGameController GameController
		{
			get
			{
				return BaseGameController.Instance as FriendzyGameController;
			}
		}

		public PLAYER WhatPlayerIsPlaying { get; set; }

		public ResultEntry InitialPlayerResultEntry { get; set; }

		void IMixGame.Initialize(MixGameData aData)
		{
			if (aData == null)
			{
				EnterPlayState();
				return;
			}
			FriendzyData gameData = GameController.GetGameData<FriendzyData>();
			ResultEntry resultEntry = new ResultEntry();
			resultEntry.mIP = gameData.Category;
			resultEntry.mQuiz = gameData.Quiz;
			resultEntry.PlayerNumber = PLAYER.PLAYER_1;
			resultEntry.QuizTitle = gameData.QuizTitle;
			EnterReplayState(resultEntry);
		}

		void IMixGame.Pause()
		{
			BaseGameController.Instance.Session.SessionSounds.PauseSoundEvent(MUSIC_CUE_NAME, base.gameObject);
			DOTween.PauseAll();
		}

		void IMixGame.Play()
		{
			DOVirtual.DelayedCall(0.1f, delegate
			{
				BaseGameController.Instance.Session.SessionSounds.PlayMusic(MUSIC_CUE_NAME, base.gameObject);
			});
		}

		void IMixGame.Quit()
		{
			BaseGameController.Instance.Session.SessionSounds.StopSoundEvent(MUSIC_CUE_NAME, base.gameObject);
			DOTween.KillAll();
		}

		void IMixGame.Resume()
		{
			DOVirtual.DelayedCall(0.1f, delegate
			{
				BaseGameController.Instance.Session.SessionSounds.UnpauseSoundEvent(MUSIC_CUE_NAME, base.gameObject);
			});
			DOTween.PlayAll();
		}

		void IMixGameDataRequest.OnDataLoaded(object aObject, object aUserData)
		{
			DataStore dataStore = aObject as DataStore;
			try
			{
				if (dataStore == null)
				{
					GameController.PauseOnNetworkError();
				}
				else if (WhatPlayerIsPlaying == PLAYER.PLAYER_1)
				{
					OnDataLoaded(dataStore);
				}
				else
				{
					OnDataLoadedPlayerTwo(dataStore);
				}
			}
			catch (Exception)
			{
				GameController.PauseOnNetworkError();
			}
		}

		private void OnEnable()
		{
			TrayGamePhysics.ResetAllGameLayers();
		}

		private void EnterPlayState()
		{
			mLoader = base.gameObject.GetComponent<Loader>();
			SubSystemInitialization();
			mStatePlayerOneState = new PlayerOneState(this);
			WhatPlayerIsPlaying = PLAYER.PLAYER_1;
			mFiniteStateMachine.CurrentState = mStatePlayerOneState;
			mFiniteStateMachine.CurrentState.Enter();
			LoadConfigDataInitialization();
			CamController.IntroZoom(1.5f).AppendCallback(CameraZoomInCallback);
			LoadPlayerAvatars();
		}

		private void EnterReplayState(ResultEntry aResultEntry)
		{
			mLoader = base.gameObject.GetComponent<Loader>();
			SubSystemInitialization();
			mSCategory = aResultEntry.mIP;
			mSQuiz = aResultEntry.mQuiz;
			mFiniteStateMachine.CurrentState = mStateQuizState;
			WhatPlayerIsPlaying = PLAYER.PLAYER_2;
			InitialPlayerResultEntry = aResultEntry;
			LoadConfigDataInitialization();
			CamController.IntroZoom(1.5f);
			LoadPlayerAvatars();
		}

		public void GameOverFirst(ResultEntry aResultEntry)
		{
			FriendzyData gameData = GameController.GetGameData<FriendzyData>();
			gameData.Category = aResultEntry.mIP;
			gameData.Quiz = aResultEntry.mQuiz;
			gameData.QuizTitle = aResultEntry.QuizTitle;
			CreateResponse(aResultEntry);
			GameController.GameOver(gameData);
		}

		public void GameOverResponse(ResultEntry aResultEntry)
		{
			if (DebugSceneIndicator.IsMainScene)
			{
				FriendzyResponse gameData = CreateResponse(aResultEntry);
				GameController.GameOver(gameData);
			}
		}

		private FriendzyResponse CreateResponse(ResultEntry aResultEntry)
		{
			FriendzyData gameData = GameController.GetGameData<FriendzyData>();
			FriendzyResponse myResponse = gameData.GetMyResponse(gameData.Responses, GameController.PlayerId);
			myResponse.Result = aResultEntry.mResult.Message;
			myResponse.ImageUrl = aResultEntry.mResult.Pic.GetImageURL();
			myResponse.Attempts++;
			return myResponse;
		}

		private void SubSystemInitialization()
		{
			mStateQuizState = new QuizState(this);
			mStateResultState = new ResultState(this);
			mFiniteStateMachine = new FiniteStateMachine();
			FriendzyQuizController = new QuizController(this);
			mFriendzyResultController = new ResultController(this);
		}

		private void LoadConfigDataInitialization()
		{
			GameController.LoadData(this, "data/Friendzy_Data/Friendzy_Data.gz", "Friendzy_Data.txt", ParseJsonData);
		}

		public void ReceiveMessage(ref FriendzyMessage message)
		{
			switch (message.type)
			{
			case FriendzyMessageType.IP_QUIZ_PICKED:
				mSCategory = message.Category;
				mSQuiz = message.Quiz;
				LogFriendzyBILog.FriendzyLogIPChosen(GameController, mSCategory);
				LogFriendzyBILog.FriendzyLogQuizChosen(GameController, mSQuiz);
				mFiniteStateMachine.ChangeToState(mStateQuizState);
				break;
			case FriendzyMessageType.QUIZ_FINISHED:
			{
				mFiniteStateMachine.ChangeToState(mStateResultState);
				mFriendzyResultController.FriendzyResultEntry = message.resultEntry;
				StartRenderingWorldGeometry();
				ResultMessage aResultMessage2 = new ResultMessage(ResultMessageType.START_THE_SHOW, message.resultEntry, PLAYER.PLAYER_1, GetCurrentCategoryInstance(), GetCurrentQuizInstance());
				mFriendzyResultController.ReceiveMessage(ref aResultMessage2);
				break;
			}
			case FriendzyMessageType.LOAD_DATA:
				mLoader.LoadJob(message.jobToDo);
				break;
			case FriendzyMessageType.FRIENDZY_FINISHED:
				if (WhatPlayerIsPlaying == PLAYER.PLAYER_1)
				{
					GameOverFirst(message.resultEntry);
				}
				else
				{
					GameOverResponse(message.resultEntry);
				}
				break;
			case FriendzyMessageType.LOAD_RESULT_ASSETS:
			{
				ResultMessage aResultMessage = new ResultMessage(ResultMessageType.LOAD_RESULT_ASSETS, message.CategoryReference, message.QuizReference);
				mFriendzyResultController.ReceiveMessage(ref aResultMessage);
				break;
			}
			case FriendzyMessageType.TURN_OFF_LEVEL_GEOMETRY:
			{
				Sequence s = DOTween.Sequence();
				s.InsertCallback(1f, StopRenderingWorldGeometry);
				break;
			}
			case FriendzyMessageType.BACK_BUTTON_PRESSED:
				LogFriendzyBILog.FriendzyLogBackButton(GameController, message.Category);
				break;
			}
		}

		public Category[] GetCategories()
		{
			return mDataStore.GetCategories();
		}

		public string[] GetQuizzesByIP(string inCategory)
		{
			return mDataStore.GetQuizzesByIP(inCategory);
		}

		public Quiz GetCurrentQuizInstance()
		{
			return mDataStore.GetQuizByCategoryAndQuizName(mSCategory, mSQuiz);
		}

		public Category GetCurrentCategoryInstance()
		{
			return mDataStore.GetCategoryByName(mSCategory);
		}

		public object ParseJsonData(string json)
		{
			try
			{
				JsonMapper.RegisterImporter((int value) => value.ToString());
				return JsonMapper.ToObject<DataStore>(json);
			}
			catch (Exception)
			{
				return null;
			}
		}

		private void OnDataLoaded(DataStore data)
		{
			mDataStore = new DataStoreAccessor(data);
			Job job = new Job();
			job.CallBackWhenFinishedJob = JobHasFinishedForPlayerOne;
			FriendzyMessage message = new FriendzyMessage(FriendzyMessageType.LOAD_DATA, null, null, job);
			Category[] categories = mDataStore.GetCategories();
			job.NumberOfItems = categories.Length * 3;
			job.ObjectsToLoad = new object[job.NumberOfItems];
			int num = 0;
			for (int i = 0; i < categories.Length; i++)
			{
				job.ObjectsToLoad[num] = categories[i].LoadLogoPicture();
				num++;
				job.ObjectsToLoad[num] = categories[i].LoadCategoryPalettePicture();
				num++;
				job.ObjectsToLoad[num] = categories[i].LoadLogoBackgroundPicture();
				num++;
			}
			ReceiveMessage(ref message);
		}

		private void OnDataLoadedPlayerTwo(DataStore data)
		{
			mDataStore = new DataStoreAccessor(data);
			Job job = new Job();
			job.CallBackWhenFinishedJob = JobHasFinishedForPlayerTwo;
			FriendzyMessage message = new FriendzyMessage(FriendzyMessageType.LOAD_DATA, null, null, job);
			Category categoryByName = mDataStore.GetCategoryByName(mSCategory);
			job.NumberOfItems = 3;
			job.ObjectsToLoad = new object[job.NumberOfItems];
			job.ObjectsToLoad[0] = categoryByName.LoadLogoPicture();
			job.ObjectsToLoad[1] = categoryByName.LoadLogoBackgroundPicture();
			job.ObjectsToLoad[2] = categoryByName.LoadCategoryPalettePicture();
			ReceiveMessage(ref message);
		}

		private void JobHasFinishedForPlayerOne(Job aJob)
		{
			mMenuMessage.Type = MenuMessageType.IP_QUIZ_DATA_LOADED;
			FriendzyMenuController.ReceiveMessage(ref mMenuMessage);
		}

		private void JobHasFinishedForPlayerTwo(Job aJob)
		{
			mFiniteStateMachine.CurrentState.Enter();
		}

		private void CameraZoomInCallback()
		{
			MenuMessage message = new MenuMessage(MenuMessageType.MENU_IMPLODE);
			FriendzyMenuController.ReceiveMessage(ref message);
		}

		public CameraController GetCameraController()
		{
			return CamController;
		}

		private void StopRenderingWorldGeometry()
		{
			WorldGeometry.SetActive(false);
		}

		private void StartRenderingWorldGeometry()
		{
			WorldGeometry.SetActive(true);
		}

		private void LoadPlayerAvatars()
		{
			if (!DebugSceneIndicator.IsDebugScene)
			{
				GameController.LoadFriend(PlayerOneAvatar, GameController.PlayerId);
				if (WhatPlayerIsPlaying == PLAYER.PLAYER_2)
				{
					GameController.LoadFriend(PlayerTwoAvatar, GameController.OwnerId);
				}
			}
			PlayerOneAvatar.GetComponent<Animator>().SetTrigger("Entry");
		}

		public static void PlaySound(string aSoundName, string aPrefix)
		{
			BaseGameController.Instance.Session.SessionSounds.PlaySoundEvent(aPrefix + aSoundName);
		}

		public static void PlaySound(string aSoundWithPrefix)
		{
			BaseGameController.Instance.Session.SessionSounds.PlaySoundEvent(aSoundWithPrefix);
		}

		public static void StopSound(string aSoundName, string aPrefix)
		{
			BaseGameController.Instance.Session.SessionSounds.StopSoundEvent(aPrefix + aSoundName);
		}

		public static void SetVolumeEvent(string eventName, float volume)
		{
			BaseGameController.Instance.Session.SessionSounds.SetVolumeEvent(eventName, volume);
		}
	}
}
