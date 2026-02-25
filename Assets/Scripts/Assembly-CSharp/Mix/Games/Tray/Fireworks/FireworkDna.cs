using UnityEngine;

namespace Mix.Games.Tray.Fireworks
{
	public class FireworkDna : Firework
	{
		public override bool Launch(Vector3 launch)
		{
			bool result = base.Launch(launch);
			foreach (ParticleSystem trail in Trails)
			{
				trail.transform.localPosition = new Vector3(0f, 0f, 0f);
				trail.GetComponent<DnaBurst>().StartOff();
			}
			return result;
		}

		public override void Explode()
		{
			base.Explode();
		}

		public override void Return()
		{
			base.Return();
			foreach (ParticleSystem trail in Trails)
			{
				trail.GetComponent<DnaBurst>().Return();
			}
		}
	}
}
