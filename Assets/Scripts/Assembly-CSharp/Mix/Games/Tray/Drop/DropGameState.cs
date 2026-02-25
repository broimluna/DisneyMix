using Disney.LaunchPad.Packages.FiniteStateMachine;
using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	[RequireComponent(typeof(State))]
	public class DropGameState : MonoBehaviour
	{
		private State state;

		private DropGame game;

		public State State
		{
			get
			{
				if (state == null)
				{
					state = base.gameObject.GetComponent<State>();
				}
				return state;
			}
		}

		public DropGame Game
		{
			get
			{
				if (game == null)
				{
					game = DropGame.Instance;
				}
				return game;
			}
		}
	}
}
