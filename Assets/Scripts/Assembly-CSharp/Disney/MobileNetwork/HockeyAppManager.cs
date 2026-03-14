using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Mix.Assets;
using UnityEngine;

namespace Disney.MobileNetwork
{
	public class HockeyAppManager : MonoBehaviour, IInitializable
	{
		protected const string HOCKEYAPP_BASEURL = "https://api.disney.com/dmn/crash/v2";

		protected const string HEADER_KEY = " FD 5F20D8F8-9411-45D7-ADAC-F186C5B3574C:72C6967910F6B3FD03DF0AAF9C692860409908D8AD8CCC9E";

		protected const string HOCKEYAPP_CRASHESPATH = "apps/[APPID]/crashes/upload";

		protected const int MAX_CHARS = 199800;

		protected const string LOG_FILE_DIR = "/logs/";

		protected string serverURL = "https://api.disney.com/dmn/crash/v2";

		public string appID = "your-hockey-app-id";

		public string packageID = "your-package-identifier";

		public bool exceptionLogging;

		public bool updateManager;

		public Hashtable loggedExceptions = new Hashtable();

		public virtual void Init()
		{
			base.gameObject.name = "HockeyApp";
			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
			string text = Application.persistentDataPath + "/logs/";
			try
			{
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
			}
			catch (Exception ex)
			{
				Debug.LogError("Error " + ex.Message + " creating logs dir : " + text);
			}
			if (exceptionLogging && IsConnected())
			{
				List<string> logFiles = GetLogFiles();
				if (logFiles.Count > 0)
				{
					StartCoroutine(SendLogs(logFiles));
				}
			}
			SubscribeToCallbacks();
		}

		public void OnEnable()
		{
			SubscribeToCallbacks();
		}

		public void OnDisable()
		{
			UnsubscribeFromCallbacks();
		}

		private void OnDestroy()
		{
			UnsubscribeFromCallbacks();
		}

		public void SubscribeToCallbacks()
		{
			if (exceptionLogging)
			{
				AppDomain.CurrentDomain.UnhandledException += OnHandleUnresolvedException;
				Application.logMessageReceived += OnHandleLogCallback;
			}
		}

		protected void UnsubscribeFromCallbacks()
		{
			AppDomain.CurrentDomain.UnhandledException -= OnHandleUnresolvedException;
			Application.logMessageReceived -= OnHandleLogCallback;
		}

		public virtual void ForceCrash()
		{
		}

		protected virtual void HandleException(string logString, string stackTrace)
		{
			WriteLogToDisk(logString, stackTrace);
		}

		public void OnHandleLogCallback(string logString, string stackTrace, LogType type)
		{
			if ((type == LogType.Assert || type == LogType.Exception) && exceptionLogging)
			{
				HandleException(logString, stackTrace);
			}
		}

		public void OnHandleUnresolvedException(object sender, UnhandledExceptionEventArgs args)
		{
			if (args != null && args.ExceptionObject != null && args.ExceptionObject.GetType() == typeof(Exception))
			{
				Exception ex = (Exception)args.ExceptionObject;
				HandleException(ex.Source + " - " + ex.Message, ex.StackTrace);
			}
		}

		public void OnHandleUnresolvedException(string logString, string stackTrace, LogType type)
		{
			HandleException(logString, stackTrace);
		}

		protected virtual List<string> GetLogHeaders()
		{
			return new List<string>();
		}

		protected virtual List<string> GetLogFiles()
		{
			List<string> list = new List<string>();
			string path = Application.persistentDataPath + "/logs/";
			try
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(path);
				FileInfo[] files = directoryInfo.GetFiles();
				if (files.Length > 0)
				{
					FileInfo[] array = files;
					foreach (FileInfo fileInfo in array)
					{
						if (fileInfo.Extension == ".log")
						{
							list.Add(fileInfo.FullName);
						}
						else
						{
							File.Delete(fileInfo.FullName);
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (Debug.isDebugBuild)
				{
					Debug.Log("Failed to write exception log to file: " + ex);
				}
			}
			return list;
		}

		protected string GetBaseURL()
		{
			string empty = string.Empty;
			string text = serverURL.Trim();
			if (text.Length > 0)
			{
				empty = text;
				if (!empty[empty.Length - 1].Equals("/"))
				{
					empty += "/";
				}
			}
			else
			{
				empty = "https://api.disney.com/dmn/crash/v2";
			}
			return empty;
		}

		protected bool IsConnected()
		{
			bool result = false;
			if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork || Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
			{
				result = true;
			}
			return result;
		}

		protected void WriteLogToDisk(string logString, string stackTrace)
		{
			string shaString = AssetManager.GetShaString(logString + stackTrace);
			if (loggedExceptions.ContainsKey(shaString))
			{
				return;
			}
			loggedExceptions.Add(shaString, true);
			string text = DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss_fff");
			string text2 = logString.Replace("\n", " ");
			string[] array = stackTrace.Split('\n');
			text2 = "\n" + text2 + "\n";
			string[] array2 = array;
			foreach (string text3 in array2)
			{
				if (text3.Length > 0)
				{
					text2 = text2 + "  at " + text3 + "\n";
				}
			}
			List<string> logHeaders = GetLogHeaders();
			using (StreamWriter streamWriter = new StreamWriter(Application.persistentDataPath + "/logs/LogFile_" + text + ".log", true))
			{
				foreach (string item in logHeaders)
				{
					streamWriter.WriteLine(item);
				}
				streamWriter.WriteLine(text2);
			}
		}

		protected IEnumerator SendLogs(List<string> logs)
		{
			Debug.Log("*** HockeyAppManager::SendLogs() - Sending Logs");
			string crashPath = "apps/[APPID]/crashes/upload";
			string url = GetBaseURL() + crashPath.Replace("[APPID]", appID);
			foreach (string log in logs)
			{
				Debug.Log("*** HockeyAppManager::SendLogs() - Sending Log: " + log);
				WWWForm postForm = CreateForm(log);
				string lContent = postForm.headers["Content-Type"].ToString();
				lContent = lContent.Replace("\"", string.Empty);
				WWW www = new WWW(headers: new Dictionary<string, string>
				{
					{ "Authorization", " FD 5F20D8F8-9411-45D7-ADAC-F186C5B3574C:72C6967910F6B3FD03DF0AAF9C692860409908D8AD8CCC9E" },
					{ "Content-Type", lContent }
				}, url: url, postData: postForm.data);
				yield return www;
				if (!string.IsNullOrEmpty(www.error))
				{
					continue;
				}
				try
				{
					File.Delete(log);
				}
				catch (Exception ex)
				{
					Exception e = ex;
					if (Debug.isDebugBuild)
					{
						Debug.Log("Failed to delete exception log: " + e);
					}
				}
			}
		}

		protected virtual WWWForm CreateForm(string log)
		{
			WWWForm wWWForm = new WWWForm();
			byte[] array = null;
			using (FileStream fileStream = File.OpenRead(log))
			{
				if (fileStream.Length > 199800)
				{
					string text = null;
					using (StreamReader streamReader = new StreamReader(fileStream))
					{
						streamReader.BaseStream.Seek(fileStream.Length - 199800, SeekOrigin.Begin);
						text = streamReader.ReadToEnd();
					}
					List<string> logHeaders = GetLogHeaders();
					string text2 = string.Empty;
					foreach (string item in logHeaders)
					{
						text2 = text2 + item + "\n";
					}
					text = text2 + "\n[...]" + text;
					try
					{
						array = Encoding.Default.GetBytes(text);
					}
					catch (ArgumentException ex)
					{
						if (Debug.isDebugBuild)
						{
							Debug.Log("Failed to read bytes of log file: " + ex);
						}
					}
				}
				else
				{
					try
					{
						array = File.ReadAllBytes(log);
					}
					catch (SystemException ex2)
					{
						if (Debug.isDebugBuild)
						{
							Debug.Log("Failed to read bytes of log file: " + ex2);
						}
					}
				}
			}
			if (array != null)
			{
				wWWForm.AddBinaryData("log", array, log, "text/plain");
			}
			return wWWForm;
		}
	}
}
