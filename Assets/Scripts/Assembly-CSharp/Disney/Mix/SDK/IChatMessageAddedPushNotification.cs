namespace Disney.Mix.SDK
{
	public interface IChatMessageAddedPushNotification : IPushNotification
	{
		string ChatThreadId { get; }

		string MessageId { get; }
	}
}
