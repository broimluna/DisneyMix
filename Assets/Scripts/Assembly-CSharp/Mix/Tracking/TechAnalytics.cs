using System.Collections.Generic;
using System.Linq;
using Mix.Assets;
using Mix.Data;
using Mix.Games;
using UnityEngine;

namespace Mix.Tracking
{
	public class TechAnalytics : Singleton<TechAnalytics>
	{
		private bool appStartTracked;

		private float appFromBackgroundStart;

		private BaseGameData gameLoading;

		private float gameLoadingStartTime;

		private bool gameCached;

		private Dictionary<GameObject, float> compositeTimes = new Dictionary<GameObject, float>();

		private int frameCount;

		private float deltaTime;

		private float trackTime = 55f;

		private float updateRate = 4f;

		private float fps;

		private float CALL_TIME = 60f;

		public void SetAppLoaded()
		{
			appStartTracked = true;
		}

		public void TrackTimeToLogin()
		{
			if (!appStartTracked)
			{
				Analytics.LogTimingAction(null, "start_to_login", Time.realtimeSinceStartup.ToString(), null);
				appStartTracked = true;
			}
		}

		public void TrackTimeToConversations()
		{
			if (!appStartTracked)
			{
				Analytics.LogTimingAction(null, "start_to_chat", Time.realtimeSinceStartup.ToString(), null);
				appStartTracked = true;
			}
		}

		public void TrackTimeToCompositeAvatarStart(GameObject aAvatarObject)
		{
			compositeTimes[aAvatarObject] = Time.realtimeSinceStartup;
		}

		public void TrackTimeToCompositeAvatarEnd(GameObject aAvatarObject, bool success)
		{
			compositeTimes = compositeTimes.Where((KeyValuePair<GameObject, float> item) => item.Key != null).ToDictionary((KeyValuePair<GameObject, float> x) => x.Key, (KeyValuePair<GameObject, float> x) => x.Value);
			if (compositeTimes.ContainsKey(aAvatarObject))
			{
				compositeTimes.Remove(aAvatarObject);
			}
		}

		public void TrackTimeFromBackgroundStart()
		{
			appFromBackgroundStart = Time.realtimeSinceStartup;
		}

		public void TrackTimeFromBackgroundEnd()
		{
			if (!(appFromBackgroundStart <= 0f))
			{
				Analytics.LogTimingAction(null, "from_background", (Time.realtimeSinceStartup - appFromBackgroundStart).ToString(), null);
				appFromBackgroundStart = 0f;
			}
		}

		public void TrackTimeToLoadGameStart(BaseGameData aEntitlement)
		{
			gameLoading = aEntitlement;
			gameLoadingStartTime = Time.realtimeSinceStartup;
			gameCached = MonoSingleton<AssetManager>.Instance.IsBundleCached(gameLoading.GetHd());
		}

		public void TrackTimeToLoadGameEnd()
		{
			if (gameLoading != null && !(gameLoadingStartTime <= 0f))
			{
				gameLoading = null;
				gameLoadingStartTime = 0f;
			}
		}

		public void TrackFPSOnFriends()
		{
			GenerateFramerate();
			if (trackTime > CALL_TIME && fps > 0f)
			{
				trackTime = 0f;
				Analytics.LogGameAction("fps_friends", null, null, fps.ToString());
			}
		}

		public void TrackFPSOnConvo()
		{
			GenerateFramerate();
			if (trackTime > CALL_TIME && fps > 0f)
			{
				trackTime = 0f;
				Analytics.LogGameAction("fps_chats", null, null, fps.ToString());
			}
		}

		public void TrackFPSOnChat(bool aIsGroupChat)
		{
			GenerateFramerate();
			if (trackTime > CALL_TIME && fps > 0f)
			{
				trackTime = 0f;
				string action = null;
				if (!MonoSingleton<GameManager>.Instance.IsSessionsPaused && MonoSingleton<GameManager>.Instance.ActiveSession != null && MonoSingleton<GameManager>.Instance.ActiveSession.Entitlement != null)
				{
					action = MonoSingleton<GameManager>.Instance.ActiveSession.Entitlement.GetName();
				}
				Analytics.LogGameAction("fps_in_chat", action, (!aIsGroupChat) ? "1:1" : "group", fps.ToString());
			}
		}

		public void TrackFPSOnAvatarEditor()
		{
			GenerateFramerate();
			if (trackTime > CALL_TIME && fps > 0f)
			{
				trackTime = 0f;
				Analytics.LogGameAction("fps_avatar", null, null, fps.ToString());
			}
		}

		private void GenerateFramerate()
		{
			frameCount++;
			deltaTime += Time.deltaTime;
			trackTime += Time.deltaTime;
			if (deltaTime > 1f / updateRate)
			{
				fps = (float)frameCount / deltaTime;
				frameCount = 0;
				deltaTime -= 1f / updateRate;
			}
		}
	}
}
