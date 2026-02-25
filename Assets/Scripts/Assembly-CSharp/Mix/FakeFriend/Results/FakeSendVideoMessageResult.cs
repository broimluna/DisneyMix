using Disney.Mix.SDK;

namespace Mix.FakeFriend.Results
{
	public class FakeSendVideoMessageResult : ISendVideoMessageResult
	{
		public bool Success { get; set; }

		public FakeSendVideoMessageResult(bool success)
		{
			Success = success;
		}
	}
}
