using DeviceDB;

namespace Mix.DeviceDb
{
	[Serialized(50, new byte[] { })]
	public class AssetCacheDocument : AbstractDocument
	{
		[Serialized(0, new byte[] { })]
		[Indexed]
		public string sha;

		[Indexed]
		[Serialized(1, new byte[] { })]
		public string path;

		[Serialized(2, new byte[] { })]
		[Indexed]
		public string header;

		[Serialized(3, new byte[] { })]
		[Indexed]
		public long accessCount;

		[Serialized(4, new byte[] { })]
		[Indexed]
		public string insertTime;

		[Serialized(5, new byte[] { })]
		public int CpipeManifestVersion;
	}
}
