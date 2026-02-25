using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	public class DropPlayerStandingBehaviour : DropPlayerBehaviour
	{
		public override void OnStateEnterOnce(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			base.Player.ShouldLerpToStandingPosition = true;
		}

		public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			base.Player.ShouldLerpToStandingPosition = true;
		}
	}
}
