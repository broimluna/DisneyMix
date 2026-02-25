namespace Fabric.ModularSynth
{
	internal class CrossfadeFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new Crossfade();
		}

		public override string Name()
		{
			return "SignalPath/Crossfade";
		}
	}
}
