namespace Disney.Mix.SDK.Internal
{
	internal class SetAvatarResult : ISetAvatarResult
	{
		public bool Success { get; private set; }

		public SetAvatarResult(bool success)
		{
			Success = success;
		}
	}
}
