using Core.MetaData;
using Mix.Localization;

namespace Mix.Data
{
	public class Sticker : BaseContentData
	{
		private const string HEIGHT = "height";

		private const string PUSH_NOTE = "pushNote";

		private const string SOUND_FILE = "soundFile";

		private const string TYPE = "type";

		private const string TAG = "tag";

		public Sticker(Sheet aSheet, Row aRow)
			: base(aSheet, aRow)
		{
		}

		public double GetHeight()
		{
			return row.TryGetFloat(sheet.GetColumnIndex("height"));
		}

		public string GetPushNote(string uid)
		{
			return Singleton<Localizer>.Instance.getString("ContentData.Sticker." + uid + ".PUSH_NOTE");
		}

		public string GetSoundFile()
		{
			return row.TryGetString(sheet.GetColumnIndex("soundFile"));
		}

		public string GetStickerType()
		{
			return row.TryGetString(sheet.GetColumnIndex("type"));
		}

		public string GetTags()
		{
			return row.TryGetString(sheet.GetColumnIndex("tag"));
		}
	}
}
