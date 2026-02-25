using System.Collections;
using Mix;
using Mix.Assets;
using UnityEngine;

public class imageload : MonoBehaviour, ICpipeReady, IPNGAssetObject
{
	private int NumTimesLoaded;

	void IPNGAssetObject.OnPNGAssetObject(Texture2D aTexture, object aUserData)
	{
		StartCoroutine(WaitToLoadTextAgain());
	}

	public void OnCpipeReady(CpipeEvent aCpipeEvent)
	{
		LoadImage();
	}

	public void OnCpipeFail(CpipeEvent aCpipeEvent)
	{
		Debug.LogError("cpipe failed to load");
	}

	private void Start()
	{
	}

	public void LoadImage()
	{
		NumTimesLoaded++;
		if (NumTimesLoaded < 3)
		{
			Debug.Log("start image load");
			string text = "https://cdn.infinity.disneyis.com/infinitycdn/as/power-discs/igp_coin_chromedamageincreaser.png";
			string shaString = AssetManager.GetShaString(text);
			LoadParams aLoadParams = new LoadParams(shaString, text);
			MonoSingleton<AssetManager>.Instance.LoadPng(this, aLoadParams);
		}
		else
		{
			Debug.Log("done loading 2");
		}
	}

	private IEnumerator WaitToLoadTextAgain()
	{
		yield return new WaitForSeconds(3f);
		LoadImage();
		yield return 0;
	}
}
