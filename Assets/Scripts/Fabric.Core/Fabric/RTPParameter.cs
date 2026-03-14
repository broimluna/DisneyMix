using System;
using Fabric.TimelineComponent;
using UnityEngine;

namespace Fabric
{
	[Serializable]
	public class RTPParameter
	{
		private static float MAX_NORMALISED_RANGE = 1f;

		private static float MIN_NORMALISED_RANGE = 0f;

		[SerializeField]
		[HideInInspector]
		public string Name = "";

		[SerializeField]
		public float _value;

		[SerializeField]
		[HideInInspector]
		public float _min;

		[SerializeField]
		[HideInInspector]
		public float _max = 1f;

		[SerializeField]
		[HideInInspector]
		public ParameterLoopBehaviour _loopBehaviour;

		[HideInInspector]
		[SerializeField]
		public float _velocity;

		[HideInInspector]
		[SerializeField]
		public float _seekSpeed;

		[SerializeField]
		private float _seekTarget;

		[HideInInspector]
		[SerializeField]
		public bool _resetToDefaultValue = true;

		private float _defaultValue;

		private float _direction = 1f;

		[HideInInspector]
		[SerializeField]
		public RTPMarkers _markers = new RTPMarkers();

		public void Init()
		{
			_value = _seekTarget;
			_defaultValue = _value;
			_direction = 1f;
		}

		public void Reset()
		{
			if (_resetToDefaultValue)
			{
				_value = (_seekTarget = _defaultValue);
			}
			_markers.Reset();
			_direction = 1f;
		}

		public void SetValue(float value)
		{
			AudioTools.Limit(ref value, _min, _max);
			float num = 1f / (_max - _min);
			value *= num;
			_seekTarget = value;
		}

		public void SetNormalisedValue(float value)
		{
			_seekTarget = value;
		}

		public float GetCurrentValue()
		{
			return Mathf.Lerp(_min, _max, _value);
		}

		public float GetNormalisedCurrentValue()
		{
			return _value;
		}

		public void Update()
		{
			float realtimeDelta = FabricTimer.GetRealtimeDelta();
			if (_direction > 0f)
			{
				_seekTarget += _velocity * realtimeDelta;
			}
			else
			{
				_seekTarget -= _velocity * realtimeDelta;
			}
			_markers.Update(_seekTarget, _direction);
			if (_markers.IsMarkerKeyOff())
			{
				_seekTarget = _markers._keyOffMarker._value;
			}
			if (_velocity != 0f)
			{
				if (_seekTarget > MAX_NORMALISED_RANGE)
				{
					if (_loopBehaviour == ParameterLoopBehaviour.Loop)
					{
						_seekTarget -= MAX_NORMALISED_RANGE - MIN_NORMALISED_RANGE;
					}
					else if (_loopBehaviour == ParameterLoopBehaviour.OneShot)
					{
						_seekTarget = MAX_NORMALISED_RANGE;
					}
					else if (_loopBehaviour == ParameterLoopBehaviour.PingPong)
					{
						_direction = -1f;
					}
					else
					{
						_seekTarget = MAX_NORMALISED_RANGE;
					}
					_markers.Reset();
				}
				else if (_seekTarget < MIN_NORMALISED_RANGE)
				{
					if (_loopBehaviour == ParameterLoopBehaviour.Loop)
					{
						_seekTarget += MAX_NORMALISED_RANGE - MIN_NORMALISED_RANGE;
					}
					else if (_loopBehaviour == ParameterLoopBehaviour.OneShot)
					{
						_seekTarget = MIN_NORMALISED_RANGE;
					}
					else if (_loopBehaviour == ParameterLoopBehaviour.PingPong)
					{
						_direction = 1f;
					}
					else
					{
						_seekTarget = MIN_NORMALISED_RANGE;
					}
					_markers.Reset();
				}
				if (_seekTarget > MAX_NORMALISED_RANGE)
				{
					_seekTarget = MAX_NORMALISED_RANGE;
				}
				else if (_seekTarget < MIN_NORMALISED_RANGE)
				{
					_seekTarget = MIN_NORMALISED_RANGE;
				}
			}
			if (_seekSpeed != 0f)
			{
				float num = _seekSpeed * realtimeDelta;
				if (_seekTarget > _value)
				{
					_value += num;
					if (_value > _seekTarget)
					{
						_value = _seekTarget;
					}
				}
				else if (_seekTarget < _value)
				{
					_value -= num;
					if (_value < _seekTarget)
					{
						_value = _seekTarget;
					}
				}
			}
			else
			{
				_value = _seekTarget;
			}
			AudioTools.Limit(ref _value, 0f, 1f);
		}
	}
}
