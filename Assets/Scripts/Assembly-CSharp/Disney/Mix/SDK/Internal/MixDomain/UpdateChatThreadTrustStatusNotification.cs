namespace Disney.Mix.SDK.Internal.MixDomain
{
	public class UpdateChatThreadTrustStatusNotification : BaseNotification
	{
		public long? ChatThreadId;

		public bool? IsTrusted;
	}
}
