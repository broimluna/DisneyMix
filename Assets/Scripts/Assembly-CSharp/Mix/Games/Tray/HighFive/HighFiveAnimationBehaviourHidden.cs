using UnityEngine;

namespace Mix.Games.Tray.HighFive
{
	public class HighFiveAnimationBehaviourHidden : PreservedStateMachineBehaviour
	{
		private HighFiveTargetAnimatorHelper mHelper;

		public override void OnStateEnterAll(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			if (mHelper == null)
			{
				mHelper = animator.GetComponent<HighFiveTargetAnimatorHelper>();
			}
			mHelper.EndAnimation();
		}
	}
}
