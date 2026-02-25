using System;
using UnityEngine;

namespace Disney.Native
{
	public class NativeVideoPlaybackRectUpdateEventArgs : EventArgs
	{
		public bool Visible;

		public Rect VideoRect;

		public NativeVideoPlaybackRectUpdateEventArgs(bool aVisible, Rect aRect)
		{
			Visible = aVisible;
			VideoRect = aRect;
		}
	}
}
