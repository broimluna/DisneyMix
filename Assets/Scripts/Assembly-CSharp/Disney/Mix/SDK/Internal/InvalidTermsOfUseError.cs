namespace Disney.Mix.SDK.Internal
{
	public class InvalidTermsOfUseError : AbstractInvalidProfileItemError, IInvalidProfileItemError, IInvalidTermsOfUseError
	{
		public InvalidTermsOfUseError(string description)
			: base(description)
		{
		}
	}
}
