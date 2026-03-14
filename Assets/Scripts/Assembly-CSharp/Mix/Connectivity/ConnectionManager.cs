using System.Collections.Generic;
using Mix.Session;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Connectivity
{
	public class ConnectionManager : MonoSingleton<ConnectionManager>
	{
		public enum BannerState
		{
			Hidden = 0,
			Disconnected = 1,
			Reconnect = 2
		}

		public delegate void OnConnected();

		public delegate void OnDisconnected();

		private const float connectionCheckInterval = 0.75f;

		public bool IsConnected;

		private OfflineQueue offlineQueue;

		public BannerState currentBannerState;

		private List<GameObject> bannerObjects = new List<GameObject>();

		private float timeToCheckConnection;

		private bool disconnectedSoundPlayed;

		public bool IsBannerShowing
		{
			get
			{
				return BannerState.Hidden != currentBannerState;
			}
		}

		public event OnConnected ConnectedEvent;

		public event OnDisconnected DisconnectedEvent;

		public void Awake()
		{
			IsConnected = Application.InternetReachability != 0;
		}

		public void Start()
		{
			offlineQueue = new OfflineQueue();
		}

		public void RegisterBanner(GameObject aObject)
		{
			if (aObject.transform.Find("Disconnected/CloseButton") == null)
			{
				return;
			}
			if (!bannerObjects.Contains(aObject) && aObject != null)
			{
				bannerObjects.Add(aObject);
				aObject.transform.Find("Disconnected/CloseButton").GetComponent<Button>().onClick.AddListener(delegate
				{
					ShowBanners(BannerState.Hidden);
				});
			}
			InternalShowBanners(currentBannerState);
		}

		public void UnRegisterBanner(GameObject aObject)
		{
			if (bannerObjects.Contains(aObject) && aObject != null)
			{
				bannerObjects.Remove(aObject);
			}
		}

		public void AddToQueue(OfflineQueueItem item)
		{
			offlineQueue.Add(MixSession.User.Id, item);
		}

		public void ProcessQueue()
		{
			if (MixSession.IsValidSession)
			{
				offlineQueue.Process(MixSession.User.Id);
			}
		}

		public void ShowBanners(BannerState newBannerState)
		{
			if (newBannerState != currentBannerState)
			{
				if (newBannerState == BannerState.Hidden)
				{
					disconnectedSoundPlayed = false;
				}
				currentBannerState = newBannerState;
				InternalShowBanners(newBannerState);
			}
		}

		private void InternalShowBanners(BannerState newBannerState)
		{
			if (bannerObjects == null || bannerObjects.Count <= 0)
			{
				return;
			}
			bannerObjects.ForEach(delegate(GameObject item)
			{
				if (item != null)
				{
					if (newBannerState == BannerState.Hidden)
					{
						item.SetActive(false);
					}
					else
					{
						item.SetActive(true);
						bool flag = newBannerState == BannerState.Disconnected;
						item.transform.Find("Disconnected").gameObject.SetActive(flag);
						item.transform.Find("Reconnect").gameObject.SetActive(!flag);
					}
				}
			});
			if (newBannerState == BannerState.Disconnected && Singleton<SoundManager>.Instance != null && !disconnectedSoundPlayed)
			{
				Singleton<SoundManager>.Instance.PlaySoundEvent("MainApp/UI/SFX_1_Error");
				disconnectedSoundPlayed = true;
			}
		}

		private void Update()
		{
			if (Time.realtimeSinceStartup > timeToCheckConnection)
			{
				timeToCheckConnection = Time.realtimeSinceStartup + 0.75f;
				checkConnecion();
			}
		}

		private void checkConnecion()
		{
			bool flag = Application.InternetReachability != 0;
			if (IsConnected != flag)
			{
				IsConnected = flag;
				if (IsConnected)
				{
					SendEvent(true);
				}
				else
				{
					SendEvent(false);
				}
			}
		}

		public void OnApplicationPausing(bool goingToBackground)
		{
			if (!goingToBackground)
			{
				checkConnecion();
			}
		}

		private void SendEvent(bool aState)
		{
			if (aState && this.ConnectedEvent != null)
			{
				this.ConnectedEvent();
			}
			else if (!aState && this.DisconnectedEvent != null)
			{
				this.DisconnectedEvent();
			}
		}
	}
}
