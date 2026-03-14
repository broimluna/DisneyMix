namespace Disney.Mix.SDK.Internal.MixDomain
{
	public class AddChatThreadNicknameRequest : BaseUserRequest
	{
		public long? ChatThreadId;

		public string Nickname;
	}
}
