using UnityEngine;

namespace Mix.Games.Tray.Runner
{
	public class SpringLaunch : MonoBehaviour
	{
		public float launchForce = 20f;

		public Animator springAnimator;

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.name.Contains("avatarPrefab"))
			{
				other.gameObject.GetComponent<RunnerController>().Launch(launchForce);
				springAnimator.Play("launch");
			}
		}
	}
}
