using System;
using UnityEngine;

namespace Disney.Native
{
	public class NativeVideoPlaybackDraggingEventArgs : EventArgs
	{
		public DragState State;

		public Vector2 Position;

		public NativeVideoPlaybackDraggingEventArgs(DragState aState, Vector2 aPosisiton)
		{
			State = aState;
			Position = aPosisiton;
		}
	}
}
