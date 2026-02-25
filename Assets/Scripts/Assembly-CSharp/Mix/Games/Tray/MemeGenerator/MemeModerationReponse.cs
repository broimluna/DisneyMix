namespace Mix.Games.Tray.MemeGenerator
{
	public class MemeModerationReponse
	{
		private bool topModerationResult;

		private bool bottomModerationResult;

		private bool topResponseReceived;

		private bool bottomResponseReceived;

		public bool AllResultsReceived
		{
			get
			{
				return topResponseReceived && bottomResponseReceived;
			}
		}

		public bool PassedModeration
		{
			get
			{
				return !topModerationResult && !bottomModerationResult;
			}
		}

		public bool TopModerationResult
		{
			set
			{
				topModerationResult = value;
				topResponseReceived = true;
			}
		}

		public bool BottomModerationResult
		{
			set
			{
				bottomModerationResult = value;
				bottomResponseReceived = true;
			}
		}
	}
}
