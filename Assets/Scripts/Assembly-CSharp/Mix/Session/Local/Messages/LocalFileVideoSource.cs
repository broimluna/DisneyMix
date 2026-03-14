using Disney.Mix.SDK;

namespace Mix.Session.Local.Messages
{
	public class LocalFileVideoSource : IFileVideoSource, IVideoSource
	{
		public string Path { get; private set; }

		public LocalFileVideoSource(string filePath)
		{
			Path = filePath;
		}

		public void FinishPlaying()
		{
		}
	}
}
