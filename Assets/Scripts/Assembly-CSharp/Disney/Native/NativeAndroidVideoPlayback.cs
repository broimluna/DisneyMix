using System;
using UnityEngine;

namespace Disney.Native
{
	public class NativeAndroidVideoPlayback : NativeVideoPlayback
	{
		private AndroidJavaClass JavaClass;
		private Action callback;
		private bool isAvailable;

		public NativeAndroidVideoPlayback()
		{
			Initialize();
		}

		public void Initialize()
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			try
			{
				JavaClass = new AndroidJavaClass("com.disney.nativevideoplayback.NativeVideoPlayback");
				isAvailable = JavaClass != null;
			}
			catch (Exception ex)
			{
				Debug.LogWarning("[NativeAndroidVideoPlayback] Plugin not found. Video playback disabled. " + ex.Message);
				isAvailable = false;
				JavaClass = null;
			}
#else
			isAvailable = false;
			JavaClass = null;
#endif
		}

		public void OnApplicationPause(bool aState)
		{
			if (!isAvailable || JavaClass == null) return;
			try { JavaClass.CallStatic("OnApplicationPause", aState); } catch {}
		}

		public override bool IsVideoPlaying()
		{
			if (!isAvailable || JavaClass == null) return false;
			try { return JavaClass.CallStatic<bool>("IsVideoPlaying", new object[0]); } catch { return false; }
		}

		public override bool IsFullScreen()
		{
			if (!isAvailable || JavaClass == null) return false;
			try { return JavaClass.CallStatic<bool>("IsFullScreen", new object[0]); } catch { return false; }
		}

		public override void Play(string aURL, Rect aRect, Action aCallback = null)
		{
			callback = aCallback;
			if (!isAvailable || JavaClass == null)
			{
				// Simulate immediate completion if video can't play
				if (callback != null)
				{
					callback();
					callback = null;
				}
				return;
			}

			try
			{
				JavaClass.CallStatic("PlayVideo", aURL, (int)aRect.x, (int)aRect.y, (int)aRect.width, (int)aRect.height);
			}
			catch (Exception ex)
			{
				Debug.LogWarning("[NativeAndroidVideoPlayback] Play failed: " + ex.Message);
				if (callback != null) { callback(); callback = null; }
			}
		}

		public override void Stop()
		{
			if (!isAvailable || JavaClass == null) return;
			try { JavaClass.CallStatic("StopVideo"); } catch {}
		}

		public override void Unload()
		{
			if (callback != null)
			{
				callback();
				callback = null;
			}
			if (!isAvailable || JavaClass == null) return;
			try { JavaClass.CallStatic("UnloadVideo"); } catch {}
		}

		public override void Pause(bool aState)
		{
			if (!isAvailable || JavaClass == null) return;
			try { JavaClass.CallStatic("PauseVideo", aState); } catch {}
		}

		public override void UpdateDragBounds(int aX, int aY, int aWidth, int aHeight)
		{
			if (!isAvailable || JavaClass == null) return;
			try { JavaClass.CallStatic("UpdateDragBounds", aX, aY, aWidth, aHeight); } catch {}
		}
	}
}
