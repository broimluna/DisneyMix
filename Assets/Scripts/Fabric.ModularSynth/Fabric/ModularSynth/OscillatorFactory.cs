namespace Fabric.ModularSynth
{
	internal class OscillatorFactory : ModuleFactory
	{
		public override Module CreateInstance()
		{
			return new Oscillator();
		}

		public override string Name()
		{
			return "Oscillator/Oscillator";
		}
	}
}
