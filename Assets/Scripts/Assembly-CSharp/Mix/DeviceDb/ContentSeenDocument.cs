using DeviceDB;

namespace Mix.DeviceDb
{
	[Serialized(61, new byte[] { })]
	public class ContentSeenDocument : AbstractDocument
	{
		[Serialized(0, new byte[] { })]
		[Indexed]
		public string contentId;
	}
}
