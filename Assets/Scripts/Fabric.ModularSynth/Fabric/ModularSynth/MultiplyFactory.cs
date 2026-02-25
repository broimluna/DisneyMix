namespace Fabric.ModularSynth
{
	internal class MultiplyFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new Multiply();
		}

		public override string Name()
		{
			return "Math/Multiply";
		}
	}
}
