using System.Collections.Generic;
using UnityEngine;

namespace Mix.Games.Tray.Fireworks
{
	public struct ShowData
	{
		public Dictionary<float, KeyValuePair<int, Vector3>> mLaunchedFireworks;

		public List<Gesture> mGestures;

		public Scene mSelectedScene;

		public string mSelectedSong;

		public string mMessage;
	}
}
