using System;
using UnityEngine;

namespace Fabric
{
	[Serializable]
	public class DSPParameter
	{
		[SerializeField]
		private float _value = 1f;

		[SerializeField]
		private float _min;

		[SerializeField]
		private float _max = 1f;

		private InterpolatedParameter _interpolatedParameter = new InterpolatedParameter();

		public float Min
		{
			get
			{
				return _min;
			}
		}

		public float Max
		{
			get
			{
				return _max;
			}
		}

		public DSPParameter()
		{
		}

		public DSPParameter(float value, float min, float max)
		{
			_value = value;
			_min = min;
			_max = max;
		}

		public void SetValue(float value, float time = 0f, float curve = 0.5f)
		{
			_value = value;
			if (_value >= _max)
			{
				_value = _max;
			}
			if (_value <= _min)
			{
				_value = _min;
			}
			_interpolatedParameter.SetTarget(FabricTimer.Get(), value, time, curve);
		}

		public float GetTargetValue()
		{
			return _value;
		}

		public float GetValue()
		{
			return _interpolatedParameter.Get(FabricTimer.Get());
		}

		public bool HasReachedTarget()
		{
			return _interpolatedParameter.HasReachedTarget();
		}
	}
}
