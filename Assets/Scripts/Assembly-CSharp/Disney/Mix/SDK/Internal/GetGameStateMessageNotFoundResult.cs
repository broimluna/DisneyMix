namespace Disney.Mix.SDK.Internal
{
	internal class GetGameStateMessageNotFoundResult : IGetGameStateMessageNotFoundResult, IGetGameStateMessageResult
	{
		public bool Success
		{
			get
			{
				return false;
			}
		}
	}
}
