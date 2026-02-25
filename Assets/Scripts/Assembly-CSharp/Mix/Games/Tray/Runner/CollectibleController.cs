using UnityEngine;

namespace Mix.Games.Tray.Runner
{
	public abstract class CollectibleController : MonoBehaviour
	{
		protected const float MAGNET_SPEED = 30f;

		public bool isMagnetized;

		private GameObject main;

		protected MainRunnerGame mainController;

		protected GameObject player;

		public void Start()
		{
			main = GameObject.Find("main");
			if (main == null)
			{
				Debug.LogWarning("Could not find main object for Runner");
				return;
			}
			mainController = main.GetComponent<MainRunnerGame>();
			if (mainController == null)
			{
				Debug.LogWarning("Could not find main script for Runner");
			}
			else if (mainController.mode == GameMode.PLAY)
			{
				player = mainController.GetSpawnedPlayer();
				if (player == null)
				{
					Debug.LogWarning("Could not find player for Runner");
				}
			}
		}

		public abstract void SetMagnetized(bool state);

		public abstract void MoveIfMagnetized();

		public abstract void CollectionAction();
	}
}
