namespace Fabric.ModularSynth
{
	internal class InputFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new Input();
		}

		public override string Name()
		{
			return "IO/Input";
		}
	}
}
