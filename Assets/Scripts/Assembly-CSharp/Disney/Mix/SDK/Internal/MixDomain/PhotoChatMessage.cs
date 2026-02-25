using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal.MixDomain
{
	public class PhotoChatMessage : BaseChatMessage
	{
		public string UgcMediaId;

		public string PhotoId;

		public string Caption;

		public List<PhotoFlavor> PhotoFlavors;
	}
}
