using System.Collections.Generic;

namespace Mix.Games.Tray.Fireworks
{
	public class GestureTracker
	{
		private const int MAX_GESTURE_POINTS = 256;

		private List<Gesture> mDrawnGestures = new List<Gesture>();

		public int AddNewGesture(Gesture gesture)
		{
			int result = 0;
			if (gesture != null)
			{
				int num = 0;
				foreach (Gesture mDrawnGesture in mDrawnGestures)
				{
					num += mDrawnGesture.GetPointCount();
				}
				if (num >= 256)
				{
					return 0;
				}
				if (num + gesture.GetPointCount() > 256)
				{
					int pointCount = 256 - num;
					gesture.TrimGesture(pointCount);
				}
				mDrawnGestures.Add(gesture);
				result = mDrawnGestures.Count - 1;
			}
			return result;
		}

		public Gesture GetGestureAtIndex(int index)
		{
			Gesture result = new Gesture();
			if (mDrawnGestures.Count > index && mDrawnGestures[index] != null)
			{
				result = mDrawnGestures[index];
			}
			return result;
		}

		public int GetNumberofGestures()
		{
			return mDrawnGestures.Count;
		}

		public void Purge()
		{
			mDrawnGestures.Clear();
		}
	}
}
