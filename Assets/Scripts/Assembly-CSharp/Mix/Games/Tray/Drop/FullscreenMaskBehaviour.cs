using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	public class FullscreenMaskBehaviour : PreservedStateMachineBehaviour
	{
		public enum FullscreenMaskState
		{
			OPEN = 0,
			CLOSED = 1
		}

		public FullscreenMaskState MaskState;

		public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			if (MaskState == FullscreenMaskState.CLOSED)
			{
				if (CommonUI.OnMaskClosed != null)
				{
					CommonUI.OnMaskClosed();
				}
			}
			else if (MaskState == FullscreenMaskState.OPEN && CommonUI.OnMaskOpened != null)
			{
				CommonUI.OnMaskOpened();
			}
		}
	}
}
