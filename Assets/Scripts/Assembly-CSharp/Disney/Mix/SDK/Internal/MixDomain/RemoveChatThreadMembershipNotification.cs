namespace Disney.Mix.SDK.Internal.MixDomain
{
	public class RemoveChatThreadMembershipNotification : BaseNotification
	{
		public long? ChatThreadId;

		public string MemberUserId;
	}
}
