using DeviceDB;

namespace Mix.DeviceDb
{
	[Serialized(60, new byte[] { })]
	public class OfflineQueueDocument : AbstractDocument
	{
		[Serialized(0, new byte[] { })]
		[Indexed]
		public long queueIndex;

		[Serialized(1, new byte[] { })]
		public string type;

		[Serialized(2, new byte[] { })]
		public string data;
	}
}
