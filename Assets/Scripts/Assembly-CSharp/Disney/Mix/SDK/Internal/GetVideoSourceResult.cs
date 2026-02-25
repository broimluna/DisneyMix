namespace Disney.Mix.SDK.Internal
{
	internal class GetVideoSourceResult : IGetVideoSourceResult
	{
		public bool Success { get; private set; }

		public IVideoSource Source { get; private set; }

		public GetVideoSourceResult(IVideoSource source, bool success)
		{
			Source = source;
			Success = success;
		}
	}
}
