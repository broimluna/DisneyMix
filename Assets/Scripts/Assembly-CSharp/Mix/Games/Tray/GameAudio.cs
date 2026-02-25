using System.Collections.Generic;
using UnityEngine;

namespace Mix.Games.Tray
{
	public abstract class GameAudio : MonoBehaviour
	{
		protected const string SFX_PREFIX = "SFX_";

		protected const string MUS_PREFIX = "MUS_";

		protected const string AUDIO_EXTENSION = ".wav";

		protected AudioSource mSFXSource;

		protected AudioSource mMusicSource;

		[SerializeField]
		protected List<AudioClip> Clips;

		protected Dictionary<string, AudioClip> mClipMap;

		protected virtual string GAME_PREFIX
		{
			get
			{
				return string.Empty;
			}
		}

		private void Awake()
		{
			AudioSource[] components = GetComponents<AudioSource>();
			if (components.Length < 2)
			{
				if (components.Length < 1)
				{
					mSFXSource = base.gameObject.AddComponent<AudioSource>();
				}
				else
				{
					mSFXSource = components[0];
				}
				mMusicSource = base.gameObject.AddComponent<AudioSource>();
			}
			else
			{
				mSFXSource = components[0];
				mMusicSource = components[1];
			}
			mClipMap = new Dictionary<string, AudioClip>();
		}

		public virtual void PlayMusic(string aKey)
		{
			if (mMusicSource != null)
			{
				string aClipName = GAME_PREFIX + "MUS_" + aKey;
				if (ConfirmAudioFile(aKey, aClipName))
				{
					mMusicSource.clip = mClipMap[aKey];
					mMusicSource.Play();
				}
			}
		}

		public virtual void PlaySound(string aKey)
		{
			if (mSFXSource != null)
			{
				string aClipName = GAME_PREFIX + "SFX_" + aKey;
				if (ConfirmAudioFile(aKey, aClipName))
				{
					mSFXSource.clip = mClipMap[aKey];
					mSFXSource.Play();
				}
			}
		}

		public virtual void StopSound()
		{
			if (mSFXSource != null)
			{
				mSFXSource.Stop();
				mSFXSource.clip = null;
			}
			if (mMusicSource != null)
			{
				mMusicSource.Stop();
			}
		}

		public virtual void PauseSound()
		{
			if (mSFXSource != null)
			{
				if (mSFXSource.isPlaying)
				{
					mSFXSource.Pause();
				}
				else
				{
					mSFXSource.UnPause();
				}
			}
			if (mMusicSource != null)
			{
				if (mMusicSource.isPlaying)
				{
					mMusicSource.Pause();
				}
				else
				{
					mMusicSource.UnPause();
				}
			}
		}

		protected virtual bool ConfirmAudioFile(string aKey, string aClipName)
		{
			int num = 0;
			if (!mClipMap.ContainsKey(aKey))
			{
				num = SearchAudioClip(aClipName);
				if (num != -1)
				{
					mClipMap.Add(aKey, Clips[num]);
					Clips.RemoveAt(num);
				}
			}
			if (num != -1)
			{
				return true;
			}
			return false;
		}

		protected virtual int SearchAudioClip(string aClipName)
		{
			for (int i = 0; i < Clips.Count; i++)
			{
				if (Clips[i] != null && Clips[i].name.CompareTo(aClipName) == 0)
				{
					return i;
				}
			}
			return -1;
		}
	}
}
