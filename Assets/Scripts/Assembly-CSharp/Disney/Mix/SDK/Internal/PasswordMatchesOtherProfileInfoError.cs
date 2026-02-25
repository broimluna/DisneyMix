namespace Disney.Mix.SDK.Internal
{
	public class PasswordMatchesOtherProfileInfoError : AbstractInvalidProfileItemError, IInvalidProfileItemError, IPasswordMatchesOtherProfileInfoError
	{
		public PasswordMatchesOtherProfileInfoError(string description)
			: base(description)
		{
		}
	}
}
