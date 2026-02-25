using Core.MetaData;

namespace Mix.Data
{
	public class Official_Account_Bot : BaseContentData
	{
		private const string BOTID = "botid";

		private const string ICON = "icon";

		public Official_Account_Bot(Sheet aSheet, Row aRow)
			: base(aSheet, aRow)
		{
		}

		public string GetBotId()
		{
			return row.TryGetString(sheet.GetColumnIndex("botid"));
		}

		public string GetIcon()
		{
			return row.TryGetString(sheet.GetColumnIndex("icon"));
		}
	}
}
