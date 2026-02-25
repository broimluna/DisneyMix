namespace Fabric.ModularSynth
{
	internal class SwitchFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new Switch();
		}

		public override string Name()
		{
			return "Panel/Switch";
		}
	}
}
