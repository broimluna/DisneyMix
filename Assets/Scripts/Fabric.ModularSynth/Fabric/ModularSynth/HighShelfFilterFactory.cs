namespace Fabric.ModularSynth
{
	internal class HighShelfFilterFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new HighShelfFilter();
		}

		public override string Name()
		{
			return "Filter/HighShelfFilter";
		}
	}
}
