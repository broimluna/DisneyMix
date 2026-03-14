using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal
{
	public class PhotoMessageDocumentPayload
	{
		public string PhotoId;

		public byte[] Key;

		public string Caption;

		public List<PhotoFlavorDocumentPayload> PhotoFlavors;
	}
}
