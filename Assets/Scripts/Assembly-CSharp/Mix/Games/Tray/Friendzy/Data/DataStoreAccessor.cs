using LitJson;

namespace Mix.Games.Tray.Friendzy.Data
{
	public class DataStoreAccessor
	{
		public readonly DataStore mData;

		public DataStoreAccessor(string aJson)
		{
			mData = JsonMapper.ToObject<DataStore>(aJson);
		}

		public DataStoreAccessor(DataStore aData)
		{
			mData = aData;
		}

		public string[] GetQuizzesByIP(string aCategory)
		{
			return mData.GetQuizzesByIP(aCategory);
		}

		public Quiz GetQuizByCategoryAndQuizName(string aCategory, string aQuiz)
		{
			return mData.GetQuizByCategoryAndQuizName(aCategory, aQuiz);
		}

		public Category GetCategoryByName(string aCategory)
		{
			return mData.GetCategoryByName(aCategory);
		}

		public Category[] GetCategories()
		{
			return mData.GetCategories();
		}
	}
}
