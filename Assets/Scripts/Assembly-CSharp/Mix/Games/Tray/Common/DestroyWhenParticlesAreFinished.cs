using UnityEngine;

namespace Mix.Games.Tray.Common
{
	public class DestroyWhenParticlesAreFinished : MonoBehaviour
	{
		public ParticleSystem particles;

		private void Awake()
		{
			if (particles == null)
			{
				particles = GetComponentInChildren<ParticleSystem>();
				if (particles == null)
				{
					Object.Destroy(this);
				}
			}
		}

		private void Update()
		{
			if (!particles.IsAlive(true))
			{
				Object.Destroy(base.gameObject);
			}
		}
	}
}
