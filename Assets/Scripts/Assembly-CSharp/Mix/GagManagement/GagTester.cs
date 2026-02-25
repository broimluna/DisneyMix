using UnityEngine;

namespace Mix.GagManagement
{
	public class GagTester : MonoBehaviour
	{
		public GameObject GagPrefab;

		public GagManager.SenderPosition SenderPosition;

		public bool FlipReceiver = true;

		private void Start()
		{
			if (GagPrefab != null)
			{
				MonoSingleton<GagManager>.Instance.PlayGag(GagPrefab, string.Empty, SenderPosition, "sender", "receiver", FlipReceiver, null, null, null, true);
			}
		}
	}
}
