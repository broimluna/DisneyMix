namespace Disney.Mix.SDK.Internal.MixDomain
{
	public class UpdateChatThreadGroupStatusNotification : BaseNotification
	{
		public long? ChatThreadId;

		public bool? IsGroup;
	}
}
