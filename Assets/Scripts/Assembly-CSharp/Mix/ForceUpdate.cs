using System;
using Disney.MobileNetwork;
using Mix.Ui;

namespace Mix
{
	public class ForceUpdate : Singleton<ForceUpdate>
	{
		public bool Init()
		{
			ExternalizedConstants.OnExternalizedConstantsRefreshed += HandleOnExternalizedConstantsRefreshed;
			return true;
		}

		private void HandleOnExternalizedConstantsRefreshed(object sender, ExternalizedConstantsEventArgs args)
		{
			CheckForForceUpdate();
		}

		public bool CheckForForceUpdate()
		{
			if (AppNeedsForceUpdate())
			{
				DisplayForceUpdateDialog();
				return true;
			}
			return false;
		}

		private bool AppNeedsForceUpdate()
		{
			if (ExternalizedConstants.ForceUpdateVersion == null)
			{
				return false;
			}
			Version bundleVersion = EnvironmentManager.BundleVersion;
			Version version = new Version(ExternalizedConstants.ForceUpdateVersion);
			if (!(bundleVersion >= version))
			{
				return true;
			}
			return false;
		}

		private void DisplayForceUpdateDialog()
		{
			NavigationRequest navigationRequest = new NavigationRequest("Prefabs/Screens/Login/LoginScreen", new TransitionAnimations(false));
			navigationRequest.AddData("forceUpdate", true);
			navigationRequest.PopLastRequest = true;
			MonoSingleton<NavigationManager>.Instance.AddRequest(navigationRequest);
			Analytics.LogForceUpdatePanelPageView(EnvironmentManager.BundleVersion.ToString());
		}

		public static void GoToAppStore(bool fromPanel = false)
		{
			if (fromPanel)
			{
				Analytics.LogForceUpdateClicked();
			}
			string url = null;
			if (ExternalizedConstants.AndroidAppStoreLink != null)
			{
				url = ExternalizedConstants.AndroidAppStoreLink;
			}
			else
			{
				Log.Exception("Can't find Android App Store link!");
			}
			Application.OpenUrl(url);
		}
	}
}
