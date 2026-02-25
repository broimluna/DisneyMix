using UnityEngine;

namespace Mix.Games.Tray.Fireworks
{
	public class FireworkDebugController : MonoBehaviour
	{
		public Firework launcher;

		private Vector3 l;

		private void Start()
		{
			if (launcher != null)
			{
				l = launcher.transform.localPosition;
			}
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space) && launcher != null)
			{
				if (launcher.transform.GetChild(0).GetChild(1).GetComponent<FontFirework>() != null && !launcher.transform.GetChild(0).GetChild(1).GetComponent<FontFirework>()
					.built)
				{
					launcher.transform.GetChild(0).GetChild(1).GetComponent<FontFirework>()
						.BuildFirework();
				}
				launcher.Launch(l);
			}
		}
	}
}
