namespace Disney.Mix.SDK.Internal
{
	public interface IInternalGagMessage : IInternalChatMessage, IChatMessage, IGagMessage
	{
		void SendComplete(string contentId, string targetUserId, long id, long timeSent, long sequenceNumber);
	}
}
