using UnityEngine;

namespace Mix.Assets.Worker
{
	public class ByteToPNG : ThreadPoolManager.IUnityThreadPoolInterface
	{
		private object userData;

		protected IByteToPNG caller;

		private bool mCancelled;

		public bool Cancelled
		{
			set
			{
				mCancelled = value;
			}
		}

		public ByteToPNG(IByteToPNG aCaller, byte[] bytes, object aUserData = null)
		{
			userData = aUserData;
			Texture2D texture2D = new Texture2D(4, 4, TextureFormat.ARGB4444, false);
			texture2D.LoadImage(bytes);
			aCaller.OnByteToPNG(texture2D, userData);
		}

		public void ThreadedMethod(object aObject)
		{
			if (!mCancelled)
			{
			}
		}

		public void ThreadComplete(object aObject)
		{
			Destroy();
		}

		public void Destroy()
		{
			userData = null;
		}
	}
}
