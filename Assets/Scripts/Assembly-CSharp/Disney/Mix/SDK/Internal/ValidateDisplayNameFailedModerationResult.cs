namespace Disney.Mix.SDK.Internal
{
	public class ValidateDisplayNameFailedModerationResult : IValidateDisplayNameFailedModerationResult, IValidateDisplayNameResult
	{
		public bool Success { get; private set; }

		public ValidateDisplayNameFailedModerationResult(bool success)
		{
			Success = success;
		}
	}
}
