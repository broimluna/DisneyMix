using UnityEngine;

namespace Mix.Connectivity
{
	public class ConnectionBanner : MonoBehaviour
	{
		public GameObject bannerToggle;

		private void Awake()
		{
			MonoSingleton<ConnectionManager>.Instance.RegisterBanner(bannerToggle);
		}

		private void OnDestroy()
		{
			if (MonoSingleton<ConnectionManager>.Instance != null)
			{
				MonoSingleton<ConnectionManager>.Instance.UnRegisterBanner(bannerToggle);
			}
		}
	}
}
