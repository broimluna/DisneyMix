namespace Fabric.ModularSynth
{
	internal class DevideFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new Devide();
		}

		public override string Name()
		{
			return "Math/Devide";
		}
	}
}
