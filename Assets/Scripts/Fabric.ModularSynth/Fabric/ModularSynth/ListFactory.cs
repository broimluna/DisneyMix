namespace Fabric.ModularSynth
{
	internal class ListFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new ListModule();
		}

		public override string Name()
		{
			return "Panel/List";
		}
	}
}
