using UnityEngine;

namespace Mix.Games.Tray.Runner
{
	public class BuildBackground : MonoBehaviour
	{
		public enum SwapDirection
		{
			UP = 0,
			DOWN = 1
		}

		public ParticleSystem dustParticles;

		public float maxParticleShiftForce = 2f;

		public void OnChunkSwap(SwapDirection direction)
		{
			if (direction == SwapDirection.UP)
			{
				PushAllParticles(Vector3.up * maxParticleShiftForce);
			}
			else
			{
				PushAllParticles(Vector3.down * maxParticleShiftForce);
			}
		}

		public void OnNextChunk()
		{
			PushAllParticles(Vector3.left * maxParticleShiftForce);
		}

		public void OnPreviousChunk()
		{
			PushAllParticles(Vector3.right * maxParticleShiftForce);
		}

		private void PushAllParticles(Vector3 force)
		{
			ParticleSystem.Particle[] array = new ParticleSystem.Particle[dustParticles.particleCount];
			int particles = dustParticles.GetParticles(array);
			for (int i = 0; i < particles; i++)
			{
				array[i].velocity += force;
			}
			dustParticles.SetParticles(array, particles);
		}
	}
}
