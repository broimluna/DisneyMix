using Core.MetaData;

namespace Mix.Data
{
	public abstract class BaseContentData
	{
		private const string UID = "uid";

		private const string NAME = "name";

		private const string THUMB = "thumb";

		private const string HD = "hd";

		private const string ORDER = "order";

		private const string HIDDEN = "hidden";

		private const string UNLOCKABLE = "unlockable";

		private const string PREVIEW = "preview";

		private const string IS_NEW = "isNew";

		protected Row row;

		protected Sheet sheet;

		public BaseContentData(Sheet aSheet, Row aRow)
		{
			sheet = aSheet;
			row = aRow;
		}

		public string GetUid()
		{
			return row.TryGetString(sheet.GetColumnIndex("uid"));
		}

		public string GetName()
		{
			return row.TryGetString(sheet.GetColumnIndex("name"));
		}

		public string GetThumb()
		{
			return row.TryGetString(sheet.GetColumnIndex("thumb"));
		}

		public string GetHd()
		{
			return row.TryGetString(sheet.GetColumnIndex("hd"));
		}

		public int GetOrder()
		{
			return row.TryGetInt(sheet.GetColumnIndex("order"));
		}

		public bool GetHidden()
		{
			return row.TryGetBool(sheet.GetColumnIndex("hidden"));
		}

		public bool GetUnlockable()
		{
			return row.TryGetBool(sheet.GetColumnIndex("unlockable"));
		}

		public bool GetPreview()
		{
			return row.TryGetBool(sheet.GetColumnIndex("preview"));
		}

		public bool GetNew()
		{
			return row.TryGetBool(sheet.GetColumnIndex("isNew"));
		}
	}
}
