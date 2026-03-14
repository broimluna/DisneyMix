using Disney.Mix.SDK;

namespace Mix.FakeFriend.Messages
{
	public class FakeAddGagMessageArgs : AbstractChatThreadGagMessageAddedEventArgs
	{
		public override IGagMessage Message { get; protected set; }

		public FakeAddGagMessageArgs(IGagMessage message)
		{
			Message = message;
		}
	}
}
