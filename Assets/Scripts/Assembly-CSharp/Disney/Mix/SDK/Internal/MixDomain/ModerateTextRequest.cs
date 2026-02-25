namespace Disney.Mix.SDK.Internal.MixDomain
{
	public class ModerateTextRequest : BaseUserRequest
	{
		public string Text;

		public long? ChatThreadId;

		public string ModerationPolicy;
	}
}
