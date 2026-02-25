namespace Disney.Mix.SDK.Internal
{
	internal class RemoveUserNicknameResult : IRemoveUserNicknameResult
	{
		public bool Success { get; private set; }

		public RemoveUserNicknameResult(bool success)
		{
			Success = success;
		}
	}
}
