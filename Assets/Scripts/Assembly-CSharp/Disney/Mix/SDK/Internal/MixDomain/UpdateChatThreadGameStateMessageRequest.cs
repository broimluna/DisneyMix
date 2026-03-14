namespace Disney.Mix.SDK.Internal.MixDomain
{
	public class UpdateChatThreadGameStateMessageRequest : BaseChatThreadAddMessageRequest
	{
		public long? GameStateMessageId;

		public string Payload;
	}
}
