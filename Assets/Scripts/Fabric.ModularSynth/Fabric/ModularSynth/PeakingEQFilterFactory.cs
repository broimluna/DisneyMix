namespace Fabric.ModularSynth
{
	internal class PeakingEQFilterFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new PeakingEQFilter();
		}

		public override string Name()
		{
			return "Filter/PeakingEQFilter";
		}
	}
}
