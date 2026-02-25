using System.Collections.Generic;
using Disney.Native;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class CameraTray : MonoBehaviour
	{
		public GameObject statusMessage;

		private INativeCamera listener;

		private bool isActive;

		public void Init(INativeCamera aListener)
		{
			listener = aListener;
		}

		public void UpdateState(bool aState)
		{
			base.gameObject.SetActive(aState);
		}

		private void RequestPermissions()
		{
			List<string> list = new List<string>();
			list.Add("record_audio");
			list.Add("camera");
			list.Add("write_external_storage");
			List<string> aPermnissions = list;
			if (MonoSingleton<NativeUtilitiesManager>.Instance.Native.HasPermissions(aPermnissions))
			{
				UserHasPermission();
			}
			else if (!MonoSingleton<NativeUtilitiesManager>.Instance.Native.AskForPermissions(aPermnissions, OnPermissionResult))
			{
				UserDoesntHavePermission();
			}
		}

		private void OnPermissionResult(NativeUtilitiesPermissionResult aResult)
		{
			if (aResult.HasPermission)
			{
				UserHasPermission();
			}
			else
			{
				UserDoesntHavePermission();
			}
		}

		private void UserHasPermission()
		{
			MonoSingleton<NativeCameraManager>.Instance.Native.ShowCameraOverlay(listener);
		}

		private void UserDoesntHavePermission()
		{
			GenericPanel genericPanel = (GenericPanel)Singleton<PanelManager>.Instance.ShowPanel(Panels.GENERIC);
			Singleton<PanelManager>.Instance.SetupTokens(new Dictionary<Text, string>
			{
				{ genericPanel.TitleText, null },
				{ genericPanel.MessageText, "customtokens.panels.enable_camera" },
				{ genericPanel.ButtonOneText, "customtokens.panels.button_ok" }
			});
		}
	}
}
