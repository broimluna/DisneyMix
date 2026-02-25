namespace Fabric.ModularSynth
{
	internal class OnStartTriggerFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new OnStartTrigger();
		}

		public override string Name()
		{
			return "Auxiliary/OnStartTrigger";
		}
	}
}
