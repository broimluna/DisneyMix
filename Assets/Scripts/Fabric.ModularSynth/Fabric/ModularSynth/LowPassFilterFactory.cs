namespace Fabric.ModularSynth
{
	internal class LowPassFilterFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new LowPassFilter();
		}

		public override string Name()
		{
			return "Filter/LowPassFilter";
		}
	}
}
