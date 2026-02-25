namespace Fabric.ModularSynth
{
	internal class IntConstant : Module
	{
		private ControlOutputPin value = new ControlOutputPin(eControlType.IntConstant);

		public IntConstant()
		{
			base.Type = ModuleType.Panel;
			RegisterPin(value, "");
		}
	}
}
