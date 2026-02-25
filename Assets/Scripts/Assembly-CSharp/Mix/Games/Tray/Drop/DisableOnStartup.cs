using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	public class DisableOnStartup : MonoBehaviour
	{
		private bool disabled;

		private void OnEnable()
		{
			if (!disabled)
			{
				base.gameObject.SetActive(false);
				disabled = true;
			}
		}
	}
}
