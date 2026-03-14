using Disney.LaunchPad.Packages.FiniteStateMachine;
using UnityEngine;

namespace Mix.Games.Tray.Drop.GameLogic
{
	public class PauseDropGameState : DropGameState
	{
		private float timer;

		private void Awake()
		{
			base.State.EventDispatcher.AddListener<StateEnterEvent>(OnStateEnter);
			base.State.EventDispatcher.AddListener<StateUpdateEvent>(OnStateUpdate);
			base.State.EventDispatcher.AddListener<StateExitEvent>(OnStateExit);
		}

		private bool OnStateEnter(StateEnterEvent evnt)
		{
			if (!base.Game.UIManager.IsUILoaded(base.Game.PauseUI))
			{
				base.Game.UIManager.LoadUI(base.Game.PauseUI);
			}
			base.Game.PausePlaying();
			timer = 0f;
			return false;
		}

		private bool OnStateUpdate(StateUpdateEvent evnt)
		{
			timer += Time.deltaTime;
			if (timer > base.Game.PauseResumeTime)
			{
				base.Game.GameEventDispatcher.DispatchEvent(new GameResumeEvent());
			}
			return false;
		}

		private bool OnStateExit(StateExitEvent evnt)
		{
			base.Game.UIManager.UnloadUI(base.Game.PauseUI);
			base.Game.ResumePlaying();
			return false;
		}
	}
}
