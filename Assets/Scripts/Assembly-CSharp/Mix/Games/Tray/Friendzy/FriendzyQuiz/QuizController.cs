namespace Mix.Games.Tray.Friendzy.FriendzyQuiz
{
	public class QuizController
	{
		private FriendzyGame mFriendzyGame;

		private QuizShow mQuizShow;

		public QuizController(FriendzyGame inFriendzyGame)
		{
			mFriendzyGame = inFriendzyGame;
			mQuizShow = mFriendzyGame.FriendzyQuizShow;
			mQuizShow.gameObject.SetActive(false);
			mQuizShow.SetQuizController(this);
		}

		public void ReceiveMessage(ref QuizMessage inMessage)
		{
			switch (inMessage.Type)
			{
			case QuizMessageType.QUIZ_INITIALIZATION:
				mQuizShow.gameObject.SetActive(true);
				mQuizShow.ReceiveMessage(ref inMessage);
				break;
			case QuizMessageType.QUIZ_FINISHED:
			{
				mQuizShow.gameObject.SetActive(false);
				FriendzyMessage message4 = new FriendzyMessage
				{
					type = FriendzyMessageType.QUIZ_FINISHED,
					resultEntry = inMessage.QuizMessageResultEntry
				};
				mFriendzyGame.ReceiveMessage(ref message4);
				break;
			}
			case QuizMessageType.LOAD_QUESTION:
			{
				FriendzyMessage message3 = new FriendzyMessage
				{
					type = FriendzyMessageType.LOAD_DATA,
					jobToDo = inMessage.JobToDo
				};
				mFriendzyGame.ReceiveMessage(ref message3);
				break;
			}
			case QuizMessageType.LOAD_RESULT_ASSETS:
			{
				FriendzyMessage message2 = new FriendzyMessage
				{
					type = FriendzyMessageType.LOAD_RESULT_ASSETS,
					CategoryReference = inMessage.QuizMessageCategory,
					QuizReference = inMessage.QuizMessageQuiz
				};
				mFriendzyGame.ReceiveMessage(ref message2);
				break;
			}
			case QuizMessageType.TURN_OFF_LEVEL_GEOMETRY:
			{
				FriendzyMessage message = new FriendzyMessage
				{
					type = FriendzyMessageType.TURN_OFF_LEVEL_GEOMETRY
				};
				mFriendzyGame.ReceiveMessage(ref message);
				break;
			}
			}
		}
	}
}
