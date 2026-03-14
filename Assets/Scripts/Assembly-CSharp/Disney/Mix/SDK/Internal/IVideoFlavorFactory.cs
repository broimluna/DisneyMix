namespace Disney.Mix.SDK.Internal
{
	public interface IVideoFlavorFactory
	{
		IVideoFlavor Create(string videoId, string videoFlavorId, int bitRate, VideoFormat format, int width, int height, IMixWebCallFactory mixWebCallFactory);
	}
}
