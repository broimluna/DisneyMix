namespace Fabric.ModularSynth
{
	internal class SamplePlayerFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new SamplePlayer();
		}

		public override string Name()
		{
			return "Sampler/SamplePlayer";
		}
	}
}
