namespace Fabric.ModularSynth
{
	internal class PitchShifterFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new PitchShifter();
		}

		public override string Name()
		{
			return "Modifier/PitchShifter";
		}
	}
}
