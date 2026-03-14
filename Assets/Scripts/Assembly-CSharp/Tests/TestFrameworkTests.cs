using Mix;
using UnityEngine;

namespace Tests
{
	public class TestFrameworkTests : MonoBehaviour
	{
		private static TestFrameworkHelper helper;

		private void Start()
		{
			if (helper == null)
			{
				UnityGameSceneWrapper scene = new UnityGameSceneWrapper();
				helper = new TestFrameworkHelper(scene);
				IntegrationTest.Assert(base.gameObject, helper.IsTesting, "Test framework helper unable to find testrunner");
			}
			else
			{
				IntegrationTest.Assert(base.gameObject, helper.HasTestChanged(), "Test did not recognize test changed");
			}
		}

		private void Update()
		{
		}
	}
}
