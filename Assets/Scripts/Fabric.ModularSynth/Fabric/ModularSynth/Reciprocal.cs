namespace Fabric.ModularSynth
{
	internal class Reciprocal : Module
	{
		private ControlInputPin input = new ControlInputPin(1f, 0f, 1f);

		private ControlOutputPin output = new ControlOutputPin(eControlType.FloatSlider, false);

		public Reciprocal()
		{
			RegisterPin(input, "1/input");
			RegisterPin(output, "output");
		}

		public override void OnControlPinsUpdated()
		{
			output.Value = 1f / (float)input.GetValue();
		}
	}
}
