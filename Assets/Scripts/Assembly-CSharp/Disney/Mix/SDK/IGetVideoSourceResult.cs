namespace Disney.Mix.SDK
{
	public interface IGetVideoSourceResult
	{
		bool Success { get; }

		IVideoSource Source { get; }
	}
}
