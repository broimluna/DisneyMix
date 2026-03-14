using Mix.Games.Tray.Friendzy.Data;
using Mix.Games.Tray.Friendzy.LoadAsset;
using Mix.Games.Tray.Friendzy.Message;

namespace Mix.Games.Tray.Friendzy
{
	public struct FriendzyMessage : IMessage
	{
		public string Category;

		public string Quiz;

		public ResultEntry resultEntry;

		public FriendzyMessageType type;

		public Job jobToDo;

		public Category CategoryReference;

		public Quiz QuizReference;

		public FriendzyMessage(FriendzyMessageType aType, string aCategory = null, string aQuiz = null, Job aJobToDo = null)
		{
			type = aType;
			Category = aCategory;
			Quiz = aQuiz;
			resultEntry = null;
			jobToDo = aJobToDo;
			CategoryReference = null;
			QuizReference = null;
		}
	}
}
