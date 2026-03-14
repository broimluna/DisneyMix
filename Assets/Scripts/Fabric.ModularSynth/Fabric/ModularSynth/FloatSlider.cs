namespace Fabric.ModularSynth
{
	internal class FloatSlider : Module
	{
		private ControlOutputPin value = new ControlOutputPin();

		public FloatSlider()
		{
			base.Type = ModuleType.Panel;
			RegisterPin(value, "");
		}
	}
}
