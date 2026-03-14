namespace Fabric.ModularSynth
{
	internal class FloatConstant : Module
	{
		private ControlOutputPin value = new ControlOutputPin(eControlType.FloatConstant);

		public FloatConstant()
		{
			base.Type = ModuleType.Panel;
			RegisterPin(value, "");
		}
	}
}
