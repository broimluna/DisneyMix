using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Disney.DMOAnalytics;
using Disney.Mix.SDK;
using Mix.Connectivity;
using Mix.Session.Extensions;
using Mix.Session.Local;
using Mix.Ui;
using UnityEngine;

namespace Mix.Session
{
	public class MixSession
	{
		public enum ConnectionState
		{
			NOT_STARTED = 0,
			RESUMING = 1,
			ONLINE = 2
		}

		public delegate void ConnectionChanged(ConnectionState newState, ConnectionState oldState);

		public delegate void GotRecentMessages(bool success, bool areNewMessages);

		public delegate void SessionInitialized();

		private const int MaxRetryCount = 6;

		public static ConnectionState connection = ConnectionState.NOT_STARTED;

		public static bool isResuming = false;

		private static bool isPaused = false;

		private static int RetryCount = 0;

		private static int currValidResumeId = 0;

		private static SdkActions actionGenerator = new SdkActions();

		private static ILocalUser combinedUser;

		public static string GuestControllerSpoofedIP = null;

		public static ISession Session { get; private set; }

		public static ILocalUser User
		{
			get
			{
				object result;
				if (connection != ConnectionState.NOT_STARTED && IsValidSession)
				{
					ILocalUser localUser = combinedUser;
					result = localUser;
				}
				else
				{
					result = null;
				}
				return (ILocalUser)result;
			}
		}

		public static bool IsValidSession
		{
			get
			{
				return connection != ConnectionState.NOT_STARTED && Session != null && !Session.IsDisposed;
			}
		}

		public static bool ParentalConsentRequired
		{
			get
			{
				return IsValidSession && Session.LocalUser.RegistrationProfile.AccountStatus == AccountStatus.AwaitingParentalConsent;
			}
		}

		public static event ConnectionChanged OnConnectionChanged;

		public static event GotRecentMessages OnGotNewMessages;

		public static event SessionInitialized OnSessionInitialized;

		static MixSession()
		{
			MixSession.OnConnectionChanged = delegate
			{
			};
			MixSession.OnGotNewMessages = delegate
			{
			};
			MixSession.OnSessionInitialized = delegate
			{
			};
		}

		public static void InitializeSessionData(ISession aSessionData, ConnectionState state)
		{
			ConnectionState oldState = connection;
			connection = state;
			isPaused = state == ConnectionState.RESUMING;
			Session = aSessionData;
			combinedUser = new LocalUserWrapper(aSessionData.LocalUser);
			(combinedUser as LocalUserWrapper).LoadFakeFriendData();
			MixFriends.Init(combinedUser);
			MixChat.Init(combinedUser);
			MonoSingleton<LocalNotificationManager>.Instance.Init();
			if (MonoSingleton<ConnectionManager>.Instance != null)
			{
				MonoSingleton<ConnectionManager>.Instance.ConnectedEvent += OnInternetConnected;
				MonoSingleton<ConnectionManager>.Instance.DisconnectedEvent += OnInternetDisconnected;
			}
			MixSession.OnConnectionChanged(state, oldState);
			if (state == ConnectionState.ONLINE)
			{
				List<IChatMessage> latest = GetLatestMessages();
				combinedUser.GetRecentChatThreadMessages(delegate(IGetRecentChatThreadMessagesResult e2)
				{
					MixSession.OnGotNewMessages(e2.Success, e2.Success && !GetLatestMessages().SequenceEqual(latest));
					DMOAnalytics.SharedAnalytics.CanUseNetwork = true;
					DMOAnalytics.SharedAnalytics.FlushAnalyticsQueue();
				});
			}
			MonoSingleton<ChatPrimerManager>.Instance.ResetPriming();
			MixSession.OnSessionInitialized();
		}

		public static void PauseSession()
		{
			if (connection == ConnectionState.NOT_STARTED)
			{
				return;
			}
			isPaused = true;
			isResuming = false;
			currValidResumeId++;
			Session.Pause(actionGenerator.CreateAction(delegate(IPauseSessionResult e)
			{
				if (e.Success)
				{
					RetryCount = 0;
				}
			}));
			ConnectionState oldState = connection;
			connection = ConnectionState.RESUMING;
			MonoSingleton<PushNotifications>.Instance.OnSessionPause(true);
			MixSession.OnConnectionChanged(connection, oldState);
		}

		public static void UnPauseSession(Action<bool> callback)
		{
			if (!MonoSingleton<ConnectionManager>.Instance.IsConnected)
			{
				MonoSingleton<ConnectionManager>.Instance.ShowBanners(ConnectionManager.BannerState.Disconnected);
			}
			else if (isPaused && connection != ConnectionState.NOT_STARTED)
			{
				DMOAnalytics.SharedAnalytics.CanUseNetwork = false;
				if (!isResuming)
				{
					MonoSingleton<ChatPrimerManager>.Instance.ResetPriming();
					MonoSingleton<PushNotifications>.Instance.OnSessionPause(false);
				}
				isResuming = true;
				int resumeId = ++currValidResumeId;
				Session.Resume(actionGenerator.CreateAction(delegate(IResumeSessionResult e)
				{
					if (resumeId == currValidResumeId)
					{
						if (!e.Success)
						{
							MonoSingleton<ConnectionManager>.Instance.StartCoroutine(RetryAfterTime(0.5f + (float)(RetryCount * RetryCount), delegate
							{
								UnPauseSession(callback);
							}));
							if (RetryCount >= 6)
							{
								MonoSingleton<PushNotifications>.Instance.ClearNavRequest();
							}
						}
						else
						{
							MonoSingleton<ConnectionManager>.Instance.ShowBanners(ConnectionManager.BannerState.Hidden);
							RetryCount = 0;
							isPaused = false;
							isResuming = false;
							ConnectionState oldState = connection;
							connection = ConnectionState.ONLINE;
							if (callback != null)
							{
								callback(true);
							}
							MixSession.OnConnectionChanged(connection, oldState);
							MonoSingleton<PushNotifications>.Instance.RetryNavRequest(true);
							MonoSingleton<ConnectionManager>.Instance.ProcessQueue();
							List<IChatMessage> latest = GetLatestMessages();
							combinedUser.GetRecentChatThreadMessages(delegate(IGetRecentChatThreadMessagesResult getRecentChatThreadMessagesResult)
							{
								if (Session != null && combinedUser != null)
								{
									MixSession.OnGotNewMessages(getRecentChatThreadMessagesResult.Success, getRecentChatThreadMessagesResult.Success && !GetLatestMessages().SequenceEqual(latest));
								}
								DMOAnalytics.SharedAnalytics.CanUseNetwork = true;
								DMOAnalytics.SharedAnalytics.FlushAnalyticsQueue();
							});
							User.GetAllOfficialAccounts(actionGenerator.CreateAction(delegate(IGetAllOfficialAccountsResult aResult)
							{
								if (IsValidSession && User != null && User.Followships != null && aResult.Success && aResult.OfficialAccounts != null)
								{
									foreach (IOfficialAccount officialAccount in aResult.OfficialAccounts)
									{
										if (officialAccount != null && User.IsFollowingOfficialAccount(officialAccount.AccountId) && !officialAccount.IsAvailable && officialAccount.CanUnfollow)
										{
											User.UnfollowOfficialAccount(officialAccount, actionGenerator.CreateAction<IUnfollowOfficialAccountResult>(delegate
											{
											}));
										}
									}
								}
							}));
						}
					}
				}));
			}
			else
			{
				MonoSingleton<ConnectionManager>.Instance.ShowBanners(ConnectionManager.BannerState.Hidden);
			}
		}

		private static List<IChatMessage> GetLatestMessages()
		{
			List<IChatMessage> list = new List<IChatMessage>();
			foreach (IChatThread item in User.ChatThreadsFromUsers())
			{
				if (item.ChatMessages.Any())
				{
					list.Add(item.ChatMessages.Last());
				}
			}
			return list;
		}

		public static IEnumerator RetryAfterTime(float time, Action attempt)
		{
			if (++RetryCount > 6)
			{
				RetryCount = 6;
				MonoSingleton<ConnectionManager>.Instance.ShowBanners(ConnectionManager.BannerState.Disconnected);
			}
			yield return new WaitForSeconds(time);
			if (RetryCount >= 6)
			{
				MonoSingleton<ConnectionManager>.Instance.ShowBanners(ConnectionManager.BannerState.Reconnect);
			}
			attempt();
		}

		public static void AddOfflineItem(OfflineQueueItem item)
		{
			if (IsValidSession)
			{
				MonoSingleton<ConnectionManager>.Instance.AddToQueue(item);
			}
		}

		public static void DiscardSessionData()
		{
			ConnectionState oldState = connection;
			connection = ConnectionState.NOT_STARTED;
			isPaused = false;
			combinedUser = null;
			Session = null;
			isResuming = false;
			if (MonoSingleton<ConnectionManager>.Instance != null)
			{
				MonoSingleton<ConnectionManager>.Instance.ConnectedEvent -= OnInternetConnected;
				MonoSingleton<ConnectionManager>.Instance.DisconnectedEvent -= OnInternetDisconnected;
			}
			MixSession.OnConnectionChanged(connection, oldState);
		}

		public static void OnInternetConnected()
		{
			MonoSingleton<ConnectionManager>.Instance.ShowBanners(ConnectionManager.BannerState.Hidden);
			UnPauseSession(null);
		}

		public static void OnInternetDisconnected()
		{
			MonoSingleton<ConnectionManager>.Instance.ShowBanners(ConnectionManager.BannerState.Disconnected);
			PauseSession();
		}
	}
}
