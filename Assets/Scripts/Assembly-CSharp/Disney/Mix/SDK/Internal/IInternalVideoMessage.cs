namespace Disney.Mix.SDK.Internal
{
	public interface IInternalVideoMessage : IInternalChatMessage, IChatMessage, IVideoMessage
	{
		string VideoId { get; }

		void SendComplete(string videoId, long id, long timeSent, long sequenceNumber);
	}
}
