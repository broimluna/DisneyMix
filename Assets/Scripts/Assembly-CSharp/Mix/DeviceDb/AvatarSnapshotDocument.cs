using DeviceDB;

namespace Mix.DeviceDb
{
	[Serialized(51, new byte[] { })]
	public class AvatarSnapshotDocument : AbstractDocument
	{
		[Indexed]
		[Serialized(0, new byte[] { })]
		public string snapshotHash;

		[Serialized(1, new byte[] { })]
		public string path;

		[Serialized(2, new byte[] { })]
		public bool isHd;

		[Serialized(3, new byte[] { })]
		public bool hasNormals;

		[Serialized(4, new byte[] { })]
		public int size;

		[Serialized(5, new byte[] { })]
		public float loadPercentage;
	}
}
