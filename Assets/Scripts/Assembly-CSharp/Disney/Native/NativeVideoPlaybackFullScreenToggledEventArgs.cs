using System;

namespace Disney.Native
{
	public class NativeVideoPlaybackFullScreenToggledEventArgs : EventArgs
	{
		public bool IsFullScreen;

		public NativeVideoPlaybackFullScreenToggledEventArgs(bool aIsFullScreen)
		{
			IsFullScreen = aIsFullScreen;
		}
	}
}
