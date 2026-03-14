namespace Fabric.ModularSynth
{
	internal class SubtractFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new Subtract();
		}

		public override string Name()
		{
			return "Math/Subtract";
		}
	}
}
