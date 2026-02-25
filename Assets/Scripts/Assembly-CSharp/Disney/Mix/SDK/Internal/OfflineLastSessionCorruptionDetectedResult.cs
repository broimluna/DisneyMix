namespace Disney.Mix.SDK.Internal
{
	public class OfflineLastSessionCorruptionDetectedResult : IInternalOfflineLastSessionResult, IOfflineLastSessionCorruptionDetectedResult, IOfflineLastSessionResult
	{
		public bool Success { get; private set; }

		public ISession Session
		{
			get
			{
				return InternalSession;
			}
		}

		public IInternalSession InternalSession { get; private set; }
	}
}
