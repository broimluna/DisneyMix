namespace Disney.Mix.SDK.Internal
{
	public class ValidateDisplayNameResult : IValidateDisplayNameResult
	{
		public bool Success { get; private set; }

		public ValidateDisplayNameResult(bool success)
		{
			Success = success;
		}
	}
}
