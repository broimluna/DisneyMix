namespace Fabric.ModularSynth
{
	public class OutputPin : PinBase
	{
		public bool Dirty { get; set; }

		public InputPin _inputPin { get; set; }

		public OutputPin(ePinType pinType)
			: base(pinType, ePinDirection.Output)
		{
			Dirty = false;
		}

		public override bool IsConnected()
		{
			if (_inputPin == null)
			{
				return false;
			}
			return true;
		}
	}
}
