using Disney.Mix.SDK;

namespace Mix.FakeFriend.Results
{
	public class FakeResendChatMessageResult : IResendChatMessageResult
	{
		public bool Success { get; set; }

		public bool IsModerated { get; set; }

		public FakeResendChatMessageResult(bool success)
		{
			Success = success;
			IsModerated = false;
		}
	}
}
