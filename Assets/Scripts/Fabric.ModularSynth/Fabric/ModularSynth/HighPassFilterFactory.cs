namespace Fabric.ModularSynth
{
	internal class HighPassFilterFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new HighPassFilter();
		}

		public override string Name()
		{
			return "Filter/HighPassFilter";
		}
	}
}
