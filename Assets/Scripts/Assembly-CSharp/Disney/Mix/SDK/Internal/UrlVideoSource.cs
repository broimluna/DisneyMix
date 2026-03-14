namespace Disney.Mix.SDK.Internal
{
	internal class UrlVideoSource : IUrlVideoSource, IVideoSource
	{
		public string Url { get; private set; }

		public UrlVideoSource(string url)
		{
			Url = url;
		}
	}
}
