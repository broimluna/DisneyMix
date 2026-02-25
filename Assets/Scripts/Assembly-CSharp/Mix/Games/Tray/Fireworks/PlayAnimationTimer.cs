using UnityEngine;

namespace Mix.Games.Tray.Fireworks
{
	public class PlayAnimationTimer : MonoBehaviour
	{
		public Animator animController;

		public float timeBeforePlay = 1f;

		public string animationToPlay;

		public void StartCountdownToExit()
		{
			Invoke("PlayAniamtion", timeBeforePlay);
		}

		private void PlayAniamtion()
		{
			if (animController != null && animationToPlay != null)
			{
				animController.Play(animationToPlay);
			}
		}
	}
}
