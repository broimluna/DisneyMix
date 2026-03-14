using Core.MetaData;
using Mix.Games.Data;
using Mix.Localization;

namespace Mix.Data
{
	public abstract class BaseGameData : BaseContentData, IEntitlementGameData
	{
		private const string PUSH_NOTE = "pushNote";

		private const string LOGO = "logo";

		private const string POST = "post";

		private const string RESULT = "result";

		private const string PAUSE_IMG = "pause_img";

		private const string ATTEMPTS = "attempts";

		private const string BASE_URL = "base_url";

		private const string POST_HEIGHT = "post_height";

		private const string LOGO_HEIGHT = "logo_height";

		private const string RESULT_HEIGHT = "results_height";

		private const string THUMB_HEIGHT = "thumb_height";

		private const string DURATION = "duration";

		public BaseGameData(Sheet aSheet, Row aRow)
			: base(aSheet, aRow)
		{
		}

		string IEntitlementGameData.GetPauseImage()
		{
			return row.TryGetString(sheet.GetColumnIndex("pause_img"));
		}

		string IEntitlementGameData.GetThumbImage()
		{
			return GetThumb();
		}

		string IEntitlementGameData.GetDuration()
		{
			return row.TryGetString(sheet.GetColumnIndex("duration"));
		}

		public string GetPushNote(string uid)
		{
			return Singleton<Localizer>.Instance.getString("ContentData.Game." + uid + ".PUSH_NOTE");
		}

		public string GetLogo()
		{
			return row.TryGetString(sheet.GetColumnIndex("logo"));
		}

		public string GetPost()
		{
			return row.TryGetString(sheet.GetColumnIndex("post"));
		}

		public string GetResult()
		{
			return row.TryGetString(sheet.GetColumnIndex("result"));
		}

		public string GetAttempts()
		{
			return row.TryGetString(sheet.GetColumnIndex("attempts"));
		}

		public string GetBaseUrl()
		{
			return row.TryGetString(sheet.GetColumnIndex("base_url"));
		}

		public int GetPostHeight()
		{
			return row.TryGetInt(sheet.GetColumnIndex("post_height"));
		}

		public int GetLogoHeight()
		{
			return row.TryGetInt(sheet.GetColumnIndex("logo_height"));
		}

		public int GetResultsHeight()
		{
			return row.TryGetInt(sheet.GetColumnIndex("results_height"));
		}

		public int GetThumbHeight()
		{
			return row.TryGetInt(sheet.GetColumnIndex("thumb_height"));
		}

		 string IEntitlementGameData.GetName()
		{
			return GetName();
		}

		 string IEntitlementGameData.GetHd()
		{
			return GetHd();
		}

		 string IEntitlementGameData.GetUid()
		{
			return GetUid();
		}
	}
}
