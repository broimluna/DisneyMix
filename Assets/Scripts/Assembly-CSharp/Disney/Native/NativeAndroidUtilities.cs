using System;
using System.Collections.Generic;
using System.Text;
using Mix;
using Mix.Ui;
using UnityEngine;
using UnityEngine.UI;

namespace Disney.Native
{
	public class NativeAndroidUtilities : NativeUtilities
	{
		private AndroidJavaClass JavaClass;

		private Action<NativeUtilitiesPermissionResult> permissionCallback;

		public NativeAndroidUtilities()
		{
			Initialize();
		}

		public void Initialize()
		{
			try
			{
				JavaClass = new AndroidJavaClass("com.disney.nativeutilities.NativeUtilities");
			}
			catch (Exception ex)
			{
				Debug.LogWarning("NativeAndroidUtilities plugin missing: " + ex.Message);
				JavaClass = null;
			}
		}

		public override void GotoApplicationSettings()
		{
			if (JavaClass != null) { try { JavaClass.CallStatic("GotoApplicationSettings"); } catch { } }
		}

		public override void UnGZipFile(string aSourcePath, string aDestPath)
		{
			if (JavaClass != null) { try { JavaClass.CallStatic("UnGZipFile", Encoding.UTF32.GetBytes(aSourcePath), Encoding.UTF32.GetBytes(aDestPath)); } catch { } }
		}

		public override bool HasPermissions(List<string> aPermissions)
		{
			if (JavaClass == null) return true; // Assume true if native check is missing
			
			List<string> list = ConvertPermissions(aPermissions);
			try
			{
				return list != null && JavaClass.CallStatic<bool>("CheckPermissions", new object[1] { list.ToArray() });
			}
			catch
			{
				return true;
			}
		}

		public override bool AskForPermissions(List<string> aPermissions, Action<NativeUtilitiesPermissionResult> aCallback)
		{
			if (JavaClass == null)
			{
				// Simulate granted permissions immediately if native plugin is missing
				if (aCallback != null) aCallback(new NativeUtilitiesPermissionResult(true));
				return true;
			}

			List<string> permissions = ConvertPermissions(aPermissions);
			permissionCallback = null;
			if (permissions == null)
			{
				return false;
			}
			
			bool askResult = false;
			try
			{
				askResult = JavaClass.CallStatic<bool>("AskPermissions", new object[2] { permissions.ToArray(), false });
			}
			catch { }

			if (askResult)
			{
				permissionCallback = aCallback;
				GenericPanel genericPanel = (GenericPanel)Singleton<PanelManager>.Instance.ShowPanel(Panels.GENERIC);
				Singleton<PanelManager>.Instance.SetupTokens(new Dictionary<Text, string>
				{
					{ genericPanel.TitleText, "customtokens.permissions.permission_title" },
					{
						genericPanel.MessageText,
						GetPermissionEducationalMessages(aPermissions)[0]
					},
					{ genericPanel.ButtonOneText, "customtokens.panels.button_ok" },
					{ genericPanel.ButtonTwoText, null }
				});
				Singleton<PanelManager>.Instance.SetupButtons(new Dictionary<Button, Action>
				{
					{ genericPanel.ButtonOne, genericPanel.ClosePanel },
					{ genericPanel.ButtonTwo, null }
				});
				genericPanel.PanelClosedEvent += delegate
				{
					if (JavaClass != null)
					{
						try { JavaClass.CallStatic<bool>("AskPermissions", new object[2] { permissions.ToArray(), true }); } catch { }
					}
				};
				return true;
			}
			return false;
		}

		public void OnNativePermissionResponse(string aResult)
		{
			if (permissionCallback != null)
			{
				bool aHasPermission = aResult.Equals("true");
				permissionCallback(new NativeUtilitiesPermissionResult(aHasPermission));
				permissionCallback = null;
			}
		}

		private List<string> ConvertPermissions(List<string> aPermissions)
		{
			List<string> list = new List<string>();
			foreach (string aPermission in aPermissions)
			{
				switch (aPermission)
				{
				case "write_external_storage":
					list.Add("android.permission.WRITE_EXTERNAL_STORAGE");
					break;
				case "camera":
					list.Add("android.permission.CAMERA");
					break;
				case "record_audio":
					list.Add("android.permission.RECORD_AUDIO");
					break;
				}
			}
			return list;
		}

		private List<string> GetPermissionEducationalMessages(List<string> aPermissions)
		{
			List<string> list = new List<string>();
			foreach (string aPermission in aPermissions)
			{
				switch (aPermission)
				{
				case "write_external_storage":
					list.Add("customtokens.permissions.prompt_media");
					break;
				case "camera":
					list.Add("customtokens.permissions.prompt_camera");
					break;
				case "record_audio":
					list.Add("customtokens.permissions.prompt_record_audio");
					break;
				}
			}
			return list;
		}
	}
}
