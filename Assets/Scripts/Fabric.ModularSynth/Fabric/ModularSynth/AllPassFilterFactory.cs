namespace Fabric.ModularSynth
{
	internal class AllPassFilterFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new AllPassFilter();
		}

		public override string Name()
		{
			return "Filter/AllPassFilter";
		}
	}
}
