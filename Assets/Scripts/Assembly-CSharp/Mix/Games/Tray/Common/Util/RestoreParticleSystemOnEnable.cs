using UnityEngine;

namespace Mix.Games.Tray.Common.Util
{
	[RequireComponent(typeof(ParticleSystem))]
	public class RestoreParticleSystemOnEnable : MonoBehaviour
	{
		private bool isPlaying;

		private float particleTime;

		private ParticleSystem particles;

		private bool isQuitting;

		private void OnEnable()
		{
			if (particles == null)
			{
				particles = GetComponent<ParticleSystem>();
			}
			if (isPlaying)
			{
				particles.Simulate(particleTime, false, true);
				particles.Play();
			}
		}

		private void OnDisable()
		{
			if (!isQuitting)
			{
				isPlaying = particles.isPlaying;
				particleTime = particles.time;
			}
			else
			{
				isPlaying = false;
			}
		}

		private void OnApplicationQuit()
		{
			isQuitting = true;
		}
	}
}
