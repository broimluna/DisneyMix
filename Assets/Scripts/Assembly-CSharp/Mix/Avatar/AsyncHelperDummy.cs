using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mix.Avatar
{
	public static class AsyncHelperDummy
	{
		public delegate void callbackSnapshot(AvatarSnapshotResult arg);

		public static void Dummy()
		{
			Dictionary<int, Action<AvatarSnapshotResult>> dictionary = new Dictionary<int, Action<AvatarSnapshotResult>>();
			Debug.LogFormat("{0}", dictionary);
		}
	}
}
