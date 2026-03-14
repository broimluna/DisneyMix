using Disney.MobileNetwork;
using Disney.Native;
using Mix.Localization;
using Mix.Native;
using UnityEngine;

namespace Mix.SequenceOperations
{
	public class ExternalLibraryStartOperation : SequenceOperation
	{
		public ExternalLibraryStartOperation(IOperationCompleteHandler aCaller)
			: base(aCaller)
		{
		}

		public override void StartOperation()
		{
			BaseStartOperation();
			Singleton<MobileNetworkManager>.Instance.Init();
			EnvironmentManager.ShowStatusBar(true);
			//InitSecurityUtilsPlugin();
			//InitAccessibilityPlugin();
			InitSystemTextPlugin();
			//InitMemoryMonitorPlugin();
			//MonoSingleton<NativeVideoPlaybackManager>.Instance.Init();
			//MonoSingleton<NativeKeyboardManager>.Instance.Init();
			//MonoSingleton<NativeUtilitiesManager>.Instance.Init();
			//MonoSingleton<NativeCameraManager>.Instance.Init();
			//if (Service.Get<SecurityUtilsManager>().IsDebuggerAttached() && (string.IsNullOrEmpty(ConfigurationManager.EnvironmentString) || ConfigurationManager.EnvironmentString == "prod"))
			//{
				//UnityEngine.Application.Quit();
			//}
			finish(OperationStatus.STATUS_SUCCESSFUL);
		}

		private void InitSecurityUtilsPlugin()
		{
			GameObject gameObject = new GameObject();
			gameObject.name = typeof(SecurityUtilsManager).Name;
			gameObject.AddComponent<SecurityUtilsAndroidManager>();
		}

		private void InitAccessibilityPlugin()
		{
			MonoSingleton<NativeAccessiblityManager>.Instance.Init(Singleton<Localizer>.Instance);
			MonoSingleton<NativeAccessiblityManager>.Instance.CheckSwitchControlEnabled();
			GameObject gameObject = new GameObject();
			gameObject.name = typeof(AccessibilityManager).Name;
			gameObject.AddComponent<AccessibilityAndroidManager>();
		}

		private void InitSystemTextPlugin()
		{
			GameObject gameObject = new GameObject();
			gameObject.name = typeof(SystemTextManager).Name;
			gameObject.AddComponent<SystemTextManager>();
		}

		private void InitMemoryMonitorPlugin()
		{
			GameObject gameObject = new GameObject();
			gameObject.name = typeof(MemoryMonitorManager).Name;
			gameObject.AddComponent<MemoryMonitorAndroidManager>();
		}

		private void InitNativeLoggerPlugin()
		{
			GameObject gameObject = new GameObject();
			gameObject.name = typeof(NativeLogger).Name;
			gameObject.AddComponent<NativeAndroidLogger>();
		}
	}
}
