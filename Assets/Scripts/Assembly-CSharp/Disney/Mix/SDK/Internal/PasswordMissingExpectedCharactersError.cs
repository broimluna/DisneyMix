namespace Disney.Mix.SDK.Internal
{
	public class PasswordMissingExpectedCharactersError : AbstractInvalidProfileItemError, IInvalidProfileItemError, IPasswordMissingExpectedCharactersError
	{
		public PasswordMissingExpectedCharactersError(string description)
			: base(description)
		{
		}
	}
}
