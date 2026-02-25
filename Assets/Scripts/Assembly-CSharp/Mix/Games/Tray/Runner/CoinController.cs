using UnityEngine;

namespace Mix.Games.Tray.Runner
{
	public class CoinController : CollectibleController
	{
		public int value = 1;

		public void Update()
		{
			MoveIfMagnetized();
		}

		public override void SetMagnetized(bool state)
		{
			isMagnetized = state;
		}

		public override void MoveIfMagnetized()
		{
			if (isMagnetized)
			{
				base.transform.position = Vector3.MoveTowards(base.transform.position, player.transform.position, 30f * Time.deltaTime);
			}
		}

		public override void CollectionAction()
		{
			mainController.AddScore(value);
			Object.Destroy(base.gameObject);
		}
	}
}
