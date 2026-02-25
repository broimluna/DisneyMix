namespace Fabric.ModularSynth
{
	internal class AddFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new Add();
		}

		public override string Name()
		{
			return "Math/Add";
		}
	}
}
