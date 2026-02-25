using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	public class SpawnParticleRotator : MonoBehaviour
	{
		public float RotationSpeed;

		private ParticleSystem particles;

		private void Start()
		{
			particles = GetComponent<ParticleSystem>();
			if (!particles)
			{
				base.enabled = false;
			}
		}

		private void Update()
		{
			base.transform.Rotate(Vector3.back, RotationSpeed * Time.deltaTime, Space.Self);
		}
	}
}
