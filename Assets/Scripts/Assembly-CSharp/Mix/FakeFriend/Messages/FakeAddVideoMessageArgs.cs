using Disney.Mix.SDK;

namespace Mix.FakeFriend.Messages
{
	public class FakeAddVideoMessageArgs : AbstractChatThreadVideoMessageAddedEventArgs
	{
		public override IVideoMessage Message { get; protected set; }

		public FakeAddVideoMessageArgs(IVideoMessage message)
		{
			Message = message;
		}
	}
}
