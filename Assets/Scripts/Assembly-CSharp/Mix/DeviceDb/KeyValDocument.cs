using DeviceDB;

namespace Mix.DeviceDb
{
	[Serialized(56, new byte[] { })]
	public class KeyValDocument : AbstractDocument
	{
		[Indexed]
		[Serialized(0, new byte[] { })]
		public string key;

		[Serialized(1, new byte[] { })]
		[Indexed]
		public string value;
	}
}
