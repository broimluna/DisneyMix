namespace Disney.Mix.SDK.Internal
{
	internal class GetGameStateMessageForbiddenResult : IGetGameStateMessageForbiddenResult, IGetGameStateMessageResult
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
