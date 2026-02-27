using System.Collections;
using System.IO;
using Disney.MobileNetwork;
using UnityEngine;

namespace Mix.Video
{
	public class FullScreenVideo
	{
		public delegate void VideoCompleteEvent(FullScreenVideo aFullScreenVideo);

		public static bool IsVideoPlaying;

		public string StreamingAssetVideoPath;

		protected string CacheFileName;

		protected float VideoPlayTime;

		public event VideoCompleteEvent OnVideoComplete;

		public FullScreenVideo(string a169FileName, string a43FileName, string aCacheFileName, float aVideoPlayTime)
		{
			CacheFileName = "/cache/" + aCacheFileName;
			VideoPlayTime = aVideoPlayTime;
			StreamingAssetVideoPath = Application.StreamingAssetsPath;
			float num = (float)Screen.height / (float)Screen.width;
			float num2 = 1.6666666f;
			if (num > num2)
			{
				StreamingAssetVideoPath = Application.StreamingAssetsPath + "/video/" + a169FileName;
			}
			else
			{
				StreamingAssetVideoPath = Application.StreamingAssetsPath + "/video/" + a43FileName;
			}
		}

		public void PlayVideo(MonoBehaviour aMonoBehaviour)
		{
			EnvironmentManager.ShowStatusBar(false);
			aMonoBehaviour.StartCoroutine(UnpackAndroidVideo(StreamingAssetVideoPath, CacheFileName));
			if (!File.Exists(Application.PersistentDataPath + "/" + CacheFileName))
			{
				Debug.LogError("Intro Video Didn't Copy to persisntent Data Correctly.");
			}
			else
			{
				aMonoBehaviour.StartCoroutine(PlayVideo(Application.PersistentDataPath + "/" + CacheFileName));
			}
		}

		private IEnumerator PlayVideo(string aFilePath)
		{
			IsVideoPlaying = true;
			float startTime = Time.realtimeSinceStartup;
			yield return new WaitForSeconds(0.1f);
			float endVideoTime = Time.realtimeSinceStartup;
			if (endVideoTime - startTime < VideoPlayTime)
			{
				Analytics.LogVideoSkip();
			}
			IsVideoPlaying = false;
			EnvironmentManager.ShowStatusBar(true);
			if (this.OnVideoComplete != null)
			{
				this.OnVideoComplete(this);
			}
		}

		private IEnumerator UnpackAndroidVideo(string aSourcePath, string aDestPath)
		{
			WWW www = new WWW(aSourcePath);
			while (!www.isDone && string.IsNullOrEmpty(www.error))
			{
			}
			if (string.IsNullOrEmpty(www.error))
			{
				File.WriteAllBytes(Application.PersistentDataPath + "/" + aDestPath, www.bytes);
			}
			yield return null;
		}
	}
}
