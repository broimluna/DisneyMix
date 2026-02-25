using Disney.Mix.SDK;

namespace Mix.FakeFriend.Messages
{
	public class FakeAddPhotoMessageArgs : AbstractChatThreadPhotoMessageAddedEventArgs
	{
		public override IPhotoMessage Message { get; protected set; }

		public FakeAddPhotoMessageArgs(IPhotoMessage message)
		{
			Message = message;
		}
	}
}
