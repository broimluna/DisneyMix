using System;
using Disney.Mix.SDK.Internal.MixDomain;

namespace Disney.Mix.SDK.Internal
{
	internal class VideoFlavor : IInternalVideoFlavor, IVideoFlavor
	{
		private readonly IVideoFlavorUrlGetter videoFlavorUrlGetter;

		private readonly IMixWebCallFactory mixWebCallFactory;

		public string VideoId { get; private set; }

		public string VideoFlavorId { get; private set; }

		public int BitRate { get; private set; }

		public VideoFormat Format { get; private set; }

		public int Height { get; private set; }

		public int Width { get; private set; }

		public VideoFlavor(string videoId, string videoFlavorId, int bitRate, VideoFormat format, int width, int height, IVideoFlavorUrlGetter videoFlavorUrlGetter, IMixWebCallFactory mixWebCallFactory)
		{
			VideoId = videoId;
			VideoFlavorId = videoFlavorId;
			BitRate = bitRate;
			Format = format;
			Width = width;
			Height = height;
			this.videoFlavorUrlGetter = videoFlavorUrlGetter;
			this.mixWebCallFactory = mixWebCallFactory;
		}

		public void GetSource(Action<IGetVideoSourceResult> callback)
		{
			videoFlavorUrlGetter.Get(VideoId, VideoFlavorId, mixWebCallFactory, delegate(GetVideoUrlResponse response)
			{
				callback(new GetVideoSourceResult(new UrlVideoSource(response.Url), true));
			}, delegate
			{
				callback(new GetVideoSourceResult(new UrlVideoSource(null), false));
			});
		}
	}
}
