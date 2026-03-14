namespace Fabric.ModularSynth
{
	internal class ADSRFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new ADSR();
		}

		public override string Name()
		{
			return "Envelope/ADSR";
		}
	}
}
