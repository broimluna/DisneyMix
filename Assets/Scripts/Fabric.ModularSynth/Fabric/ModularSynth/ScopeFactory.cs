namespace Fabric.ModularSynth
{
	internal class ScopeFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new Scope();
		}

		public override string Name()
		{
			return "Display/Scope";
		}
	}
}
