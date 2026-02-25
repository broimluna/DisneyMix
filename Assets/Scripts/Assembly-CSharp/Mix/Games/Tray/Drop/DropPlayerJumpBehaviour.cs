using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	public class DropPlayerJumpBehaviour : DropPlayerBehaviour
	{
		private int destinationXAnimatorId;

		private int destinationYAnimatorId;

		private void OnEnable()
		{
			destinationXAnimatorId = Animator.StringToHash("DestinationX");
			destinationYAnimatorId = Animator.StringToHash("DestinationY");
		}

		public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			Coordinate2D coords = new Coordinate2D(animator.GetInteger(destinationXAnimatorId), animator.GetInteger(destinationYAnimatorId));
			Platform platformAtCoords = base.Player.Game.LevelGenerator.GetPlatformAtCoords(coords);
			base.Player.ArriveAtPlatform(platformAtCoords);
		}
	}
}
