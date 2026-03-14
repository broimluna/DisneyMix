namespace Fabric.ModularSynth
{
	public class InputPin : PinBase
	{
		public OutputPin _outputPin { get; set; }

		public InputPin(ePinType pinType)
			: base(pinType, ePinDirection.Input)
		{
			_outputPin = null;
		}

		public override bool IsConnected()
		{
			if (_outputPin == null)
			{
				return false;
			}
			return true;
		}

		public bool IsDirty()
		{
			if (_outputPin != null)
			{
				return _outputPin.Dirty;
			}
			return false;
		}

		public void ResetDirtyFlag()
		{
			if (_outputPin != null)
			{
				_outputPin.Dirty = false;
			}
		}
	}
}
