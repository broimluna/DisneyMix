using UnityEngine;

namespace Mix.Games.Tray.WouldYouRather
{
	public class SelectedEffect : MonoBehaviour
	{
		public ParticleSystem[] glowParticles;

		private bool isActive;

		public void OnEnable()
		{
			if (isActive)
			{
				for (int i = 0; i < glowParticles.Length; i++)
				{
					glowParticles[i].Play();
				}
			}
		}

		public void Activate()
		{
			if (!isActive)
			{
				for (int i = 0; i < glowParticles.Length; i++)
				{
					glowParticles[i].Play();
				}
				isActive = true;
			}
		}

		public void Deactivate()
		{
			if (isActive)
			{
				for (int i = 0; i < glowParticles.Length; i++)
				{
					glowParticles[i].Stop();
				}
				isActive = false;
			}
		}
	}
}
