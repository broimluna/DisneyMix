namespace Fabric.ModularSynth
{
	internal class Button : Module
	{
		private ControlOutputPin value = new ControlOutputPin(eControlType.Button);

		public Button()
		{
			base.Type = ModuleType.Panel;
			RegisterPin(value, "");
		}
	}
}
