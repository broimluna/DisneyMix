namespace Mix.Assets
{
	public class TextAssetObject : AssetObject
	{
		protected ITextAssetObject caller;

		public TextAssetObject(IAssetManager aAssetManager, ITextAssetObject aCaller, LoadParams aLoadParams, object aUserData)
			: base(aAssetManager, aLoadParams, aUserData)
		{
			caller = aCaller;
		}

		public override string GetFileExtension()
		{
			return ".txt";
		}

		public override void Destroy()
		{
			CallCaller(null);
			caller = null;
			base.Destroy();
		}

		public void LoadFromBundle()
		{
			BaseLoadFromBundle("text");
		}

		public void CallCaller(string text)
		{
			if (caller != null && !base.flow.IsCallerCalled)
			{
				base.flow.IsCallerCalled = true;
				caller.TextAssetObjectComplete(text, userData);
			}
		}

		public override void RecordReturnedFromDBAndPathExistsLocal(string aPath)
		{
			string text = assetManager.LoadText(string.Empty, aPath);
			CallCaller(text);
		}

		public override void LoadFromWeb()
		{
			LoadTextFromWeb();
		}
	}
}
