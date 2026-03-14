using DeviceDB;

namespace Mix.DeviceDb
{
	[Serialized(53, new byte[] { 64, 57, 55, 58, 63, 59 })]
	public class BaseFakeFirstFriendDocument : AbstractDocument
	{
		[Serialized(0, new byte[] { })]
		public bool isFromFriend;

		[Serialized(1, new byte[] { })]
		public long timeSent;

		[Serialized(2, new byte[] { })]
		[Indexed]
		public string uniqueId;

		[Serialized(3, new byte[] { })]
		public bool isSent;

		[Serialized(4, new byte[] { })]
		public bool isRead;

		[Serialized(5, new byte[] { })]
		public string ContentId_or_GameName_or_Text_or_MessageId;

		[Serialized(6, new byte[] { })]
		public string GameData_or_FilePath_or_VideoPath;

		[Serialized(7, new byte[] { })]
		public string Encoding_or_Format;

		[Serialized(8, new byte[] { })]
		public int Width_or_Bitrate;

		[Serialized(9, new byte[] { })]
		public int Height_or_Duration;

		[Serialized(10, new byte[] { })]
		public int VideoWidth;

		[Serialized(11, new byte[] { })]
		public int VideoHeight;

		[Serialized(12, new byte[] { })]
		public string ThumbnailPath;

		[Serialized(13, new byte[] { })]
		public string ThumbnailEncoding;

		[Serialized(14, new byte[] { })]
		public int ThumbnailWidth;

		[Serialized(15, new byte[] { })]
		public int ThumbnailHeight;

		[Serialized(150, new byte[] { })]
		public byte EntryTypeId;

		public bool IsType(byte aType)
		{
			if (EntryTypeId != 0 && EntryTypeId == aType)
			{
				return true;
			}
			if (TypeId == aType)
			{
				return true;
			}
			return false;
		}
	}
}
