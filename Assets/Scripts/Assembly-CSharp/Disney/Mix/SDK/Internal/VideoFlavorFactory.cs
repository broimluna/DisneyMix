namespace Disney.Mix.SDK.Internal
{
	public class VideoFlavorFactory : IVideoFlavorFactory
	{
		private readonly IVideoFlavorUrlGetter videoFlavorUrlGetter;

		public VideoFlavorFactory(IVideoFlavorUrlGetter videoFlavorUrlGetter)
		{
			this.videoFlavorUrlGetter = videoFlavorUrlGetter;
		}

		public IVideoFlavor Create(string videoId, string videoFlavorId, int bitRate, VideoFormat format, int width, int height, IMixWebCallFactory mixWebCallFactory)
		{
			return new VideoFlavor(videoId, videoFlavorId, bitRate, format, width, height, videoFlavorUrlGetter, mixWebCallFactory);
		}
	}
}
