namespace Disney.Mix.SDK.Internal
{
	public class OfficialAccountVideoMessageSenderResult : IOfficialAccountVideoMessageSenderResult
	{
		public bool Success { get; private set; }

		public OfficialAccountVideoMessageSenderResult(bool success)
		{
			Success = success;
		}
	}
}
