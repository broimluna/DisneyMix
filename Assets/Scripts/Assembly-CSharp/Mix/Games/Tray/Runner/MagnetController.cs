using UnityEngine;

namespace Mix.Games.Tray.Runner
{
	public class MagnetController : CollectibleController
	{
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
			mainController.SetPowerup(Powerup.MAGNET);
			Object.Destroy(base.gameObject);
		}
	}
}
