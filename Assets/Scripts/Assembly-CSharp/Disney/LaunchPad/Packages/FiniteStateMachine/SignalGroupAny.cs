using System.Collections.Generic;
using Disney.LaunchPadFramework.Utility;
using UnityEngine;

namespace Disney.LaunchPad.Packages.FiniteStateMachine
{
	public class SignalGroupAny : Signal
	{
		public List<Signal> mSignals = new List<Signal>();

		public Signal mActiveSignal;

		private bool mSignalOverride;

		public override bool IsSignaled()
		{
			foreach (Signal mSignal in mSignals)
			{
				if (mSignal.IsSignaled())
				{
					mActiveSignal = mSignal;
					return true;
				}
			}
			return mSignalOverride;
		}

		public override bool ActivateSignal()
		{
			mSignalOverride = true;
			mActiveSignal = this;
			return true;
		}

		public override void Reset()
		{
			foreach (Signal mSignal in mSignals)
			{
				mSignal.Reset();
			}
			mSignalOverride = false;
			mActiveSignal = null;
		}

		public void RefreshChildrenSignalMapping()
		{
			mSignals.Clear();
			Component[] componentsInChildren = GetComponentsInChildren(typeof(Signal));
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Signal signal = componentsInChildren[i] as Signal;
				if (signal != null && signal != this)
				{
					AddSignal(signal);
				}
			}
		}

		private void AddSignal(Signal signal)
		{
			if (signal != this)
			{
				mSignals.AddIfUnique(signal);
			}
		}
	}
}
