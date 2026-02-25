using UnityEngine;

namespace Mix.Test
{
	public class AssetDatabaseAccessCountIncrement : MonoBehaviour
	{
		private void Start()
		{
			TestingUtils.ClearAllCache();
			IntegrationTest.Pass(base.gameObject);
		}
	}
}
