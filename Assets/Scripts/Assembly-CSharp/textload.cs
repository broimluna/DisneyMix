using System.Collections;
using Mix;
using Mix.Assets;
using UnityEngine;

public class textload : MonoBehaviour, ICpipeReady, ITextAssetObject
{
	private int NumTimesLoaded;

	void ITextAssetObject.TextAssetObjectComplete(string aText, object aUserData)
	{
		StartCoroutine(WaitToLoadTextAgain());
	}

	private void Start()
	{
	}

	public void OnCpipeReady(CpipeEvent aCpipeEvent)
	{
		LoadText();
	}

	public void OnCpipeFail(CpipeEvent aCpipeEvent)
	{
		Debug.LogError("cpipe failed to load");
	}

	public void LoadText()
	{
		NumTimesLoaded++;
		if (NumTimesLoaded < 3)
		{
			Debug.Log("start text load");
			string text = "http://www.google.com";
			string shaString = AssetManager.GetShaString(text);
			LoadParams aLoadParams = new LoadParams(shaString, text);
			MonoSingleton<AssetManager>.Instance.LoadText(this, aLoadParams);
		}
		else
		{
			Debug.Log("done loading 2");
		}
	}

	private IEnumerator WaitToLoadTextAgain()
	{
		yield return new WaitForSeconds(1f);
		LoadText();
		yield return 0;
	}
}
