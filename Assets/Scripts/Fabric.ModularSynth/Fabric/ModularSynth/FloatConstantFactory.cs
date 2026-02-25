namespace Fabric.ModularSynth
{
	internal class FloatConstantFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new FloatConstant();
		}

		public override string Name()
		{
			return "Panel/FloatConstant";
		}
	}
}
