namespace Fabric.ModularSynth
{
	internal class TextFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new Text();
		}

		public override string Name()
		{
			return "Panel/Text";
		}
	}
}
