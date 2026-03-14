using UnityEngine;
using UnityTest;

namespace Mix
{
	public class UnityGameSceneWrapper : IUnitySceneInterface
	{
		private static ServiceLocator serviceLocator;

		public IUnityTestRunnerObject GetTestObj()
		{
			TestRunner testRunner = (TestRunner)Object.FindObjectOfType(typeof(TestRunner));
			if (testRunner != null)
			{
				return new TestRunnerGameObject(testRunner);
			}
			return null;
		}

		public IServiceLocator GetServiceLocator()
		{
			if (serviceLocator == null)
			{
				serviceLocator = new ServiceLocator();
			}
			return serviceLocator;
		}
	}
}
