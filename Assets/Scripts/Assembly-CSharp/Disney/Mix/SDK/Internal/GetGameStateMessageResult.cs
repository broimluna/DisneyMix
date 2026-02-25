namespace Disney.Mix.SDK.Internal
{
	internal class GetGameStateMessageResult : IGetGameStateMessageResult
	{
		public bool Success { get; private set; }

		public GetGameStateMessageResult(bool success)
		{
			Success = success;
		}
	}
}
