using LitJson;
using UnityEngine;

namespace Disney.Native
{
	public class NativeCamera : MonoBehaviour
	{
		private INativeCamera listener;

		public virtual void ShowCameraOverlay(INativeCamera aListener)
		{
			listener = aListener;
		}

		public virtual void CloseOverlay()
		{
		}

		public virtual bool OnAndroidBackButton()
		{
			return false;
		}

		public virtual void OnApplicationPaused()
		{
		}

		public virtual void OnApplicationResumed()
		{
		}

		public virtual void OnSendPhoto(string aJsonData)
		{
			JsonData aJsonData2 = JsonMapper.ToObject(aJsonData);
			if (listener != null)
			{
				listener.SendPhoto(aJsonData2);
			}
			CloseOverlay();
		}

		public virtual void OnSendVideo(string aJsonData)
		{
			JsonData aJsonData2 = JsonMapper.ToObject(aJsonData);
			if (listener != null)
			{
				listener.SendVideo(aJsonData2);
			}
			CloseOverlay();
		}
	}
}
