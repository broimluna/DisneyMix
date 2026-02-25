using DeviceDB;

namespace Mix.DeviceDb
{
	[Serialized(52, new byte[] { })]
	public class AvatarTextureDocument : AbstractDocument
	{
		[Serialized(0, new byte[] { })]
		[Indexed]
		public string textureHash;

		[Serialized(1, new byte[] { })]
		public string diffusePath;

		[Serialized(2, new byte[] { })]
		public string normalPath;

		[Serialized(3, new byte[] { })]
		public bool isHd;

		[Serialized(4, new byte[] { })]
		public float loadPercentage;
	}
}
