using Disney.Mix.SDK;

namespace Mix.FakeFriend.Results
{
	public class FakeTextModerationResult : ITextModerationResult
	{
		public bool Success { get; set; }

		public bool IsModerated { get; set; }

		public string ModeratedText { get; set; }

		public FakeTextModerationResult(bool success, bool isModerated, string text)
		{
			Success = success;
			IsModerated = isModerated;
			ModeratedText = text;
		}
	}
}
