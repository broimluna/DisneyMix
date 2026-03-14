using UnityEngine;

namespace Mix.Games.Tray.Fireworks
{
	public class FireworkGesture : MonoBehaviour
	{
		public string SoundEvent;

		private ParticleSystem mParticleSystem;

		private void Start()
		{
			mParticleSystem = GetComponent<ParticleSystem>();
		}

		public void Play()
		{
			mParticleSystem.Play(true);
			BaseGameController.Instance.Session.SessionSounds.PlaySoundEvent(SoundEvent, this);
		}

		public void Stop()
		{
			mParticleSystem.Stop(true);
			BaseGameController.Instance.Session.SessionSounds.StopSoundEvent(SoundEvent);
		}
	}
}
