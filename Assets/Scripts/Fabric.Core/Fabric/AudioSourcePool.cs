using System.Collections.Generic;
using UnityEngine;

namespace Fabric
{
	public class AudioSourcePool : MonoBehaviour
	{
		public class AudioVoice : MonoBehaviour
		{
			private enum AudioVoiceState
			{
				Stopped = 0,
				Playing = 1,
				Stopping = 2,
				Paused = 3
			}

			public AudioSource _audioSource;

			public Component _component;

			private IAudioSourcePoolListener _listener;

			private InterpolatedParameter _fadeParameter = new InterpolatedParameter(0f);

			private AudioVoiceState _state;

			private float _volume;

			private bool _callStop;

			public void Init()
			{
				_audioSource = base.gameObject.AddComponent<AudioSource>();
				_state = AudioVoiceState.Stopped;
			}

			public bool IsPlaying()
			{
				if (_state == AudioVoiceState.Stopped)
				{
					return false;
				}
				return true;
			}

			public void Play(Component component, float fadeInTime)
			{
				_fadeParameter.SetTarget(FabricTimer.Get(), 1f, fadeInTime, 0.5f);
				_audioSource.volume = 0f;
				_component = component;
				_listener = component as IAudioSourcePoolListener;
				_state = AudioVoiceState.Playing;
			}

			public void Stop(float fadeOutTime, bool callStop)
			{
				if (fadeOutTime > 0f)
				{
					_fadeParameter.SetTarget(FabricTimer.Get(), 0f, fadeOutTime, 0.5f);
					_volume = _audioSource.volume;
					_callStop = callStop;
					_state = AudioVoiceState.Stopping;
				}
				else
				{
					StopAudioVoice();
				}
			}

			private void StopAudioVoice()
			{
				if (_callStop)
				{
					_audioSource.Stop();
				}
				_component = null;
				_audioSource.clip = null;
				_state = AudioVoiceState.Stopped;
			}

			public void UpdateInternal()
			{
				_audioSource.volume = _volume * _fadeParameter.Get(FabricTimer.Get());
				if (_state == AudioVoiceState.Stopping && _fadeParameter.HasReachedTarget())
				{
					StopAudioVoice();
				}
			}
		}

		public interface IAudioSourcePoolListener
		{
		}

		private Queue<AudioVoice> _audioVoicePool;

		private List<AudioVoice> _allocatedList;

		private List<AudioVoice> _freeingVoicesList;

		private static GameObject _container;

		private float _fadeInTime;

		private float _fadeOutTime = 0.5f;

		private static AudioSourcePool _instance = null;

		public static AudioSourcePool Instance
		{
			get
			{
				if (_instance == null)
				{
					_container = new GameObject("AudioSourcePool");
					_container.transform.parent = FabricManager.Instance.gameObject.transform;
					_instance = _container.AddComponent<AudioSourcePool>();
				}
				return _instance;
			}
		}

		public static bool IsInitialised()
		{
			if (!(_instance != null))
			{
				return false;
			}
			return true;
		}

		public int Size()
		{
			if (_audioVoicePool == null)
			{
				return 0;
			}
			return _audioVoicePool.Count;
		}

		public void Initialise(int count, float fadeInTime, float fadeOutTime)
		{
			if (count == 0)
			{
				return;
			}
			_audioVoicePool = new Queue<AudioVoice>();
			_allocatedList = new List<AudioVoice>(count);
			_freeingVoicesList = new List<AudioVoice>(count);
			_fadeInTime = fadeInTime;
			_fadeOutTime = fadeOutTime;
			for (int i = 0; i < count; i++)
			{
				GameObject gameObject = new GameObject();
				gameObject.name = "AudioVoice_" + i;
				AudioVoice audioVoice = gameObject.AddComponent<AudioVoice>();
				if (audioVoice == null)
				{
					DebugLog.Print("Failed to allocate audio source in the pool!", DebugLevel.Error);
					break;
				}
				audioVoice.Init();
				audioVoice.transform.parent = _container.transform;
				Generic.SetGameObjectActive(audioVoice.gameObject, false);
				_audioVoicePool.Enqueue(audioVoice);
			}
		}

		public AudioSource Alloc(Component component)
		{
			if (_audioVoicePool == null || _audioVoicePool.Count == 0)
			{
				return null;
			}
			AudioVoice audioVoice = _audioVoicePool.Dequeue();
			if (audioVoice == null)
			{
				return null;
			}
			_allocatedList.Add(audioVoice);
			Generic.SetGameObjectActive(audioVoice.gameObject, true);
			audioVoice.Play(component, _fadeInTime);
			return audioVoice._audioSource;
		}

		public void Free(AudioSource audioSource, bool callStop = false)
		{
			if (audioSource != null)
			{
				AudioVoice audioVoice = FindAudioVoiceFromSource(audioSource);
				if (audioVoice != null && _allocatedList.Remove(audioVoice))
				{
					audioVoice.Stop(_fadeOutTime, callStop);
					_freeingVoicesList.Add(audioVoice);
				}
			}
		}

		public void Update()
		{
			if (_freeingVoicesList == null)
			{
				return;
			}
			for (int i = 0; i < _freeingVoicesList.Count; i++)
			{
				AudioVoice audioVoice = _freeingVoicesList[i];
				audioVoice.UpdateInternal();
				if (!audioVoice.IsPlaying())
				{
					Generic.SetGameObjectActive(audioVoice.gameObject, false);
					_freeingVoicesList.Remove(audioVoice);
					_audioVoicePool.Enqueue(audioVoice);
				}
			}
		}

		public void FreeAll()
		{
			AudioVoice[] array = _allocatedList.ToArray();
			AudioVoice[] array2 = array;
			foreach (AudioVoice audioVoice in array2)
			{
				Free(audioVoice._audioSource);
			}
		}

		public void GetInfo(ref int numAllocatedVoices, ref int numToBeRemovedVoices)
		{
			if (_allocatedList != null)
			{
				numAllocatedVoices = _allocatedList.Count;
			}
			if (_freeingVoicesList != null)
			{
				numToBeRemovedVoices = _freeingVoicesList.Count;
			}
		}

		public AudioVoice[] GetAllocatedAudioVoices()
		{
			return _allocatedList.ToArray();
		}

		public void SetFadeInTime(float fadeInTime)
		{
			_fadeInTime = fadeInTime;
		}

		public void SetFadeOutTime(float fadeOutTime)
		{
			_fadeOutTime = fadeOutTime;
		}

		private AudioVoice FindAudioVoiceFromSource(AudioSource audioSource)
		{
			for (int i = 0; i < _allocatedList.Count; i++)
			{
				if (_allocatedList[i]._audioSource == audioSource)
				{
					return _allocatedList[i];
				}
			}
			return null;
		}

		private AudioSource FindAudioSource(AudioVoice audioVoice)
		{
			int num = _allocatedList.IndexOf(audioVoice);
			if (num >= 0)
			{
				return _allocatedList[num]._audioSource;
			}
			return null;
		}
	}
}
