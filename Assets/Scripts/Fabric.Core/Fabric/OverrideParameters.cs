using System;
using UnityEngine;

namespace Fabric
{
	[Serializable]
	public class OverrideParameters
	{
		[SerializeField]
		public bool _overrideIncomingEventAction;

		[SerializeField]
		public EventAction _incomingEventAction;

		[SerializeField]
		public EventAction _overrideEventAction;

		[SerializeField]
		public OverrideParameterType _type;

		[SerializeField]
		private float _floatParameter = 1f;

		[SerializeField]
		private string _stringParameter = "";

		[SerializeField]
		private SwitchPresetData _switchPresetData;

		[SerializeField]
		private DSPParameterData _dspParameterData;

		[SerializeField]
		private TransitionToSnapshotData _transitionToSnapshotData;

		public float FloatParameter
		{
			get
			{
				return _floatParameter;
			}
		}

		public string StringParameter
		{
			get
			{
				return _stringParameter;
			}
		}

		public SwitchPresetData SwitchPresetData
		{
			get
			{
				return _switchPresetData;
			}
		}

		public DSPParameterData DSPParameterData
		{
			get
			{
				return _dspParameterData;
			}
		}

		public TransitionToSnapshotData TransitionToSnapshotData
		{
			get
			{
				return _transitionToSnapshotData;
			}
		}
	}
}
