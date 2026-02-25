using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal.MixDomain
{
	public class VideoChatMessage : BaseChatMessage
	{
		public string UgcMediaId;

		public string VideoId;

		public string Caption;

		public PhotoFlavor Thumbnail;

		public int? Duration;

		public List<VideoFlavor> VideoFlavors;
	}
}
