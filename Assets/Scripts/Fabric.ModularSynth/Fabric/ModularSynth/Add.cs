namespace Fabric.ModularSynth
{
	internal class Add : Module
	{
		private ControlInputPin x = new ControlInputPin(1f, 0f, 1f);

		private ControlInputPin y = new ControlInputPin(1f, 0f, 1f);

		private ControlOutputPin r = new ControlOutputPin(eControlType.FloatSlider, false);

		public Add()
		{
			RegisterPin(r, "result");
			RegisterPin(x, "x");
			RegisterPin(y, "y");
		}

		public override void OnControlPinsUpdated()
		{
			r.Value = (float)x.GetValue() + (float)y.GetValue();
		}
	}
}
