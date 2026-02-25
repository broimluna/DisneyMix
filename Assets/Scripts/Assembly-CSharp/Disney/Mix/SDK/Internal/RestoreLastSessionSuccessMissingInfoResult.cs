namespace Disney.Mix.SDK.Internal
{
	internal class RestoreLastSessionSuccessMissingInfoResult : IRestoreLastSessionResult, IRestoreLastSessionSuccessMissingInfoResult
	{
		public bool Success { get; private set; }

		public ISession Session { get; private set; }

		public RestoreLastSessionSuccessMissingInfoResult()
		{
			Success = true;
			Session = null;
		}

		public RestoreLastSessionSuccessMissingInfoResult(bool success, ISession session)
		{
			Success = success;
			Session = session;
		}
	}
}
