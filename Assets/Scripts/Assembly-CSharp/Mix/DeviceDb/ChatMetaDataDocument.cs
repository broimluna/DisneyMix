using DeviceDB;

namespace Mix.DeviceDb
{
	[Serialized(54, new byte[] { })]
	public class ChatMetaDataDocument : AbstractDocument
	{
		[Indexed]
		[Serialized(0, new byte[] { })]
		public string messageId;

		[Serialized(1, new byte[] { })]
		public float height;
	}
}
