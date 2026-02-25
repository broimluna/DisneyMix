namespace Mix
{
	public class TestFrameworkHelper
	{
		private IUnitySceneInterface scene;

		private IUnityTestRunnerObject testObj;

		private string currentTest;

		private bool isTesting;

		public bool IsTesting
		{
			get
			{
				return isTesting;
			}
		}

		public TestFrameworkHelper(IUnitySceneInterface _scene)
		{
			scene = _scene;
			testObj = scene.GetTestObj();
			isTesting = testObj != null;
			if (isTesting)
			{
				currentTest = testObj.GetTestName();
			}
		}

		public bool HasTestChanged()
		{
			if (!isTesting)
			{
				return false;
			}
			testObj = scene.GetTestObj();
			if (testObj != null && currentTest != testObj.GetTestName())
			{
				currentTest = testObj.GetTestName();
				return true;
			}
			return false;
		}
	}
}
