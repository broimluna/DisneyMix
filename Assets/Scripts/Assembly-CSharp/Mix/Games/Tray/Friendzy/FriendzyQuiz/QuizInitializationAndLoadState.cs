using Mix.Games.Tray.Friendzy.FSM;
using Mix.Games.Tray.Friendzy.Message;

namespace Mix.Games.Tray.Friendzy.FriendzyQuiz
{
	public class QuizInitializationAndLoadState : IState
	{
		private QuizShow mQuizShow;

		private bool initialLoad = true;

		public QuizInitializationAndLoadState(QuizShow inQuizShow)
		{
			mQuizShow = inQuizShow;
		}

		void IState.Enter()
		{
			if (initialLoad)
			{
				mQuizShow.InitialLoadQuiz();
				initialLoad = false;
			}
		}

		void IState.Update()
		{
			if (mQuizShow.DataLoadedAndReady)
			{
				mQuizShow.DataLoadedAndReady = false;
				mQuizShow.QuizShowFSM.ChangeToState(mQuizShow.QuizTakingState);
			}
		}

		void IState.Exit()
		{
		}

		void IState.ReceiveMessage(IMessage eventMessage)
		{
		}
	}
}
