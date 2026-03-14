namespace Fabric.ModularSynth
{
	public abstract class ModuleFactory
	{
		public abstract Module CreateInstance();

		public abstract string Name();
	}
}
