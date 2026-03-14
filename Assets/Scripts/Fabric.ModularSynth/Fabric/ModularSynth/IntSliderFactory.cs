namespace Fabric.ModularSynth
{
	internal class IntSliderFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new IntSlider();
		}

		public override string Name()
		{
			return "Panel/IntSlider";
		}
	}
}
