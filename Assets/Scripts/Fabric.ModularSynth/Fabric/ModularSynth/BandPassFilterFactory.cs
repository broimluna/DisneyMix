namespace Fabric.ModularSynth
{
	internal class BandPassFilterFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new BandPassFilter();
		}

		public override string Name()
		{
			return "Filter/BandPassFilter";
		}
	}
}
