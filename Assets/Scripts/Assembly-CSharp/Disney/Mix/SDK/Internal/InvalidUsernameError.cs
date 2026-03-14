namespace Disney.Mix.SDK.Internal
{
	public class InvalidUsernameError : AbstractInvalidProfileItemError, IInvalidProfileItemError, IInvalidUsernameError
	{
		public InvalidUsernameError(string description)
			: base(description)
		{
		}
	}
}
