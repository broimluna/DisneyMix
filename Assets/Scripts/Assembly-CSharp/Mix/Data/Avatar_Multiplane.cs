using Core.MetaData;

namespace Mix.Data
{
	public class Avatar_Multiplane : BaseContentData
	{
		private const string CATEGORY = "category";

		private const string SD = "sd";

		private const string WHITE_LIST = "whiteList";

		private const string REFERENCE_ID = "referenceId";

		private const string DEFAULT_TINT = "defaultTint";

		public Avatar_Multiplane(Sheet aSheet, Row aRow)
			: base(aSheet, aRow)
		{
		}

		public string GetCategory()
		{
			return row.TryGetString(sheet.GetColumnIndex("category"));
		}

		public string GetSd()
		{
			return row.TryGetString(sheet.GetColumnIndex("sd"));
		}

		public bool GetWhiteList()
		{
			return row.TryGetBool(sheet.GetColumnIndex("whiteList"));
		}

		public int GetReferenceId()
		{
			return row.TryGetInt(sheet.GetColumnIndex("referenceId"));
		}

		public int GetDefaultTint()
		{
			return row.TryGetInt(sheet.GetColumnIndex("defaultTint"));
		}
	}
}
