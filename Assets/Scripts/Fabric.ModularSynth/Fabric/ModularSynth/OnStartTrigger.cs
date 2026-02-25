namespace Fabric.ModularSynth
{
	internal class OnStartTrigger : Module
	{
		private ControlOutputPin controlOutput = new ControlOutputPin(eControlType.Button, false);

		public OnStartTrigger()
		{
			RegisterPin(controlOutput, "Trigger");
		}

		public override void OnPlay()
		{
			controlOutput.Value = true;
		}
	}
}
