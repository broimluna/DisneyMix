using Disney.Mix.SDK;

namespace Mix.FakeFriend.Messages
{
	public class FakeAddTextMessageArgs : AbstractChatThreadTextMessageAddedEventArgs
	{
		public override ITextMessage Message { get; protected set; }

		public FakeAddTextMessageArgs(ITextMessage message)
		{
			Message = message;
		}
	}
}
