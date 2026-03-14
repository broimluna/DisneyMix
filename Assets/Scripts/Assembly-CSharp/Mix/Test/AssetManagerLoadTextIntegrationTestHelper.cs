using Mix.Assets;
using Mix.SequenceOperations;
using UnityEngine;

namespace Mix.Test
{
	public class AssetManagerLoadTextIntegrationTestHelper : SequenceOperation, ITextAssetObject
	{
		public MonoBehaviour monoEngine;

		private string text;

		private string URL = "http://7e65011e-d1aa-935a-265a-3f36f7c14c51.disney.io/1469670667/donotcopy/n7c4c656";

		public AssetManagerLoadTextIntegrationTestHelper(IOperationCompleteHandler aCaller, MonoBehaviour aMonoEngine)
			: base(aCaller)
		{
			monoEngine = aMonoEngine;
		}

		public override void StartOperation()
		{
			BaseStartOperation();
			TestLoadText();
		}

		public void TestLoadText()
		{
			string shaString = AssetManager.GetShaString(URL);
			LoadParams aLoadParams = new LoadParams(shaString, URL);
			MonoSingleton<AssetManager>.Instance.AssetObjectQueue.Clear();
			MonoSingleton<AssetManager>.Instance.OnAssetObjectComplete += OnAssetObjectComplete;
			MonoSingleton<AssetManager>.Instance.LoadText(this, aLoadParams, "TestLoadText");
		}

		public void OnAssetObjectComplete(AssetObject aAssetObject)
		{
			if (aAssetObject.LoadParams.Url.Equals(URL))
			{
				MonoSingleton<AssetManager>.Instance.OnAssetObjectComplete -= OnAssetObjectComplete;
				if (text != null && text.Length > 0)
				{
					IntegrationTest.Pass(monoEngine.gameObject);
				}
				else
				{
					IntegrationTest.Fail(monoEngine.gameObject, "Text is empty.");
				}
			}
		}

		public void TextAssetObjectComplete(string aText, object aUserData)
		{
			text = aText;
		}
	}
}
