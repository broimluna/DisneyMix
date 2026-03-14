namespace Fabric.ModularSynth
{
	internal class MixerFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new Mixer();
		}

		public override string Name()
		{
			return "SignalPath/Mixer";
		}
	}
}
