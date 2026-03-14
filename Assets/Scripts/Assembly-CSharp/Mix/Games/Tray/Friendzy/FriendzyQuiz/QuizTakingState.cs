using Mix.Games.Tray.Friendzy.FSM;
using Mix.Games.Tray.Friendzy.Message;

namespace Mix.Games.Tray.Friendzy.FriendzyQuiz
{
	public class QuizTakingState : IState
	{
		private QuizShow mQuizShow;

		public QuizTakingState(QuizShow inQuizShow)
		{
			mQuizShow = inQuizShow;
		}

		void IState.Enter()
		{
			mQuizShow.ShowNextQuestion();
		}

		void IState.Update()
		{
		}

		void IState.Exit()
		{
		}

		void IState.ReceiveMessage(IMessage eventMessage)
		{
		}
	}
}
