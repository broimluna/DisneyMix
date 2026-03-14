using Mix;
using UnityEngine;

namespace Disney.Native
{
	public class NativeVideoPlayer : MonoBehaviour
	{
		public string URL;

		public void PlayVideo()
		{
			MonoSingleton<NativeVideoPlaybackManager>.Instance.Native.Play(URL, Util.GetRectInPhysicalScreenSpace(GetComponent<RectTransform>()));
		}
	}
}
