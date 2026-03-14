using System.Runtime.CompilerServices;

namespace Fabric.ModularSynth
{
	public class ControlOutputPin : OutputPin
	{
		private object _value;

		[CompilerGenerated]
		private bool _003CIsVisible_003Ek__BackingField;

		public new eControlType Type { get; set; }

		private bool IsVisible
		{
			[CompilerGenerated]
			set
			{
				_003CIsVisible_003Ek__BackingField = value;
			}
		}

		public object Value
		{
			get
			{
				return _value;
			}
			set
			{
				if (HasValueTypeChanged(value))
				{
					_value = value;
					base.Dirty = true;
				}
			}
		}

		public ControlOutputPin(eControlType type = eControlType.FloatSlider, bool isVisible = true)
			: base(ePinType.Control)
		{
			Type = type;
			IsVisible = isVisible;
			if (Type == eControlType.IntSlider || Type == eControlType.IntConstant)
			{
				_value = 0;
			}
			else if (Type == eControlType.FloatSlider || Type == eControlType.FloatConstant)
			{
				_value = 0f;
			}
			else if (Type == eControlType.Button || Type == eControlType.Switch)
			{
				_value = false;
			}
			else if (Type == eControlType.Text)
			{
				_value = " ";
			}
			else if (Type == eControlType.List)
			{
				_value = new ControlPinList();
			}
		}

		private bool HasValueTypeChanged(object value)
		{
			if ((value is int && Type == eControlType.IntSlider) || Type == eControlType.IntConstant)
			{
				return (int)_value != (int)value;
			}
			if ((value is float && Type == eControlType.FloatSlider) || Type == eControlType.FloatConstant)
			{
				return (float)_value != (float)value;
			}
			if ((value is bool && Type == eControlType.Button) || Type == eControlType.Switch)
			{
				return (bool)_value != (bool)value;
			}
			if (value is string && Type == eControlType.Text)
			{
				return (string)_value != (string)value;
			}
			if (value is ControlPinList && Type == eControlType.List)
			{
				if (((ControlPinList)_value).selectedIndex == ((ControlPinList)value).selectedIndex)
				{
					return false;
				}
				return true;
			}
			return false;
		}
	}
}
