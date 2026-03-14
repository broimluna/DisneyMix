namespace Disney.Mix.SDK.Internal.MixDomain
{
	public class RemoveChatThreadMembershipRequest : BaseUserRequest
	{
		public long? ChatThreadId;

		public string RemovedUserId;
	}
}
