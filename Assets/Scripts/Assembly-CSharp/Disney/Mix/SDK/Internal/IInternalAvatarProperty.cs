namespace Disney.Mix.SDK.Internal
{
	public interface IInternalAvatarProperty : IAvatarProperty
	{
		new string SelectionKey { get; set; }

		new int TintIndex { get; set; }

		new double XOffset { get; set; }

		new double YOffset { get; set; }
	}
}
