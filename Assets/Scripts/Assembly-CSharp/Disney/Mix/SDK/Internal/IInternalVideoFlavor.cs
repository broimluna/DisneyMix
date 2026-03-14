namespace Disney.Mix.SDK.Internal
{
	public interface IInternalVideoFlavor : IVideoFlavor
	{
		string VideoId { get; }

		string VideoFlavorId { get; }
	}
}
