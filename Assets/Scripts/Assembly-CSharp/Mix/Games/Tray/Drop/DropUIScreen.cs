using UnityEngine;

namespace Mix.Games.Tray.Drop
{
	public class DropUIScreen : MonoBehaviour
	{
		public bool SpawnBehindCommonUI;

		public virtual void HideAndDestroy()
		{
			Object.Destroy(base.gameObject);
		}
	}
}
