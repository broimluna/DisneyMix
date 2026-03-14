namespace Disney.Mix.SDK.Internal.MixDomain
{
	public class GameEventChatMessage : BaseChatMessage
	{
		public long? GameStateMessageId;

		public string GameName;

		public string Payload;
	}
}
