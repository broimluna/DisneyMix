namespace Disney.Mix.SDK.Internal
{
	public class PasswordLikePhoneNumberError : AbstractInvalidProfileItemError, IInvalidProfileItemError, IPasswordLikePhoneNumberError
	{
		public PasswordLikePhoneNumberError(string description)
			: base(description)
		{
		}
	}
}
