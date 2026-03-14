using Disney.DMOAnalytics;
using Disney.MobileNetwork;
using UnityEngine;

namespace Mix
{
	public class MobileNetworkManager : Singleton<MobileNetworkManager>
	{
		public void Init()
		{
			InitEnvironmentManager();
            //InitKochava();
            //InitAnalytics();
			InitHockeyAppManager();
            //InitReferralStore();
        }

        public void InitEnvironmentManager()
		{
			if (!Service.IsSet<EnvironmentManagerWindows>())
			{
				EnvironmentManager environmentManager = null;
				GameObject gameObject = new GameObject();
				gameObject.name = typeof(EnvironmentManager).Name;
				environmentManager = gameObject.AddComponent<EnvironmentManagerWindows>();
				Service.Set(environmentManager);
				environmentManager.SetLogger(LoggerDelegate);
				environmentManager.Initialize();
				if (!string.IsNullOrEmpty(EnvironmentManager.SKU))
				{
				}
			}
		}

		private void InitAnalytics()
		{
			string text = BuildSettings.Get("mobilenetwork.dmoAnalytics.key", string.Empty);
			string text2 = BuildSettings.Get("mobilenetwork.dmoAnalytics.secret", string.Empty);
			if (!Service.IsSet<AnalyticsManager>() && !string.IsNullOrEmpty(text) && !string.IsNullOrEmpty(text2))
			{
				AnalyticsManager analyticsManager = null;
				GameObject gameObject = new GameObject();
				gameObject.name = typeof(AnalyticsManager).Name;
				analyticsManager = gameObject.AddComponent<AnalyticsManager>();
				Service.Set(analyticsManager);
				analyticsManager.SetLogger(LoggerDelegate);
				analyticsManager.SetAutoLogLifetimeEvents(true);
				analyticsManager.SetLogEventsInEditor(true);
				analyticsManager.SetKeys(text, text2);
				analyticsManager.Init();
				DMOAnalytics.SharedAnalytics.CanUseNetwork = false;
			}
		}

		private void InitKochava()
		{
			string text = BuildSettings.Get("mobilenetwork.kochava.appID", string.Empty);
			if (!Service.IsSet<KochavaManager>() && !string.IsNullOrEmpty(text))
			{
				KochavaManager kochavaManager = null;
				GameObject gameObject = new GameObject();
				gameObject.name = typeof(KochavaManager).Name;
				kochavaManager = gameObject.AddComponent<KochavaManager>();
				Service.Set(kochavaManager);
				KochavaManager.KochavaConfiguration kochavaConfiguration = new KochavaManager.KochavaConfiguration();
				kochavaConfiguration.KochavaAppIdAndroid = text;
				kochavaConfiguration.IncognitoMode = false;
				kochavaConfiguration.RequestAttribution = false;
				kochavaConfiguration.AppCurrency = "USD";
				kochavaManager.Configure(kochavaConfiguration);
				kochavaManager.Init();
			}
		}

		public void InitHockeyAppManager()
		{
			if (!Service.IsSet<HockeyAppManager>())
			{
				string appId = BuildSettings.Get("mobilenetwork.hockeyApp.appID", string.Empty);
				if (string.IsNullOrEmpty(appId))
				{
					Debug.LogWarning("[MobileNetworkManager] HockeyApp disabled: missing 'mobilenetwork.hockeyApp.appID' in BuildSettings.");
					return;
				}

				GameObject gameObject = new GameObject();
				gameObject.name = typeof(HockeyAppManager).Name;
				HockeyAppManager hockeyAppManager = null;
				hockeyAppManager = gameObject.AddComponent<HockeyAppManagerAndroid>();
				hockeyAppManager.exceptionLogging = true;
				Service.Set(hockeyAppManager);
				hockeyAppManager.appID = appId;
				hockeyAppManager.packageID = EnvironmentManager.BundleIdentifier;
				hockeyAppManager.Init();
			}
		}

		public void LoggerDelegate(object sourceObject, string message, LogType logType)
		{
			switch (logType)
			{
			case LogType.Exception:
				Log.Exception(message);
				break;
			case LogType.Error:
			case LogType.Assert:
				break;
			case LogType.Log:
				break;
			case LogType.Warning:
				break;
			}
		}

		public void InitReferralStore()
		{
			if (!Service.IsSet<ReferralStoreManager>())
			{
				GameObject gameObject = new GameObject();
				gameObject.name = typeof(ReferralStoreManager).Name;
				ReferralStoreManager referralStoreManager = null;
				referralStoreManager = gameObject.AddComponent<ReferralStoreAndroidManager>();
				Service.Set(referralStoreManager);
				referralStoreManager.Init();
			}
		}
	}
}
