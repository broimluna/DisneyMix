namespace Mix.Games.Data
{
	public interface IEntitlementGameData
	{
		string GetName();

		string GetHd();

		string GetUid();

		string GetLogo();

		string GetPost();

		string GetResult();

		string GetPauseImage();

		string GetThumbImage();

		string GetAttempts();

		string GetBaseUrl();

		string GetDuration();

		int GetPostHeight();

		int GetLogoHeight();

		int GetResultsHeight();

		int GetThumbHeight();
	}
}
