using Mix.Games.Data;

namespace Mix.Games
{
	public interface IMixGame
	{
		void Initialize(MixGameData aData = null);

		void Pause();

		void Play();

		void Quit();

		void Resume();
	}
}
