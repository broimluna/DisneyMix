using Mix.Data;
using Mix.Games.Data;

namespace Mix.DataEntry
{
	public class SharedEntitlement
	{
		private readonly string uid;

		private readonly bool hidden;

		private readonly bool preview;

		private readonly bool isNew;

		private readonly string tags;

		private readonly string pushNote;

		private readonly int order;

		private readonly string spreadsheetName;

		private readonly string worksheetName;

		public string Uid
		{
			get
			{
				return uid;
			}
		}

		public bool Hidden
		{
			get
			{
				return hidden;
			}
		}

		public bool Preview
		{
			get
			{
				return preview;
			}
		}

		public bool New
		{
			get
			{
				return isNew;
			}
		}

		public string Tags
		{
			get
			{
				return tags;
			}
		}

		public string PushNote
		{
			get
			{
				return pushNote;
			}
		}

		public int Order
		{
			get
			{
				return order;
			}
		}

		public string SpreadsheetName
		{
			get
			{
				return spreadsheetName;
			}
		}

		public string WorksheetName
		{
			get
			{
				return worksheetName;
			}
		}

		public string Name { get; private set; }

		public SharedEntitlement(MixGameContentData mixGameContentData)
		{
			uid = mixGameContentData.uid;
			hidden = mixGameContentData.hidden;
			preview = mixGameContentData.preview;
			order = mixGameContentData.order;
			isNew = mixGameContentData.isNew;
			tags = mixGameContentData.tags;
			pushNote = mixGameContentData.pushNote;
			spreadsheetName = mixGameContentData.GetType().Name;
			if (!string.IsNullOrEmpty(mixGameContentData.worksheetName))
			{
				worksheetName = mixGameContentData.worksheetName;
			}
			Name = "Game Item";
		}

		public SharedEntitlement(BaseContentData baseContentData)
		{
			uid = baseContentData.GetUid();
			hidden = baseContentData.GetHidden();
			preview = baseContentData.GetPreview();
			order = baseContentData.GetOrder();
			isNew = baseContentData.GetNew();
			if (baseContentData is Sticker)
			{
				tags = ((Sticker)baseContentData).GetTags();
				pushNote = ((Sticker)baseContentData).GetPushNote(baseContentData.GetUid());
			}
			spreadsheetName = "ContentData";
			worksheetName = baseContentData.GetType().Name;
			Name = baseContentData.GetName();
		}
	}
}
