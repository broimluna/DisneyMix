namespace Fabric.ModularSynth
{
	internal class LFOFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new LFO();
		}

		public override string Name()
		{
			return "LFO/LFO";
		}
	}
}
