using UnityEngine;

namespace Disney.Native
{
	public class NativeAndroidCamera : NativeCamera
	{
		private AndroidJavaClass JavaClass;

		public NativeAndroidCamera()
		{
			JavaClass = new AndroidJavaClass("com.disney.nativecamera.NativeCamera");
		}

		public override void ShowCameraOverlay(INativeCamera aListener)
		{
			base.ShowCameraOverlay(aListener);
			JavaClass.CallStatic("ShowCameraOverlay");
		}

		public override void CloseOverlay()
		{
			JavaClass.CallStatic("CloseOverlay");
		}

		public override bool OnAndroidBackButton()
		{
			return JavaClass.CallStatic<bool>("OnAndroidBackButton", new object[0]);
		}

		public override void OnApplicationPaused()
		{
			JavaClass.CallStatic("OnApplicationPaused");
		}

		public override void OnApplicationResumed()
		{
			JavaClass.CallStatic("OnApplicationResumed");
		}
	}
}
