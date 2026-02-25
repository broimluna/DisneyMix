namespace Disney.Mix.SDK
{
	public interface IResendChatMessageResult
	{
		bool Success { get; }

		bool IsModerated { get; }
	}
}
