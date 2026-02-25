namespace Disney.Mix.SDK.Internal
{
	internal class LoginSecurityUpdateResult : ILoginResult, ILoginSecurityUpdateResult
	{
		public bool Success { get; private set; }

		public ISession Session { get; private set; }

		public LoginSecurityUpdateResult()
		{
			Success = false;
			Session = null;
		}

		public LoginSecurityUpdateResult(bool success, ISession session)
		{
			Success = success;
			Session = session;
		}
	}
}
