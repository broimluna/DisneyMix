using Mix.Games.Harness;

namespace Mix.Games.Tray
{
	public class MixStandaloneDebugSceneIndicator : DebugSceneIndicator
	{
		public MixStandaloneDebugSceneIndicator()
		{
			DebugSceneIndicator.GetGameManager = () => MonoSingleton<MixStandaloneGameManager>.Instance;
		}
	}
}
