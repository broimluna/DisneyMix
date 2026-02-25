namespace Disney.Mix.SDK
{
	public interface IGagMessage : IChatMessage
	{
		string ContentId { get; }

		string TargetUserId { get; }
	}
}
