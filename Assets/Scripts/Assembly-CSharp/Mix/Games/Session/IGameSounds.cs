using UnityEngine;

namespace Mix.Games.Session
{
	public interface IGameSounds
	{
		void PauseSoundEvent(string eventName, GameObject gameObject);

		void PlayMusic(string eventName, GameObject gameObject = null);

		void PlaySoundEvent(string eventName);

		void PlaySoundEvent(string eventName, GameObject gameObject);

		void PlaySoundEvent(string eventName, object parameter);

		void StopSoundEvent(string eventName);

		void StopSoundEvent(string eventName, GameObject gameObject);

		void UnpauseSoundEvent(string eventName, GameObject gameObject);

		void SetVolumeEvent(string eventName, float volume);

		void SetSwitchEvent(string eventName, string switchEventName, GameObject gameObject);
	}
}
