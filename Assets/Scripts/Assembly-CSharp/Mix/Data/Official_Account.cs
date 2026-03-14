using Core.MetaData;

namespace Mix.Data
{
	public class Official_Account : BaseContentData
	{
		private const string OAID = "oaid";

		private const string DESCRIPTION = "description";

		private const string ICON = "icon";

		private const string BACKGROUND = "background";

		private const string TINT_COLOR_1 = "tint_color_1";

		private const string TINT_COLOR_2 = "tint_color_2";

		private const string TINT_COLOR_3 = "tint_color_3";

		public Official_Account(Sheet aSheet, Row aRow)
			: base(aSheet, aRow)
		{
		}

		public string GetOAID()
		{
			return row.TryGetString(sheet.GetColumnIndex("oaid"));
		}

		public string GetDescription()
		{
			return row.TryGetString(sheet.GetColumnIndex("description"));
		}

		public string GetIcon()
		{
			return row.TryGetString(sheet.GetColumnIndex("icon"));
		}

		public string GetBackground()
		{
			return row.TryGetString(sheet.GetColumnIndex("background"));
		}

		public string GetTintColor1()
		{
			return row.TryGetString(sheet.GetColumnIndex("tint_color_1"));
		}

		public string GetTintColor2()
		{
			return row.TryGetString(sheet.GetColumnIndex("tint_color_2"));
		}

		public string GetTintColor3()
		{
			return row.TryGetString(sheet.GetColumnIndex("tint_color_3"));
		}
	}
}
