namespace Mix.Games.Session
{
	public interface IGameModerationResult
	{
		void OnModerationResult(bool aIsModerated, string aModeratedText, object aUserData);

		void OnModerationError(object aUserData);
	}
}
