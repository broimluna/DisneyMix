using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Fabric
{
	[AddComponentMenu("Fabric/Mixing/AudioMixer")]
	public class AudioMixer : MonoBehaviour, IEventListener
	{
		public UnityEngine.Audio.AudioMixer _audioMixer;

		bool IEventListener.IsDestroyed
		{
			get
			{
				return this == null;
			}
		}

		private void Start()
		{
			EventManager.Instance.RegisterListener(this, "AudioMixer");
		}

		EventStatus IEventListener.Process(Event zEvent)
		{
			if (_audioMixer == null)
			{
				return EventStatus.Not_Handled;
			}
			EventAction eventAction = zEvent.EventAction;
			if (eventAction == EventAction.TransitionToSnapshot)
			{
				TransitionToSnapshotData transitionToSnapshotData = (TransitionToSnapshotData)zEvent._parameter;
				if (transitionToSnapshotData != null)
				{
					AudioMixerSnapshot audioMixerSnapshot = _audioMixer.FindSnapshot(transitionToSnapshotData._snapshot);
					if (audioMixerSnapshot != null)
					{
						audioMixerSnapshot.TransitionTo(transitionToSnapshotData._timeToReach);
					}
				}
			}
			return EventStatus.Handled;
		}

		bool IEventListener.IsActive(GameObject parentGameObject)
		{
			return false;
		}

		bool IEventListener.GetEventListeners(string eventName, List<EventListener> listeners)
		{
			return false;
		}

		bool IEventListener.GetEventListeners(int eventID, List<EventListener> listeners)
		{
			return false;
		}

		bool IEventListener.GetEventInfo(GameObject parentGameObject, ref EventInfo eventInfo)
		{
			return false;
		}
	}
}
