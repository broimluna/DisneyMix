using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	public class DropPlayerInactiveBehaviour : DropPlayerBehaviour
	{
		public override void OnStateEnterAll(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			base.Player.PlayerMesh.SetActive(false);
			base.Player.PlayerCollider.enabled = false;
		}

		public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			base.Player.PlayerMesh.SetActive(true);
			base.Player.PlayerCollider.enabled = true;
		}
	}
}
