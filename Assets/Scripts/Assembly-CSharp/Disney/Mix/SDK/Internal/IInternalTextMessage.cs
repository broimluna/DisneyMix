namespace Disney.Mix.SDK.Internal
{
	public interface IInternalTextMessage : IInternalChatMessage, IChatMessage, ITextMessage
	{
		new string Text { get; set; }
	}
}
