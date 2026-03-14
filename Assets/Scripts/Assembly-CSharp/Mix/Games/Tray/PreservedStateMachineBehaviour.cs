using UnityEngine;

namespace Mix.Games.Tray
{
	public class PreservedStateMachineBehaviour : StateMachineBehaviour
	{
		public sealed override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			if (!animator.GetBool("_Restored"))
			{
				OnStateEnterOnce(animator, stateInfo, layerIndex);
			}
			else
			{
				OnStateEnterReturning(animator, stateInfo, layerIndex);
				animator.SetBool("_Restored", false);
			}
			OnStateEnterAll(animator, stateInfo, layerIndex);
		}

		public virtual void OnStateEnterOnce(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
		}

		public virtual void OnStateEnterReturning(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
		}

		public virtual void OnStateEnterAll(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
		}
	}
}
