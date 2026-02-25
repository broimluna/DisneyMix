using System;
using System.Collections;
using System.Collections.Generic;
using Mix.Games.Data;
using Mix.Games.Message;
using Mix.Games.Session;
using Mix.Games.Tray;
using UnityEngine;

namespace Mix.Games
{
	public abstract class BaseGameController : MonoBehaviour
	{
		public GameObject MixGameObject;

		public Camera MixGameCamera;

		public IMixGame MixGame;

		public IMixGameTimer MixGameTimer;

		public GameAudio AudioManager;

		protected MixGameData mGameData;

		protected GameSession mSession;

		protected int mGameClock;

		protected float mTimeRemaining = 30f;

		protected IEnumerator mCountdown;

		protected static BaseGameController mInstance;

		private float mTimeInterval;

		public static BaseGameController Instance
		{
			get
			{
				return mInstance;
			}
		}

		public string FriendName
		{
			get
			{
				return mSession.GetFriendName();
			}
		}

		public string PlayerId
		{
			get
			{
				return mSession.PlayerId;
			}
		}

		public string PlayerName
		{
			get
			{
				return mSession.GetPlayerName(mSession.PlayerId);
			}
		}

		public string OwnerId
		{
			get
			{
				if (mSession.MessageData != null)
				{
					return mSession.MessageData.SenderId;
				}
				Debug.LogWarning("No message data for this session");
				return string.Empty;
			}
		}

		public int NumberOfGamesCompleted
		{
			get
			{
				return mSession.GetNumberOfCompletedSessions();
			}
		}

		public bool IsGroupSession
		{
			get
			{
				return mSession.IsGroupSession;
			}
		}

		public GameSession Session
		{
			get
			{
				return mSession;
			}
		}

		private void Awake()
		{
		}

		public void CancelBundles()
		{
			mSession.CancelBundles();
		}

		public void CancelBundles(string aUrl)
		{
			mSession.CancelBundles(aUrl);
		}

		public virtual void CleanUpGame()
		{
			if (MixGame != null)
			{
				MixGame.Quit();
			}
		}

		public virtual void CloseGame()
		{
			if (mSession != null)
			{
				mSession.QuitSession();
			}
		}

		public void DestroyBundleInstance(string aPath, UnityEngine.Object aObject = null)
		{
			mSession.DestroyBundleInstance(aPath, aObject);
		}

		public virtual void PauseOnNetworkError(bool retryGame = false)
		{
			mSession.PauseOnNetworkError();
			if (retryGame)
			{
				float time = 3f;
				Invoke("RetryGame", time);
			}
		}

		private void RetryGame()
		{
			mSession.ResumeNetworkError();
		}

		public UnityEngine.Object GetBundleInstance(string aPath)
		{
			return mSession.GetBundleInstance(aPath);
		}

		public void LoadAsset(IGameAsset aSessionAsset, string aPath, object aParam = null)
		{
			mSession.LoadAsset(aSessionAsset, aPath, aParam);
		}

		public void LoadData(IMixGameDataRequest aGameDataRequester, string aPath, string aFileName, Func<string, object> aMethod)
		{
			mSession.LoadData(aGameDataRequester, aPath, aFileName, aMethod);
		}

		public void LoadFriend(GameObject aMesh, string aPlayerId, bool aHideCostumes = false, bool aHideGeoAcessories = false)
		{
			mSession.LoadFriend(aMesh, aPlayerId, aHideCostumes, aHideGeoAcessories);
		}

		public void LoadRandomFriend(GameObject aMesh)
		{
			mSession.LoadRandomFriend(aMesh);
		}

		public void LoadSnapshot(string aPlayerId, int size, Action<bool, Sprite> callback)
		{
			mSession.LoadSnapshot(aPlayerId, size, callback);
		}

		public void LogEvent(GameLogEventType aEventType, object aGameParameter, object aGameMessage = null)
		{
			if (mSession != null)
			{
				switch (aEventType)
				{
				case GameLogEventType.ACTION:
					mSession.LogAction((string)aGameParameter, aGameMessage);
					break;
				case GameLogEventType.PAGEVIEW:
					mSession.LogPageView((string)aGameParameter);
					break;
				case GameLogEventType.TIMING:
					mSession.LogTiming((double)aGameParameter);
					break;
				}
			}
		}

		public string GetFriendName(string aPlayerId)
		{
			return mSession.GetPlayerName(aPlayerId);
		}

		public T GetGameData<T>() where T : MixGameData
		{
			return (T)mGameData;
		}

		public virtual void GameOver(object gameData)
		{
			mSession.GameStatistics.LastPlayed = DateTime.Now;
			mSession.GameStatistics.SessionsCompleted++;
			if (gameData is MixGameData)
			{
				mSession.EndSession(gameData);
			}
			else if (gameData is MixGameResponse)
			{
				mSession.UpdateSession(mGameData, gameData as MixGameResponse);
			}
		}

		public string GetLocalizedString(string aToken)
		{
			return mSession.GetLocalizedString(aToken);
		}

		public abstract void SetupGameData();

		public abstract void SetupGameData(MixGameData aData);

		public abstract void SetupGameData(string aGameDataJson);

		public virtual void Initialize(GameSession aSession)
		{
			mInstance = this;
			if (aSession.MessageData is IGameEventMessageData)
			{
				Dictionary<string, object> state = (aSession.MessageData as IGameEventMessageData).State;
				string aGameDataJson = state["GameData"] as string;
				SetupGameData(aGameDataJson);
			}
			else
			{
				SetupGameData(aSession.MessageData.GetJson());
			}
			mSession = aSession;
			MixGame = (IMixGame)MixGameObject.GetComponent(typeof(IMixGame));
			MixGame.Initialize(mGameData);
			mSession.GameStatistics.SessionsStarted++;
		}

		public virtual void Initialize(GameSession aSession, string aEntitlementId)
		{
			mInstance = this;
			SetupGameData();
			mGameData.Entitlement = aEntitlementId;
			mSession = aSession;
			MixGame = (IMixGame)MixGameObject.GetComponent(typeof(IMixGame));
			MixGame.Initialize();
			mSession.GameStatistics.SessionsStarted++;
		}

		public virtual void Initialize(GameSession aSession, MixGameData aData)
		{
			mInstance = this;
			SetupGameData(aData);
			mSession = aSession;
			MixGame = (IMixGame)MixGameObject.GetComponent(typeof(IMixGame));
			MixGame.Initialize(aData);
			mSession.GameStatistics.SessionsStarted++;
		}

		public virtual void PlayGame()
		{
			mSession.UpdateGameSessionState(GameSessionState.PLAYING);
			MixGame.Play();
		}

		public virtual void PauseGame()
		{
			if (mSession != null && MixGame != null)
			{
				mSession.UpdateGameSessionState(GameSessionState.PAUSED);
				MixGame.Pause();
			}
			if (MixGameTimer != null)
			{
				StopCountdown();
			}
		}

		public void PreloadAvatar(string aPlayerId = null)
		{
			mSession.PreloadAvatar(aPlayerId);
		}

		public virtual void QuitGame()
		{
			if (MixGame != null)
			{
				MixGame.Quit();
			}
			UnityEngine.Object.Destroy(base.gameObject);
		}

		public virtual void ResumeGame()
		{
			mSession.UpdateGameSessionState(GameSessionState.PLAYING);
			MixGame.Resume();
			if (MixGameTimer != null)
			{
				ResumeCountdown();
			}
		}

		public void ResumeCountdown()
		{
			mCountdown = CountdownGame(mTimeRemaining);
			StartCoroutine(mCountdown);
		}

		public void StartCountdown(int aTime = 30, float aTimeInterval = 1f)
		{
			mTimeInterval = aTimeInterval;
			MixGameTimer = MixGame as IMixGameTimer;
			if (MixGameTimer == null)
			{
				mSession.Logger.LogError("IMixGameTimer not implemented. Unable to start game countdown");
				return;
			}
			mGameClock = aTime;
			mCountdown = CountdownGame(mGameClock);
			StartCoroutine(mCountdown);
		}

		public void StopCountdown()
		{
			if (mCountdown != null && !mCountdown.Equals(null))
			{
				StopCoroutine(mCountdown);
			}
		}

		public float GetCurrentTime()
		{
			return mTimeRemaining;
		}

		private IEnumerator CountdownGame(float aTimeRemaining)
		{
			for (float i = aTimeRemaining; i >= 0f; i -= mTimeInterval)
			{
				mTimeRemaining = i;
				yield return new WaitForSeconds(mTimeInterval);
				if (i == (float)mGameClock)
				{
					MixGameTimer.GameTimerStart();
				}
				else if (i == 0f)
				{
					MixGameTimer.GameTimerComplete();
				}
				else
				{
					MixGameTimer.GameTimerProgress(i);
				}
			}
		}

		public void ModerateText(string aTextToModerate, IGameModerationResult aGame, object aUserData)
		{
			mSession.ModerateText(aTextToModerate, aGame, aUserData);
		}
	}
}
