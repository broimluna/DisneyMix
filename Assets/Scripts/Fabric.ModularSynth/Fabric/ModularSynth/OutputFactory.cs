namespace Fabric.ModularSynth
{
	internal class OutputFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new Output();
		}

		public override string Name()
		{
			return "IO/Output";
		}
	}
}
