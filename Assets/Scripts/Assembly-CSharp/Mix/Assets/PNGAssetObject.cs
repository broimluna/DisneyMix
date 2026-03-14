using UnityEngine;

namespace Mix.Assets
{
	public class PNGAssetObject : AssetObject
	{
		protected IPNGAssetObject caller;

		public PNGAssetObject(IAssetManager aAssetManager, IPNGAssetObject aCaller, LoadParams aLoadParams, object aUserData)
			: base(aAssetManager, aLoadParams, aUserData)
		{
			caller = aCaller;
		}

		public override void Destroy()
		{
			CallCaller(null);
			caller = null;
			base.Destroy();
		}

		public override string GetFileExtension()
		{
			if (base.LoadParams.Url.EndsWith(".png"))
			{
				return ".png";
			}
			return ".jpg";
		}

		public void LoadFromBundle()
		{
			BaseLoadFromBundle("image");
		}

		public void CallCaller(Texture2D tex)
		{
			if (caller != null && !base.flow.IsCallerCalled)
			{
				base.flow.IsCallerCalled = true;
				caller.OnPNGAssetObject(tex, userData);
			}
		}

		public override void RecordReturnedFromDBAndPathExistsLocal(string aPath)
		{
			Texture2D image = new Texture2D(4, 4, TextureFormat.ARGB4444, false);
			assetManager.GetImage(string.Empty, ref image, aPath);
			CallCaller(image);
		}

		public override void LoadFromWeb()
		{
			LoadBinaryFromWeb();
		}
	}
}
