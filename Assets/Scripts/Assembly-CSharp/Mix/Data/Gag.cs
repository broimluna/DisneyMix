using Core.MetaData;
using Mix.Localization;

namespace Mix.Data
{
	public class Gag : BaseContentData
	{
		private const string POKABLE = "pokable";

		private const string PUSH_NOTE = "pushNote";

		private const string TYPE = "type";

		public Gag(Sheet aSheet, Row aRow)
			: base(aSheet, aRow)
		{
		}

		public bool GetPokable()
		{
			return row.TryGetBool(sheet.GetColumnIndex("pokable"));
		}

		public string GetPushNote(string uid)
		{
			return Singleton<Localizer>.Instance.getString("ContentData.Gag." + uid + ".PUSH_NOTE");
		}

		public string GetGagType()
		{
			return row.TryGetString(sheet.GetColumnIndex("type"));
		}

		public bool IsAutoPlay()
		{
			if (string.IsNullOrEmpty(GetGagType()))
			{
				return true;
			}
			return false;
		}
	}
}
