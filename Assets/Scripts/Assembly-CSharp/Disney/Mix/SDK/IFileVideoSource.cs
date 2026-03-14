namespace Disney.Mix.SDK
{
	public interface IFileVideoSource : IVideoSource
	{
		string Path { get; }

		void FinishPlaying();
	}
}
