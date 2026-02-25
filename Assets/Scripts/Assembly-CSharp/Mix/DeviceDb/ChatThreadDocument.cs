using DeviceDB;

namespace Mix.DeviceDb
{
	[Serialized(62, new byte[] { })]
	public class ChatThreadDocument : AbstractDocument
	{
		[Indexed]
		[Serialized(0, new byte[] { })]
		public string threadId;

		[Serialized(1, new byte[] { })]
		public long mostRecentWhenAllDone;

		[Serialized(2, new byte[] { })]
		public string mostRecentMessageId;
	}
}
