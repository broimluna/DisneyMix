using Disney.LaunchPad.Packages.FiniteStateMachine;
using UnityEngine;

namespace Mix.Games.Tray.TemplateGame
{
	public class BaseGameState : MonoBehaviour
	{
		protected internal TemplateGameGame mGame;

		protected State mState;

		protected virtual void Awake()
		{
			mGame = base.transform.GetComponentInParent<TemplateGameGame>();
			mState = GetComponent<State>();
		}
	}
}
