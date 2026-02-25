using Disney.Mix.SDK;

namespace Mix.Session.Local.Messages
{
	public class LocalGetVideoFlavorFileResult : IGetVideoSourceResult
	{
		public bool Success { get; private set; }

		public IVideoSource Source { get; private set; }

		public LocalGetVideoFlavorFileResult(string filePath)
		{
			Success = true;
			Source = new LocalFileVideoSource(filePath);
		}
	}
}
