using Disney.Mix.SDK;

namespace Mix.FakeFriend.Messages
{
	public class FakeAddStickerMessageArgs : AbstractChatThreadStickerMessageAddedEventArgs
	{
		public override IStickerMessage Message { get; protected set; }

		public FakeAddStickerMessageArgs(IStickerMessage message)
		{
			Message = message;
		}
	}
}
