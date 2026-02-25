namespace Fabric.ModularSynth
{
	internal class IntSlider : Module
	{
		private ControlOutputPin value = new ControlOutputPin(eControlType.IntSlider);

		public IntSlider()
		{
			base.Type = ModuleType.Panel;
			RegisterPin(value, "");
		}
	}
}
