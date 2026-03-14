namespace Disney.Mix.SDK.Internal
{
	public interface IInternalPhotoMessage : IInternalChatMessage, IChatMessage, IPhotoMessage
	{
		string PhotoId { get; }

		void SendComplete(string photoId, long id, long timeSent, long sequenceNumber);
	}
}
