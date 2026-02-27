using UnityEngine;

namespace Disney.Native
{
	public class NativeAndroidAccessibility : NativeAccessibility
	{
		private AndroidJavaClass JavaClass;
		private bool isAvailable;

		public NativeAndroidAccessibility()
		{
			Initialize();
		}

		public void Initialize()
		{
			//JavaClass = new AndroidJavaClass("com.disney.nativeaccessibility.NativeAccessibility");
		}

		public override int GetAccessibilityLevel()
		{
			return 0;
		}

		public override bool IsSwitchControlEnabled()
		{
			return false;
		}

		public override bool IsVoiceOverEnabled()
		{
			return false;
		}

		public override void RemoveView(int aId)
		{
		}

		public override void RenderText(int aId, Rect aRect, string aLabel)
		{
		}

		public override void RenderButton(int aId, Rect aRect, string aLabel)
		{
		}

		public override void ClearAllElements()
		{
		}

		public override void UpdateView(int aId, Rect aRect, string aLabel)
		{
			if (aLabel == null)
			{
				aLabel = string.Empty;
			}
		}
	}
}
