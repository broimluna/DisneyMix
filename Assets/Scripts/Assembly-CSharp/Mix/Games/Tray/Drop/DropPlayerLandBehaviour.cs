using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	public class DropPlayerLandBehaviour : DropPlayerBehaviour
	{
		private int stuckInSnowAnimatorId;

		private void OnEnable()
		{
			stuckInSnowAnimatorId = Animator.StringToHash("StuckInSnow");
		}

		public override void OnStateEnterOnce(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			if (!animator.GetBool(stuckInSnowAnimatorId))
			{
				DropAudio.PlaySound("SFX/Gameplay/Player/Land");
			}
		}
	}
}
