using System;

namespace Disney.Mix.SDK
{
	public interface IPhotoFlavor
	{
		PhotoEncoding Encoding { get; }

		int Height { get; }

		int Width { get; }

		void GetFile(Action<IGetPhotoFlavorFileResult> callback);
	}
}
