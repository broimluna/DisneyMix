using System;
using Mix;
using UnityEngine;

namespace Disney.Native
{
	public class NativeVideoPlayback : MonoBehaviour
	{
		private ScreenOrientation defaultOrientation;

		public event EventHandler<NativeVideoPlaybackDraggingEventArgs> OnVideoDragging = delegate
		{
		};

		public event EventHandler<NativeVideoPlaybackRectUpdateEventArgs> OnVideoRectUpdated = delegate
		{
		};

		public event EventHandler<NativeVideoPlaybackFullScreenToggledEventArgs> OnFullScreenToggled = delegate
		{
		};

		protected void Awake()
		{
			defaultOrientation = Screen.orientation;
		}

		public virtual void Play(string aURL, Rect aRect, Action aCallback = null)
		{
		}

		public virtual void Stop()
		{
		}

		public virtual void Unload()
		{
		}

		public virtual void Pause(bool aState)
		{
		}

		public virtual bool IsFullScreen()
		{
			return false;
		}

		public virtual bool IsVideoPlaying()
		{
			return false;
		}

		public virtual void UpdateDragBounds(int aX, int aY, int aWidth, int aHeight)
		{
		}

		public virtual void FullScreenToggled(string aData)
		{
			this.OnFullScreenToggled(this, new NativeVideoPlaybackFullScreenToggledEventArgs(bool.Parse(aData)));
		}

		public virtual void VideoRectUpdated(string aData)
		{
			bool aVisible = false;
			Rect aRect = ConvertRect(aData, out aVisible);
			this.OnVideoRectUpdated(this, new NativeVideoPlaybackRectUpdateEventArgs(aVisible, aRect));
		}

		public virtual void VideoDragStart(string aPosition)
		{
			this.OnVideoDragging(this, new NativeVideoPlaybackDraggingEventArgs(DragState.Start, ConvertPosition(aPosition)));
		}

		public virtual void VideoDragging(string aPosition)
		{
			this.OnVideoDragging(this, new NativeVideoPlaybackDraggingEventArgs(DragState.Dragging, ConvertPosition(aPosition)));
		}

		public virtual void VideoDragEnd(string aPosition)
		{
			this.OnVideoDragging(this, new NativeVideoPlaybackDraggingEventArgs(DragState.End, ConvertPosition(aPosition)));
		}

		public virtual void AllowLandscape(string aNotUsed)
		{
			Screen.autorotateToPortrait = true;
			Screen.autorotateToPortraitUpsideDown = true;
			Screen.autorotateToLandscapeLeft = true;
			Screen.autorotateToLandscapeRight = true;
			Screen.orientation = ScreenOrientation.AutoRotation;
		}

		public virtual void Reset(string aNotUsed)
		{
			Screen.orientation = defaultOrientation;
		}

		private Vector2 ConvertPosition(string aData)
		{
			string[] array = aData.Split('|');
			return new Vector2(float.Parse(array[0]), (float)Singleton<SettingsManager>.Instance.GetScreenHeight() - float.Parse(array[1]));
		}

		private Rect ConvertRect(string aData, out bool aVisible)
		{
			string[] array = aData.Split('|');
			aVisible = bool.Parse(array[0]);
			return new Rect(float.Parse(array[1]), float.Parse(array[2]), float.Parse(array[3]), float.Parse(array[4]));
		}
	}
}
