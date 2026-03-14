using Mix.Games.Tray.Friendzy.FSM;
using Mix.Games.Tray.Friendzy.Message;

namespace Mix.Games.Tray.Friendzy.ResultAnimator
{
	public class PlayerTwoMovementState : IState
	{
		private ResultsAnimator mResultsAnim;

		private bool mDone;

		public PlayerTwoMovementState(ResultsAnimator aResultsAnim)
		{
			mResultsAnim = aResultsAnim;
		}

		void IState.Enter()
		{
			mDone = false;
		}

		void IState.Update()
		{
			if (mResultsAnim.CamController.CheckState("Final") && !mDone && mResultsAnim.LightAnim.SequenceEnded)
			{
				mDone = true;
				mResultsAnim.DisplayFullDescription();
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
