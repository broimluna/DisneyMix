namespace Fabric.ModularSynth
{
	internal class SingleDelayFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new SingleDelay();
		}

		public override string Name()
		{
			return "Delay/SingleDelay";
		}
	}
}
