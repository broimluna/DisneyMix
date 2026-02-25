using Mix.Games.Tray.Friendzy.Message;

namespace Mix.Games.Tray.Friendzy.Menu
{
	public struct MenuMessage : IMessage
	{
		public string Category;

		public string Quiz;

		public MenuMessageType Type;

		public MenuMessage(MenuMessageType aType, string aCategory = null, string aQuiz = null)
		{
			Category = aCategory;
			Quiz = aQuiz;
			Type = aType;
		}
	}
}
