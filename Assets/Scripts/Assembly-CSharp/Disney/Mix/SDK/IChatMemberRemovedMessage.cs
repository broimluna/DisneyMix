namespace Disney.Mix.SDK
{
	public interface IChatMemberRemovedMessage : IChatMessage
	{
		string MemberId { get; }
	}
}
