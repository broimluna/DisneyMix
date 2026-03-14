using DG.Tweening;
using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	public class DropPlayerJumpAnticipationBehaviour : DropPlayerBehaviour
	{
		public float PlayerTurnTime;

		private int moveXAnimatorId;

		private int moveYAnimatorId;

		private void OnEnable()
		{
			moveXAnimatorId = Animator.StringToHash("MoveX");
			moveYAnimatorId = Animator.StringToHash("MoveY");
		}

		public override void OnStateEnterOnce(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			Vector3 vector = new Vector3(animator.GetInteger(moveXAnimatorId), 0f, animator.GetInteger(moveYAnimatorId));
			base.Player.transform.DOLookAt(base.Player.transform.position + vector, PlayerTurnTime).SetEase(Ease.InOutQuad).SetId(base.Player);
		}
	}
}
