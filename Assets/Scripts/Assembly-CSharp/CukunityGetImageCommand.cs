using System;
using System.Collections;
using UnityEngine;

public class CukunityGetImageCommand : CukunityCommand
{
	public override void Process(Hashtable req, Hashtable res)
	{
		string jsonString = Utility.GetJsonString(req, "name");
		GameObject gameObject = null;
		try
		{
			gameObject = GameObject.Find(jsonString);
		}
		catch (Exception ex)
		{
			Debug.LogWarning("@@UA_DEBUG@@ Unable to get Image from scene " + jsonString + " " + ex.ToString());
		}
		Texture2D texture2D = null;
		try
		{
			Material material = gameObject.GetComponent<SkinnedMeshRenderer>().material;
			Debug.LogWarning("@@UA_DEBUG@@ SCD");
			if (material == null)
			{
				Debug.LogWarning("@@UA_DEBUG@@ material NULL ");
			}
			Debug.LogWarning("@@UA_DEBUG@@ SCD");
			if (material.GetTexture("_MainTex") == null)
			{
				Debug.LogWarning("@@UA_DEBUG@@ TESSTX NULL");
			}
			Debug.LogWarning("@@UA_DEBUG@@ SCD");
			texture2D = material.GetTexture("_MainTex") as Texture2D;
			if (texture2D == null)
			{
				Debug.LogWarning("@@UA_DEBUG@@ TMP IS NULL ");
			}
		}
		catch (Exception ex2)
		{
			Debug.LogWarning("@@UA_DEBUG@@ Unable to get material " + ex2.ToString());
		}
		string text = "X";
		try
		{
			text = Convert.ToBase64String(texture2D.EncodeToPNG());
		}
		catch (Exception ex3)
		{
			Debug.LogWarning("@@UA_DEBUG@@ Unable to convert to base64 " + ex3.ToString());
		}
		Debug.LogWarning("@@UA_DEBUG@@ ENCODING TO BASE64 " + text);
		res["image"] = string.Empty;
	}
}
