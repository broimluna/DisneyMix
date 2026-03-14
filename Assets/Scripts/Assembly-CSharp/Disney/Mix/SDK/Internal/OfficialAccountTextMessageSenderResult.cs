namespace Disney.Mix.SDK.Internal
{
	public class OfficialAccountTextMessageSenderResult : IOfficialAccountTextMessageSenderResult
	{
		public bool Success { get; private set; }

		public OfficialAccountTextMessageSenderResult(bool success)
		{
			Success = success;
		}
	}
}
