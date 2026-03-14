namespace Fabric.ModularSynth
{
	internal class ListModule : Module
	{
		private ControlOutputPin value = new ControlOutputPin(eControlType.List);

		public ListModule()
		{
			base.Type = ModuleType.Panel;
			RegisterPin(value, "");
		}
	}
}
