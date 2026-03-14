using UnityEngine;
using System;

namespace Disney.Native
{
	public class NativeAndroidAccessibility : NativeAccessibility
	{
		private AndroidJavaClass JavaClass;

		public NativeAndroidAccessibility()
		{
			Initialize();
		}

		public void Initialize()
		{
			try
			{
				JavaClass = new AndroidJavaClass("com.disney.nativeaccessibility.NativeAccessibility");
			}
			catch (Exception ex)
			{
				Debug.LogWarning("NativeAndroidAccessibility missing: " + ex.Message);
				JavaClass = null;
			}
		}

		public override int GetAccessibilityLevel()
		{
			if (JavaClass == null) return 0;
			try { return JavaClass.CallStatic<int>("GetAccessibilityLevel", new object[0]); } catch { return 0; }
		}

		public override bool IsSwitchControlEnabled()
		{
			if (JavaClass == null) return false;
			try { return JavaClass.CallStatic<bool>("IsSwitchControlEnabled", new object[0]); } catch { return false; }
		}

		public override bool IsVoiceOverEnabled()
		{
			if (JavaClass == null) return false;
			try { return JavaClass.CallStatic<bool>("IsVoiceOverEnabled", new object[0]); } catch { return false; }
		}

		public override void RemoveView(int aId)
		{
			if (JavaClass != null) { try { JavaClass.CallStatic("RemoveView", aId); } catch { } }
		}

		public override void RenderText(int aId, Rect aRect, string aLabel)
		{
			if (JavaClass != null) { try { JavaClass.CallStatic("RenderText", aId, (int)aRect.x, (int)aRect.y, (int)aRect.width, (int)aRect.height, aLabel); } catch { } }
		}

		public override void RenderButton(int aId, Rect aRect, string aLabel)
		{
			if (JavaClass != null) { try { JavaClass.CallStatic("RenderButton", aId, (int)aRect.x, (int)aRect.y, (int)aRect.width, (int)aRect.height, aLabel); } catch { } }
		}

		public override void ClearAllElements()
		{
			if (JavaClass != null) { try { JavaClass.CallStatic("ClearAllElements"); } catch { } }
		}

		public override void UpdateView(int aId, Rect aRect, string aLabel)
		{
			if (aLabel == null)
			{
				aLabel = string.Empty;
			}
			if (JavaClass != null) { try { JavaClass.CallStatic("UpdateView", aId, (int)aRect.x, (int)aRect.y, (int)aRect.width, (int)aRect.height, aLabel); } catch { } }
		}
	}
}
