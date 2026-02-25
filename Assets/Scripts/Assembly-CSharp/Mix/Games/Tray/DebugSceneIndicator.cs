using Mix.Games.Session;
using UnityEngine;

namespace Mix.Games.Tray
{
	public abstract class DebugSceneIndicator : MonoBehaviour
	{
		public delegate IGameSession GameManagerRetriever();

		private static bool mIsDebugScene;

		public static bool IsDebugScene
		{
			get
			{
				return UnityEngine.Application.isEditor && mIsDebugScene;
			}
		}

		public static bool IsMainScene
		{
			get
			{
				return !IsDebugScene;
			}
		}

		public static GameManagerRetriever GetGameManager { get; set; }

		private void Awake()
		{
			mIsDebugScene = true;
		}
	}
}
