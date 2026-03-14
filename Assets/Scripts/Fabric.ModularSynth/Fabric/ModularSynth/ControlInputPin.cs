using System.Runtime.CompilerServices;

namespace Fabric.ModularSynth
{
	public class ControlInputPin : InputPin
	{
		[CompilerGenerated]
		private object _003CMin_003Ek__BackingField;

		public new eControlType Type { get; private set; }

		public object Default { get; set; }

		public object Min
		{
			[CompilerGenerated]
			set
			{
				_003CMin_003Ek__BackingField = value;
			}
		}

		public object Max { get; set; }

		public ControlInputPin(int _default, int _min, int _max)
			: base(ePinType.Control)
		{
			Default = _default;
			Min = _min;
			Max = _max;
			Type = eControlType.IntSlider;
		}

		public ControlInputPin(float _default, float _min, float _max)
			: base(ePinType.Control)
		{
			Default = _default;
			Min = _min;
			Max = _max;
			Type = eControlType.FloatSlider;
		}

		public ControlInputPin(bool _default)
			: base(ePinType.Control)
		{
			Default = _default;
			Type = eControlType.Button;
		}

		public ControlInputPin(ControlPinList _list)
			: base(ePinType.Control)
		{
			Default = _list;
			Type = eControlType.List;
		}

		private ControlOutputPin GetOutputPin()
		{
			if (base._outputPin != null)
			{
				ControlOutputPin controlOutputPin = base._outputPin as ControlOutputPin;
				if (controlOutputPin != null)
				{
					return controlOutputPin;
				}
			}
			return null;
		}

		public object GetValue()
		{
			ControlOutputPin outputPin = GetOutputPin();
			if (outputPin != null)
			{
				return outputPin.Value;
			}
			return Default;
		}
	}
}
