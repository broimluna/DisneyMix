namespace Mix.Games.Tray.Fireworks
{
	public class Toolbox
	{
		public static Toolbox Instance;

		public FireworkManager mFireworkManager = new FireworkManager();

		public SceneManager mSceneManager = new SceneManager();

		public PlaybackAssembler mPlaybackAssembler = new PlaybackAssembler();

		public GestureTracker mGestureTracker = new GestureTracker();

		public FireworksGame mFireworkGame;

		public void Initialize()
		{
			mSceneManager.Initialize();
		}

		public static void PurgeToolbox()
		{
			Instance.mFireworkManager.Purge();
			Instance.mGestureTracker.Purge();
		}
	}
}
