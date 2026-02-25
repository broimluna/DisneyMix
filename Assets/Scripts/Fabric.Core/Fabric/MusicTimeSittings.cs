using System;
using UnityEngine;

namespace Fabric
{
	[Serializable]
	public class MusicTimeSittings
	{
		public delegate void OnBarHandler(double timeOffset);

		public delegate void OnBeatHandler(double timeOffset);

		[SerializeField]
		public string _name = "";

		[SerializeField]
		public float _bpm = 120f;

		[SerializeField]
		public int _timeSignatureLower = 4;

		[SerializeField]
		public int _timeSignatureUpper = 4;

		[SerializeField]
		public MusicSyncType _syncType = MusicSyncType.OnBar;

		[NonSerialized]
		public double _lookAheadTime = 1.0;

		[NonSerialized]
		public double _nextBeatTimeEvent;

		[NonSerialized]
		public double _nextBarTimeEvent;

		[NonSerialized]
		private float _notesPerSecond;

		[NonSerialized]
		private float _barsPerSecond;

		protected OnBeatHandler OnBeatEventInvoker;

		protected OnBarHandler OnBarEventInvoker;

		public event OnBeatHandler _onBeatDetected
		{
			add
			{
				OnBeatEventInvoker = (OnBeatHandler)Delegate.Combine(OnBeatEventInvoker, value);
			}
			remove
			{
				OnBeatEventInvoker = (OnBeatHandler)Delegate.Remove(OnBeatEventInvoker, value);
			}
		}

		public event OnBarHandler _onBarDetected
		{
			add
			{
				OnBarEventInvoker = (OnBarHandler)Delegate.Combine(OnBarEventInvoker, value);
			}
			remove
			{
				OnBarEventInvoker = (OnBarHandler)Delegate.Remove(OnBarEventInvoker, value);
			}
		}

		private void OnBeatDetected(double time)
		{
			if (OnBeatEventInvoker != null)
			{
				OnBeatEventInvoker(time);
			}
		}

		private void OnBarDetected(double time)
		{
			if (OnBarEventInvoker != null)
			{
				OnBarEventInvoker(time);
			}
		}

		public void Init()
		{
			_notesPerSecond = 60f / _bpm * 4f / (float)_timeSignatureLower;
			_barsPerSecond = _notesPerSecond * (float)_timeSignatureUpper;
			if (_lookAheadTime > (double)_notesPerSecond)
			{
				_lookAheadTime = _notesPerSecond;
			}
			_nextBeatTimeEvent = AudioSettings.dspTime + (double)_notesPerSecond;
			_nextBarTimeEvent = AudioSettings.dspTime + (double)_barsPerSecond;
		}

		public void Update(bool activateCallbacks = true)
		{
			double offset = 0.0;
			bool flag = false;
			if (CheckIfNextEventIsWithinRange(MusicSyncType.OnBeat, ref offset))
			{
				flag = true;
			}
			double offset2 = 0.0;
			bool flag2 = false;
			if (CheckIfNextEventIsWithinRange(MusicSyncType.OnBar, ref offset2))
			{
				flag2 = true;
			}
			if (flag && activateCallbacks)
			{
				OnBeatDetected(offset);
			}
			if (flag2 && activateCallbacks)
			{
				OnBarDetected(offset2);
			}
		}

		public double GetDelay(int numBeats = 0)
		{
			double num = 0.0;
			if (_syncType == MusicSyncType.OnBeat || _syncType == MusicSyncType.OnEnd)
			{
				num = _nextBeatTimeEvent - AudioSettings.dspTime;
			}
			if (_syncType == MusicSyncType.OnBar)
			{
				num = _nextBarTimeEvent - AudioSettings.dspTime;
			}
			if (numBeats > 0)
			{
				num += (double)((float)numBeats * _notesPerSecond);
			}
			return num;
		}

		public bool CheckIfNextEventIsWithinRange(ref double offset)
		{
			return CheckIfNextEventIsWithinRange(_syncType, ref offset);
		}

		public bool CheckIfNextEventIsWithinRange(MusicSyncType syncType, ref double offset)
		{
			double num = AudioSettings.dspTime + _lookAheadTime;
			if (syncType == MusicSyncType.OnBeat && num > _nextBeatTimeEvent)
			{
				offset = _nextBeatTimeEvent - AudioSettings.dspTime;
				_nextBeatTimeEvent += _notesPerSecond;
				return true;
			}
			if (syncType == MusicSyncType.OnBar && num > _nextBarTimeEvent)
			{
				offset = _nextBarTimeEvent - AudioSettings.dspTime;
				_nextBarTimeEvent += _barsPerSecond;
				return true;
			}
			offset = 0.0;
			return false;
		}
	}
}
