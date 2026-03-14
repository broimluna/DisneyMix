namespace Fabric.ModularSynth
{
	internal class LevelMeterFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new LevelMeter();
		}

		public override string Name()
		{
			return "Display/LevelMeter";
		}
	}
}
