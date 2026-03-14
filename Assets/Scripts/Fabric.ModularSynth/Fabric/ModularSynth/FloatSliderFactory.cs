namespace Fabric.ModularSynth
{
	internal class FloatSliderFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new FloatSlider();
		}

		public override string Name()
		{
			return "Panel/FloatSlider";
		}
	}
}
