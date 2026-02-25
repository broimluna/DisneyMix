namespace Disney.Mix.SDK.Internal
{
	public class ValidateDisplayNameExistsResult : IValidateDisplayNameExistsResult, IValidateDisplayNameResult
	{
		public bool Success { get; private set; }

		public ValidateDisplayNameExistsResult(bool success)
		{
			Success = success;
		}
	}
}
