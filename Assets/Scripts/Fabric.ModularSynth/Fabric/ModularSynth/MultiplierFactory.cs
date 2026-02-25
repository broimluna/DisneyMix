namespace Fabric.ModularSynth
{
	internal class MultiplierFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new Multiplier();
		}

		public override string Name()
		{
			return "SignalPath/Multiplier";
		}
	}
}
