using Disney.Mix.SDK;

namespace Mix.FakeFriend.Results
{
	public class FakeGetGameStateMessageResult : IGetGameStateMessageResult
	{
		public bool Success { get; set; }

		public FakeGetGameStateMessageResult(bool success)
		{
			Success = success;
		}
	}
}
