using Mix.Games.Tray.Friendzy.FSM;
using Mix.Games.Tray.Friendzy.Message;

namespace Mix.Games.Tray.Friendzy.ResultAnimator
{
	public class InitialState : IState
	{
		private const float FADE_DURATION = 0.15f;

		private ResultsAnimator mResultsAnim;

		public InitialState(ResultsAnimator aResultsAnim)
		{
			mResultsAnim = aResultsAnim;
		}

		void IState.Enter()
		{
		}

		void IState.Update()
		{
			if (mResultsAnim.CamController.CheckState("CameraFullyZoomedOut"))
			{
				mResultsAnim.mFSM.ChangeToState(mResultsAnim.ResultDetermined);
			}
		}

		void IState.Exit()
		{
			mResultsAnim.Background.ToggleFade(true, 0.15f);
			mResultsAnim.PedestalLift();
			mResultsAnim.ResultsZoom();
			mResultsAnim.StartLightingSequence();
		}

		void IState.ReceiveMessage(IMessage aEventMessage)
		{
		}
	}
}
