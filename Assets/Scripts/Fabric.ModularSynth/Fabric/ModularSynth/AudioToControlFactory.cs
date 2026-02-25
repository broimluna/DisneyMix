namespace Fabric.ModularSynth
{
	internal class AudioToControlFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new AudioToControl();
		}

		public override string Name()
		{
			return "Auxiliary/AudioToControl";
		}
	}
}
