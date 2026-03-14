using Disney.LaunchPad.Packages.FiniteStateMachine;
using UnityEngine;

namespace Mix.Games.Tray.Drop.GameLogic
{
	public class InitializeDropGameState : DropGameState
	{
		private void Awake()
		{
			base.State.EventDispatcher.AddListener<StateEnterEvent>(OnStateEnter);
		}

		private bool OnStateEnter(StateEnterEvent stateChange)
		{
			base.Game.CreatePlayer();
			base.Game.CreateGhost();
			base.Game.InitScoreboard();
			base.Game.FollowCamera.enabled = false;
			int num = 0;
			if (base.Game.IsFirstRound)
			{
				num = Random.Range(0, int.MaxValue);
			}
			else
			{
				DropData gameData = base.Game.GameController.GetGameData<DropData>();
				num = gameData.LevelGenerationSeed;
				base.Game.BestScore = 0;
				for (int i = 0; i < gameData.Responses.Count; i++)
				{
					base.Game.BestScore = Mathf.Max(gameData.Responses[i].Score, base.Game.BestScore);
				}
			}
			base.Game.LevelGenerator.Initialize(num);
			base.Game.ColumnGenerator.Initialize();
			base.Game.GameEventDispatcher.DispatchEvent(new GameInitializedEvent());
			return false;
		}
	}
}
