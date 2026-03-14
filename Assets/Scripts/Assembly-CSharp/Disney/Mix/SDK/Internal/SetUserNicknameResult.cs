namespace Disney.Mix.SDK.Internal
{
	internal class SetUserNicknameResult : ISetUserNicknameResult
	{
		public bool Success { get; private set; }

		public SetUserNicknameResult(bool success)
		{
			Success = success;
		}
	}
}
