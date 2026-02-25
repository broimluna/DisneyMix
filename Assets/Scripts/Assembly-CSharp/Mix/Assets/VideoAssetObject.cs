using System.Collections.Generic;

namespace Mix.Assets
{
	public class VideoAssetObject : AssetObject
	{
		protected IVideoAssetObject caller;

		public VideoAssetObject(IAssetManager aAssetManager, IVideoAssetObject aCaller, LoadParams aLoadParams, object aUserData)
			: base(aAssetManager, aLoadParams, aUserData)
		{
			caller = aCaller;
		}

		public void LoadFromBundle()
		{
			BaseLoadFromBundle("path");
		}

		public override void Destroy()
		{
			CallCaller(null);
			caller = null;
			base.Destroy();
		}

		public override string GetFileExtension()
		{
			if (base.LoadParams.Url.EndsWith(".mp4"))
			{
				return ".mp4";
			}
			return ".3gp";
		}

		public void CallCaller(string text)
		{
			if (caller != null && !base.flow.IsCallerCalled)
			{
				base.flow.IsCallerCalled = true;
				caller.OnVideoAssetObject(text, userData);
			}
		}

		public override void RecordReturnedFromDBAndPathExistsLocal(string aPath)
		{
			CallCaller(aPath);
		}

		public override void LoadFromWeb()
		{
			LoadBinaryFromWeb();
		}

		protected override void WebBinaryResponseHandler(bool aIsSuccess, byte[] aBody, Dictionary<string, string> aHeaders, bool aIsReturnPathToAsset = false)
		{
			base.WebBinaryResponseHandler(aIsSuccess, aBody, aHeaders, true);
		}
	}
}
