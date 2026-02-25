using System;

namespace Disney.Mix.SDK
{
	public interface IVideoFlavor
	{
		int BitRate { get; }

		VideoFormat Format { get; }

		int Height { get; }

		int Width { get; }

		void GetSource(Action<IGetVideoSourceResult> callback);
	}
}
