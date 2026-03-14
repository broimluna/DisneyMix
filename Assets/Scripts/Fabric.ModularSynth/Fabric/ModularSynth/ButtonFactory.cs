namespace Fabric.ModularSynth
{
	internal class ButtonFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new Button();
		}

		public override string Name()
		{
			return "Panel/Button";
		}
	}
}
