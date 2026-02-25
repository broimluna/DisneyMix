using Disney.Mix.SDK;

namespace Mix.FakeFriend.Results
{
	public class FakeSendGagMessageResult : ISendGagMessageResult
	{
		public bool Success { get; set; }

		public FakeSendGagMessageResult(bool success)
		{
			Success = success;
		}
	}
}
