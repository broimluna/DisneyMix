using Disney.Mix.SDK;

namespace Mix.FakeFriend.Results
{
	public class FakeSendTextMessageResult : ISendTextMessageResult
	{
		public bool Success { get; set; }

		public bool IsModerated { get; set; }

		public FakeSendTextMessageResult(bool success)
		{
			Success = success;
			IsModerated = false;
		}
	}
}
