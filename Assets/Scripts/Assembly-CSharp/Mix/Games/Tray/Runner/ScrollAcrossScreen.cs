using UnityEngine;

namespace Mix.Games.Tray.Runner
{
	public class ScrollAcrossScreen : MonoBehaviour
	{
		public float speed = 1f;

		public MainRunnerGame main;

		private void Update()
		{
			if (main.mode == GameMode.PLAY)
			{
				Vector3 localPosition = base.transform.localPosition;
				localPosition.x += speed * Time.deltaTime;
				base.transform.localPosition = localPosition;
			}
		}
	}
}
