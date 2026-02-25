namespace Mix.Games.Tray.Friendzy.Data
{
	public class Question
	{
		public readonly string Content;

		public readonly QUESTION_TYPE Type;

		public readonly string[] Answers;

		public readonly Picture[] Pictures;

		public readonly int[][] IndexToResult;

		public QUESTION_TYPE GetQuestionGenre()
		{
			QUESTION_TYPE result = QUESTION_TYPE.QUESTION_UNINIT;
			if (Type == QUESTION_TYPE.QUESTION_FOUR_TEXT || Type == QUESTION_TYPE.QUESTION_TWO_HOR_TEXT || Type == QUESTION_TYPE.QUESTION_TWO_VERT_TEXT)
			{
				result = QUESTION_TYPE.QUESTION_TWO_VERT_TEXT;
			}
			if (Type == QUESTION_TYPE.QUESTION_FOUR_PICTURE || Type == QUESTION_TYPE.QUESTION_TWO_HOR_PIC || Type == QUESTION_TYPE.QUESTION_TWO_VERT_PIC)
			{
				result = QUESTION_TYPE.QUESTION_TWO_VERT_PIC;
			}
			return result;
		}
	}
}
