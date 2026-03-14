namespace Mix.Games.Session
{
	public interface IGameSessionSettings
	{
		int GetScreenHeight();

		int GetScreenWidth();

		int GetStatusBarHeight();

		float GetHeightScale();

		float GetWidthScale();
	}
}
