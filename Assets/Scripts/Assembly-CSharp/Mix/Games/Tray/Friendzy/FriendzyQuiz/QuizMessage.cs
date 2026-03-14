using Mix.Games.Tray.Friendzy.Data;
using Mix.Games.Tray.Friendzy.LoadAsset;
using Mix.Games.Tray.Friendzy.Message;

namespace Mix.Games.Tray.Friendzy.FriendzyQuiz
{
	public class QuizMessage : IMessage
	{
		public QuizMessageType Type;

		public Quiz QuizMessageQuiz;

		public Category QuizMessageCategory;

		public ResultEntry QuizMessageResultEntry;

		public Job JobToDo;

		public QuizMessage(QuizMessageType aType)
		{
			Type = aType;
		}

		public QuizMessage(QuizMessageType aType, Category aCategory, Quiz aQuiz)
		{
			Type = aType;
			QuizMessageCategory = aCategory;
			QuizMessageQuiz = aQuiz;
		}

		public QuizMessage(QuizMessageType aType, ResultEntry aResultEntry)
		{
			Type = aType;
			QuizMessageResultEntry = aResultEntry;
		}

		public QuizMessage(QuizMessageType aType, Job aJob)
		{
			Type = aType;
			JobToDo = aJob;
		}
	}
}
