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
			//JavaClass = new AndroidJavaClass("com.disney.nativeutilities.NativeUtilities");
		}

		public override void GotoApplicationSettings()
		{
			//JavaClass.CallStatic("GotoApplicationSettings");
		}

		public override void UnGZipFile(string aSourcePath, string aDestPath)
		{
			//JavaClass.CallStatic("UnGZipFile", Encoding.UTF32.GetBytes(aSourcePath), Encoding.UTF32.GetBytes(aDestPath));
		}

		public override bool HasPermissions(List<string> aPermissions)
		{
			List<string> list = ConvertPermissions(aPermissions);
			return list != null;
		}

		public override bool AskForPermissions(List<string> aPermissions, Action<NativeUtilitiesPermissionResult> aCallback)
		{
			List<string> permissions = ConvertPermissions(aPermissions);
			permissionCallback = null;
			if (permissions == null)
			{
				return false;
			}

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

				};
				return true;
			}
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
			using (List<string>.Enumerator enumerator = aPermissions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					switch (enumerator.Current)
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
		}

		private List<string> GetPermissionEducationalMessages(List<string> aPermissions)
		{
			List<string> list = new List<string>();
			using (List<string>.Enumerator enumerator = aPermissions.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					switch (enumerator.Current)
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
}
