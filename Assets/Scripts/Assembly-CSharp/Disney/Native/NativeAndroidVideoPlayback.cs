using System;
using UnityEngine;

namespace Disney.Native
{
	public class NativeAndroidVideoPlayback : NativeVideoPlayback
	{
		private AndroidJavaClass JavaClass;

		private Action callback;

		public NativeAndroidVideoPlayback()
		{
			Initialize();
		}

		public void OnApplicationPause(bool aState)
		{
			if (JavaClass != null)
			{
				JavaClass.CallStatic("OnApplicationPause", aState);
			}
		}

		public void Initialize()
		{
			JavaClass = new AndroidJavaClass("com.disney.nativevideoplayback.NativeVideoPlayback");
		}

		public override bool IsVideoPlaying()
		{
			return JavaClass.CallStatic<bool>("IsVideoPlaying", new object[0]);
		}

		public override bool IsFullScreen()
		{
			return JavaClass.CallStatic<bool>("IsFullScreen", new object[0]);
		}

		public override void Play(string aURL, Rect aRect, Action aCallback = null)
		{
			callback = aCallback;
			JavaClass.CallStatic("PlayVideo", aURL, (int)aRect.x, (int)aRect.y, (int)aRect.width, (int)aRect.height);
		}

		public override void Stop()
		{
			JavaClass.CallStatic("StopVideo");
		}

		public override void Unload()
		{
			if (callback != null)
			{
				callback();
				callback = null;
			}
			JavaClass.CallStatic("UnloadVideo");
		}

		public override void Pause(bool aState)
		{
			JavaClass.CallStatic("PauseVideo", aState);
		}

		public override void UpdateDragBounds(int aX, int aY, int aWidth, int aHeight)
		{
			JavaClass.CallStatic("UpdateDragBounds", aX, aY, aWidth, aHeight);
		}
	}
}
