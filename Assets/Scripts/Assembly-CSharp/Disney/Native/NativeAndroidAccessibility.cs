using UnityEngine;

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
			JavaClass = new AndroidJavaClass("com.disney.nativeaccessibility.NativeAccessibility");
		}

		public override int GetAccessibilityLevel()
		{
			return JavaClass.CallStatic<int>("GetAccessibilityLevel", new object[0]);
		}

		public override bool IsSwitchControlEnabled()
		{
			return JavaClass.CallStatic<bool>("IsSwitchControlEnabled", new object[0]);
		}

		public override bool IsVoiceOverEnabled()
		{
			return JavaClass.CallStatic<bool>("IsVoiceOverEnabled", new object[0]);
		}

		public override void RemoveView(int aId)
		{
			JavaClass.CallStatic("RemoveView", aId);
		}

		public override void RenderText(int aId, Rect aRect, string aLabel)
		{
			JavaClass.CallStatic("RenderText", aId, (int)aRect.x, (int)aRect.y, (int)aRect.width, (int)aRect.height, aLabel);
		}

		public override void RenderButton(int aId, Rect aRect, string aLabel)
		{
			JavaClass.CallStatic("RenderButton", aId, (int)aRect.x, (int)aRect.y, (int)aRect.width, (int)aRect.height, aLabel);
		}

		public override void ClearAllElements()
		{
			JavaClass.CallStatic("ClearAllElements");
		}

		public override void UpdateView(int aId, Rect aRect, string aLabel)
		{
			if (aLabel == null)
			{
				aLabel = string.Empty;
			}
			JavaClass.CallStatic("UpdateView", aId, (int)aRect.x, (int)aRect.y, (int)aRect.width, (int)aRect.height, aLabel);
		}
	}
}
