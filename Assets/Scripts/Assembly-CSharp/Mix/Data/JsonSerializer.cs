using System;
using System.Collections;
using System.IO;
using LitJson;

namespace Mix.Data
{
	public class JsonSerializer : ThreadPoolManager.IUnityThreadPoolInterface
	{
		protected IJsonSerializer caller;

		private bool mCancelled;

		public bool Cancelled
		{
			set
			{
				mCancelled = value;
			}
		}

		public JsonSerializer(IJsonSerializer aCaller, string aJsonFileOrData, bool aReadFromFile, Func<string, object> aFunc)
		{
			caller = aCaller;
			Hashtable hashtable = new Hashtable();
			hashtable["ContentData"] = null;
			hashtable["JsonFileOrData"] = aJsonFileOrData;
			hashtable["ReadFromFile"] = aReadFromFile;
			hashtable["parseMethod"] = aFunc;
			ThreadPoolManager.Instance.addToPool(this, hashtable);
		}

		public void ThreadedMethod(object aObject)
		{
			if (mCancelled)
			{
				return;
			}
			try
			{
				if (caller == null)
				{
					return;
				}
				Hashtable hashtable = (Hashtable)aObject;
				Func<string, object> func = null;
				if (hashtable.ContainsKey("parseMethod"))
				{
					func = (Func<string, object>)hashtable["parseMethod"];
				}
				object value = null;
				string text = (string)hashtable["JsonFileOrData"];
				if ((bool)hashtable["ReadFromFile"])
				{
					text = readFromDiskCache(text);
				}
				if (!string.IsNullOrEmpty(text))
				{
					try
					{
						value = ((func != null) ? func(text) : JsonMapper.ToObject(text));
					}
					catch (Exception)
					{
					}
					hashtable["ContentData"] = value;
				}
				else
				{
					hashtable["ContentData"] = "ContentData is null.";
				}
			}
			catch (Exception ex2)
			{
				Console.WriteLine(ex2.Message);
			}
		}

		private string readFromDiskCache(string aFileName)
		{
			try
			{
				return File.ReadAllText(aFileName);
			}
			catch (IOException)
			{
				return string.Empty;
			}
		}

		public void ThreadComplete(object aObject)
		{
			if (!mCancelled)
			{
				Hashtable hashtable = (Hashtable)aObject;
				caller.OnJsonSerializer(hashtable["ContentData"]);
			}
			Destroy();
		}

		public void Destroy()
		{
			caller = null;
		}
	}
}
