namespace Fabric.ModularSynth
{
	internal class LowShelfFilterFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new LowShelfFilter();
		}

		public override string Name()
		{
			return "Filter/LowShelfFilter";
		}
	}
}
