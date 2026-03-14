using Disney.Mix.SDK;

namespace Mix.FakeFriend.Results
{
	public class FakeSendGameStateMessageResult : ISendGameStateMessageResult
	{
		public bool Success { get; set; }

		public FakeSendGameStateMessageResult(bool success)
		{
			Success = success;
		}
	}
}
