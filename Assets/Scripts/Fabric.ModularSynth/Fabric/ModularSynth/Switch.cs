namespace Fabric.ModularSynth
{
	internal class Switch : Module
	{
		private ControlOutputPin value = new ControlOutputPin(eControlType.Switch);

		public Switch()
		{
			base.Type = ModuleType.Panel;
			RegisterPin(value, "");
		}
	}
}
