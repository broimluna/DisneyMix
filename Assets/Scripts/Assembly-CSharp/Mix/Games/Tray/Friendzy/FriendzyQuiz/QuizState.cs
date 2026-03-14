using Mix.Games.Tray.Friendzy.Data;
using Mix.Games.Tray.Friendzy.FSM;
using Mix.Games.Tray.Friendzy.Message;

namespace Mix.Games.Tray.Friendzy.FriendzyQuiz
{
	public class QuizState : IState
	{
		private FriendzyGame mFriendzyGame;

		private Quiz mQuiz;

		private Category mCategory;

		public QuizState(FriendzyGame inFriendzyGame)
		{
			mFriendzyGame = inFriendzyGame;
		}

		void IState.Enter()
		{
			mQuiz = mFriendzyGame.GetCurrentQuizInstance();
			mCategory = mFriendzyGame.GetCurrentCategoryInstance();
			QuizMessage inMessage = new QuizMessage(QuizMessageType.QUIZ_INITIALIZATION, mCategory, mQuiz);
			mFriendzyGame.FriendzyQuizController.ReceiveMessage(ref inMessage);
			BaseGameController.Instance.Session.SessionSounds.SetVolumeEvent(FriendzyGame.MUSIC_CUE_NAME, 0.5f);
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
