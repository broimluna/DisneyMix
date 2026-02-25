namespace Disney.Mix.SDK.Internal
{
	internal class UpdateGameStateMessageResult : IUpdateGameStateMessageResult
	{
		public bool Success { get; private set; }

		public string Result { get; private set; }

		public UpdateGameStateMessageResult(bool success, string result)
		{
			Success = success;
			Result = result;
		}
	}
}
