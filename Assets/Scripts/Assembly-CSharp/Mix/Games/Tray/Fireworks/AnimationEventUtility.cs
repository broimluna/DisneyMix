using UnityEngine;

namespace Mix.Games.Tray.Fireworks
{
	public class AnimationEventUtility : MonoBehaviour
	{
		public GameObject TargetObject;

		public Animator TargetAnimController;

		public void DisableTargetObject()
		{
			if (TargetObject != null)
			{
				TargetObject.SetActive(false);
			}
		}

		public void DestroyTargetObject()
		{
			if (TargetObject != null)
			{
				Object.Destroy(TargetObject);
			}
		}

		public void PlayAnimOnTarget(string animation)
		{
			if (TargetAnimController != null)
			{
				TargetAnimController.Play(animation);
			}
		}
	}
}
