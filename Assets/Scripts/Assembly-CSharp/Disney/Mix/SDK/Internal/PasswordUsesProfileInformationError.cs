namespace Disney.Mix.SDK.Internal
{
	public class PasswordUsesProfileInformationError : AbstractInvalidProfileItemError, IInvalidProfileItemError, IPasswordUsesProfileInformationError
	{
		public PasswordUsesProfileInformationError(string description)
			: base(description)
		{
		}
	}
}
