using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CukunityLoadLevelCommand : CukunityCommand
{
	public override void Process(Hashtable req, Hashtable res)
	{
		int value = -1;
		string jsonString = Utility.GetJsonString(req, "name");
		string text = Utility.GetJsonString(req, "method");
		bool jsonInt = GetJsonInt(req, "number", ref value);
		if (jsonInt && jsonString != null)
		{
			Debug.LogError("Cukunity: cannot specify both level name and number");
			res["error"] = "BothLevelNameOrNumberError";
			return;
		}
		if ((value < 0 || value >= SceneManager.sceneCountInBuildSettings) && (jsonString == null || jsonString.Length == 0))
		{
			Debug.LogError("Cukunity: missing level name/number");
			res["error"] = "MissingLevelNameOrNumberError";
			return;
		}
		if (text == null)
		{
			text = "sync";
		}
		switch (text.ToLower())
		{
		case "sync":
			try
			{
				if (jsonInt)
				{
					SceneManager.LoadScene(value);
				}
				else
				{
					SceneManager.LoadScene(jsonString);
				}
				break;
			}
			catch (Exception)
			{
				Debug.LogWarning("load level failed ");
				throw new Exception("Load level failed");
			}
		case "async":
			try
			{
				if (jsonInt)
				{
					SceneManager.LoadScene(value);
				}
				else
				{
					SceneManager.LoadSceneAsync(jsonString);
				}
				break;
			}
			catch (Exception)
			{
				Debug.LogWarning("load level failed async ");
				throw new Exception("Load level failed");
			}
		case "additive":
			try
			{
				if (jsonInt)
				{
					SceneManager.LoadScene(value);
				}
				else
				{
					SceneManager.LoadScene(jsonString);
				}
				break;
			}
			catch (Exception)
			{
				throw new Exception("Load level failed");
			}
		case "additiveasync":
			try
			{
				if (jsonInt)
				{
					SceneManager.LoadSceneAsync(value);
				}
				else
				{
					SceneManager.LoadSceneAsync(jsonString);
				}
				break;
			}
			catch (Exception)
			{
				throw new Exception("Load level failed");
			}
		default:
			Debug.LogError("Cukunity: unknown load level method in client's request");
			res["error"] = "UnknownLoadLevelMethodError";
			break;
		}
	}

	private static bool GetJsonInt(Hashtable data, string key, ref int value)
	{
		if (data == null || !data.Contains(key) || data[key].GetType() != typeof(int))
		{
			return false;
		}
		value = (int)data[key];
		return true;
	}
}
