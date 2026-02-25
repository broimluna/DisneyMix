using System.Collections.Generic;

namespace Disney.Mix.SDK.Internal
{
	public class VideoMessageDocumentPayload
	{
		public string VideoId;

		public long Duration;

		public string Caption;

		public PhotoFlavorDocumentPayload Thumbnail;

		public List<VideoFlavorDocumentPayload> VideoFlavors;
	}
}
