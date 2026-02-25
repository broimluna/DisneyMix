namespace Disney.Mix.SDK.Internal
{
	public class PasswordSizeError : AbstractInvalidProfileItemError, IInvalidProfileItemError, IPasswordSizeError
	{
		public PasswordSizeError(string description)
			: base(description)
		{
		}
	}
}
