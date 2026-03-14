using Core.MetaData;

namespace Mix.Data
{
	public class Sticker_Tag : BaseGameData
	{
		private const string TAG_MAP = "tagMap";

		private const string ICON = "icon";

		public Sticker_Tag(Sheet aSheet, Row aRow)
			: base(aSheet, aRow)
		{
		}

		public string GetTagMap()
		{
			return row.TryGetString(sheet.GetColumnIndex("tagMap"));
		}

		public string GetIcon()
		{
			return row.TryGetString(sheet.GetColumnIndex("icon"));
		}
	}
}
