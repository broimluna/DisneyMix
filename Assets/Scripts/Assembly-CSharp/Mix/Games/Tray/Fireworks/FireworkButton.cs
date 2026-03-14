using UnityEngine;

namespace Mix.Games.Tray.Fireworks
{
	public class FireworkButton : MonoBehaviour
	{
		public int mFireworkIndex;

		public FireworksGame mFireworksGame;

		public void LaunchContainedFirework()
		{
			mFireworksGame.SaveFirework(mFireworkIndex);
		}
	}
}
