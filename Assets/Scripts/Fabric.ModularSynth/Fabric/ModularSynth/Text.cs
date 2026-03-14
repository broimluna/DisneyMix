namespace Fabric.ModularSynth
{
	internal class Text : Module
	{
		private ControlOutputPin value = new ControlOutputPin(eControlType.Text);

		public Text()
		{
			base.Type = ModuleType.Panel;
			RegisterPin(value, "");
		}
	}
}
