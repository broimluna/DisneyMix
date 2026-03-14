using UnityEngine;

namespace Mix.Test
{
	public class LoadPNGTest : MonoBehaviour
	{
		private void Start()
		{
			TestingUtils.ClearAllCache();
			IntegrationTest.Pass(base.gameObject);
		}
	}
}
