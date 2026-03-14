using UnityEngine;

namespace Mix.Games.Tray.Fireworks
{
	public class FireworkFanBurst : Firework
	{
		public float setHeight;

		public override bool Launch(Vector3 launchVector)
		{
			launchVector = new Vector3(launchVector.x, setHeight, launchVector.z);
			return base.Launch(launchVector);
		}

		public override void Return()
		{
			base.Return();
		}
	}
}
