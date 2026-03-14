namespace Fabric.ModularSynth
{
	internal class IntConstantFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new IntConstant();
		}

		public override string Name()
		{
			return "Panel/IntConstant";
		}
	}
}
