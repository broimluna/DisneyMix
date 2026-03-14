namespace Disney.Mix.SDK
{
	public interface ISendTextMessageResult
	{
		bool Success { get; }

		bool IsModerated { get; }
	}
}
