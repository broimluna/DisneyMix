namespace Fabric.ModularSynth
{
	internal class NotchFilterFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new NotchFilter();
		}

		public override string Name()
		{
			return "Filter/NotchFilterFilter";
		}
	}
}
