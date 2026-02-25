using Core.MetaData;

namespace Mix.Data
{
	public class Sticker_Pack : BaseContentData
	{
		private const string STICKERS = "stickers";

		public Sticker_Pack(Sheet aSheet, Row aRow)
			: base(aSheet, aRow)
		{
		}

		public string MyStickers()
		{
			return row.TryGetString(sheet.GetColumnIndex("stickers"));
		}

		public override bool Equals(object obj)
		{
			if (obj is Sticker_Pack)
			{
				return ((Sticker_Pack)obj).GetUid() == GetUid();
			}
			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public string[] GetStickers()
		{
			string text = MyStickers();
			if (string.IsNullOrEmpty(text))
			{
				return new string[0];
			}
			return text.Split(',');
		}
	}
}
