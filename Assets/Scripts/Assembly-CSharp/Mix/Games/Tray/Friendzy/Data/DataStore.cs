namespace Mix.Games.Tray.Friendzy.Data
{
	public class DataStore
	{
		public readonly Content content;

		public readonly SpreadsheetInfo spreadsheetInfo;

		public readonly int version;

		public string[] GetQuizzesByIP(string aCategory)
		{
			string[] array = null;
			Quiz[] array2 = null;
			Category[] friendzy = content.objects.friendzy;
			for (int i = 0; i < friendzy.Length; i++)
			{
				if (string.Equals(aCategory, friendzy[i].Name))
				{
					array2 = friendzy[i].Quizzes;
					break;
				}
			}
			array = new string[array2.Length];
			for (int j = 0; j < array.Length; j++)
			{
				array[j] = array2[j].Title;
			}
			return array;
		}

		public Quiz GetQuizByCategoryAndQuizName(string aCategory, string aQuiz)
		{
			Category category = null;
			Quiz result = null;
			Category[] friendzy = content.objects.friendzy;
			for (int i = 0; i < friendzy.Length; i++)
			{
				if (string.Equals(friendzy[i].Name, aCategory))
				{
					category = friendzy[i];
					break;
				}
			}
			for (int j = 0; j < category.Quizzes.Length; j++)
			{
				if (string.Equals(category.Quizzes[j].Title, aQuiz))
				{
					result = category.Quizzes[j];
					break;
				}
			}
			return result;
		}

		public Category GetCategoryByName(string aCategory)
		{
			Category result = null;
			Category[] friendzy = content.objects.friendzy;
			for (int i = 0; i < friendzy.Length; i++)
			{
				if (string.Equals(friendzy[i].Name, aCategory))
				{
					result = friendzy[i];
					break;
				}
			}
			return result;
		}

		public Category[] GetCategories()
		{
			return content.objects.friendzy;
		}
	}
}
