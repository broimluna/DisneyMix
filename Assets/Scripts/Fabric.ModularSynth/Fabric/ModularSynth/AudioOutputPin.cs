namespace Fabric.ModularSynth
{
	public class AudioOutputPin : OutputPin
	{
		private AudioBuffer audioBuffer;

		public AudioOutputPin()
			: base(ePinType.Audio)
		{
			audioBuffer = new AudioBuffer(4410, 2);
		}

		public AudioBuffer GetBuffer()
		{
			return audioBuffer;
		}

		public bool HasBuffer()
		{
			if (audioBuffer == null)
			{
				return false;
			}
			return true;
		}
	}
}
