using Mix.Games.Tray.Friendzy.FSM;
using Mix.Games.Tray.Friendzy.Message;

namespace Mix.Games.Tray.Friendzy.ResultAnimator
{
	public class DisplayResultsState : IState
	{
		private ResultsAnimator mResultsAnim;

		private bool mDone;

		public DisplayResultsState(ResultsAnimator aResultsAnim)
		{
			mResultsAnim = aResultsAnim;
		}

		void IState.Enter()
		{
			mDone = false;
			mResultsAnim.ActivatePlayerTwo();
		}

		void IState.Update()
		{
			if (mResultsAnim.PedestalAnimators[1].GetCurrentAnimatorStateInfo(0).IsName("Pedestal2_Final") && !mDone)
			{
				mDone = true;
				mResultsAnim.SetLightBlinkRate(0.35f);
				mResultsAnim.ActivateResultScreen(true);
			}
		}

		void IState.Exit()
		{
		}

		void IState.ReceiveMessage(IMessage aEventMessage)
		{
		}
	}
}
