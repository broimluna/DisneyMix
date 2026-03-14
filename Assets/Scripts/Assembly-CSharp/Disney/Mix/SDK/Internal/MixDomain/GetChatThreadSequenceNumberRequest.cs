namespace Disney.Mix.SDK.Internal.MixDomain
{
	public class GetChatThreadSequenceNumberRequest : BaseUserRequest
	{
		public long? ChatThreadId;

		public long? StartMessageTimestamp;
	}
}
