using Disney.MobileNetwork;
using Mix;
using Mix.Assets;
using Mix.Localization;
using Mix.Ui;
using UnityEngine;
using UnityEngine.UI;

public class StartupSequenceInit : MonoBehaviour
{
	private static float LONG_PAUSE = 5f;

	private void Start()
	{
		Service.ResetAll();
		AssetManager.SIMULATE_CONNECTION_LOST = true;
		MonoSingleton<StartUpSequence>.Instance.Init();
		Invoke("checkStartupInit", LONG_PAUSE);
	}

	public void checkStartupInit()
	{
		AssetManager.SIMULATE_CONNECTION_LOST = false;
		if (!MonoSingleton<StartUpSequence>.Instance.StartUpSequenceComplete)
		{
			IntegrationTest.Fail(base.gameObject);
			return;
		}
		NavigationRequest lastProcessedRequest = MonoSingleton<NavigationManager>.Instance.GetLastProcessedRequest();
		if (lastProcessedRequest.PrefabPath != "Prefabs/Screens/Login/LoginScreen")
		{
			IntegrationTest.Fail(base.gameObject);
			return;
		}
		GameObject gameObject = GameObject.Find("UI_FG_LoginScreen");
		if (gameObject == null)
		{
			IntegrationTest.Fail(base.gameObject);
			return;
		}
		Transform transform = gameObject.transform.Find("LoginBtn/LoginTxt");
		if (transform == null)
		{
			IntegrationTest.Fail(base.gameObject);
			return;
		}
		Text component = transform.gameObject.GetComponent<Text>();
		if (component == null)
		{
			IntegrationTest.Fail(base.gameObject);
		}
		else if (component.text == Localizer.NO_TOKEN)
		{
			IntegrationTest.Fail(base.gameObject);
		}
		else
		{
			IntegrationTest.Pass(base.gameObject);
		}
	}
}
