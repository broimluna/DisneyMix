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
                try { JavaClass.CallStatic("OnApplicationPause", aState); } catch { }
            }
        }

        public void Initialize()
        {
            try
            {
                JavaClass = new AndroidJavaClass("com.disney.nativevideoplayback.NativeVideoPlayback");
            }
            catch (Exception ex)
            {
                Debug.LogWarning("NativeVideoPlayback plugin missing: " + ex.Message);
                JavaClass = null;
            }
        }

        public override bool IsVideoPlaying()
        {
            if (JavaClass == null) return false;
            try { return JavaClass.CallStatic<bool>("IsVideoPlaying", new object[0]); } catch { return false; }
        }

        public override bool IsFullScreen()
        {
            if (JavaClass == null) return false;
            try { return JavaClass.CallStatic<bool>("IsFullScreen", new object[0]); } catch { return false; }
        }

        public override void Play(string aURL, Rect aRect, Action aCallback = null)
        {
            callback = aCallback;
            if (JavaClass != null)
            {
                try { JavaClass.CallStatic("PlayVideo", aURL, (int)aRect.x, (int)aRect.y, (int)aRect.width, (int)aRect.height); } catch { }
            }
            else
            {
                // Simulate immediate finish if plugin is missing to prevent UI lockup
                if (callback != null)
                {
                    callback();
                    callback = null;
                }
            }
        }

        public override void Stop()
        {
            if (JavaClass != null) { try { JavaClass.CallStatic("StopVideo"); } catch { } }
        }

        public override void Unload()
        {
            if (callback != null)
            {
                callback();
                callback = null;
            }
            if (JavaClass != null) { try { JavaClass.CallStatic("UnloadVideo"); } catch { } }
        }

        public override void Pause(bool aState)
        {
            if (JavaClass != null) { try { JavaClass.CallStatic("PauseVideo", aState); } catch { } }
        }

        public override void UpdateDragBounds(int aX, int aY, int aWidth, int aHeight)
        {
            if (JavaClass != null) { try { JavaClass.CallStatic("UpdateDragBounds", aX, aY, aWidth, aHeight); } catch { } }
        }
    }
}