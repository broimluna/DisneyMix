namespace Fabric.ModularSynth
{
	public class AudioInputPin : InputPin
	{
		public AudioInputPin()
			: base(ePinType.Audio)
		{
		}

		public override bool IsConnected()
		{
			if (base._outputPin == null)
			{
				return false;
			}
			return true;
		}

		public AudioBuffer GetBuffer()
		{
			if (base._outputPin != null)
			{
				AudioOutputPin audioOutputPin = base._outputPin as AudioOutputPin;
				if (audioOutputPin != null)
				{
					return audioOutputPin.GetBuffer();
				}
				return null;
			}
			return null;
		}
	}
}
