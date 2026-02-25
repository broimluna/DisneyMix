namespace Disney.Mix.SDK.Internal
{
	public interface IInternalGameEventMessage : IInternalChatMessage, IChatMessage, IGameEventMessage
	{
		long GameStateMessageId { get; }

		void UpdateGameStateMessage(IInternalGameStateMessage gameStateMessage);
	}
}
