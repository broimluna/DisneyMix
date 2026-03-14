using System;
using Disney.Mix.SDK;

namespace Mix.Session.Local.Messages
{
	public class LocalVideoFlavor : IVideoFlavor
	{
		private string videoPath;

		public int Height { get; private set; }

		public int Width { get; private set; }

		public int BitRate { get; private set; }

		public VideoFormat Format { get; private set; }

		public LocalVideoFlavor(string filePath, VideoFormat format, int bitrate, int duration, int width, int height)
		{
			Format = format;
			BitRate = bitrate;
			Width = width;
			Height = height;
			videoPath = filePath;
		}

		public void GetSource(Action<IGetVideoSourceResult> callback)
		{
			callback(new LocalGetVideoFlavorFileResult(videoPath));
		}
	}
}
