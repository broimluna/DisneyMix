using UnityEngine;

namespace Mix
{
	public class Application
	{
		public static string TestingDirectory { get; set; }

		public static string PersistentDataPath
		{
			get
			{
				return UnityEngine.Application.persistentDataPath + TestingDirectory;
			}
		}

		public static string DataPath
		{
			get
			{
				return UnityEngine.Application.dataPath;
			}
		}

		public static string StreamingAssetsPath
		{
			get
			{
				return UnityEngine.Application.streamingAssetsPath;
			}
		}

		public static int Platform
		{
			get
			{
				return (int)UnityEngine.Application.platform;
			}
		}

		public static bool IsEditor
		{
			get
			{
				return UnityEngine.Application.isEditor;
			}
		}

		public static int TargetFrameRate
		{
			get
			{
				return UnityEngine.Application.targetFrameRate;
			}
			set
			{
				UnityEngine.Application.targetFrameRate = value;
			}
		}

		public static int InternetReachability
		{
			get
			{
				return (int)UnityEngine.Application.internetReachability;
			}
		}

		public static int SystemLanguage
		{
			get
			{
				return (int)UnityEngine.Application.systemLanguage;
			}
		}

		public static void OpenUrl(string url)
		{
			UnityEngine.Application.OpenURL(url);
		}

		public static void Quit()
		{
			UnityEngine.Application.Quit();
		}
	}
}
