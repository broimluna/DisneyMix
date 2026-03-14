namespace Mix.Games
{
	public interface IMixGameTimer
	{
		void GameTimerStart();

		void GameTimerProgress(float aTimeRemaining);

		void GameTimerComplete();
	}
}
