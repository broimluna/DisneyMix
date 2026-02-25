namespace Disney.Mix.SDK.Internal
{
	public class PasswordTooCommonError : AbstractInvalidProfileItemError, IInvalidProfileItemError, IPasswordTooCommonError
	{
		public PasswordTooCommonError(string description)
			: base(description)
		{
		}
	}
}
