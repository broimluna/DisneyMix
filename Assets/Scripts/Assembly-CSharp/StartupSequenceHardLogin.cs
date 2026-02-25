using System.IO;
using Disney.MobileNetwork;
using Mix;
using Mix.Assets;
using Mix.Localization;
using Mix.Native;
using Mix.Ui;
using UnityEngine;
using UnityEngine.UI;

public class StartupSequenceHardLogin : MonoBehaviour
{
	private static float MEDIUM_PAUSE = 5f;

	private static float LONG_PAUSE = 10f;

	private void Start()
	{
		Service.ResetAll();
		AssetManager.SIMULATE_CONNECTION_LOST = true;
		if (Directory.Exists(UnityEngine.Application.persistentDataPath))
		{
			string[] files = Directory.GetFiles(UnityEngine.Application.persistentDataPath);
			string[] array = files;
			foreach (string path in array)
			{
				File.Delete(path);
			}
		}
		MonoSingleton<StartUpSequence>.Instance.Init();
		Invoke("checkStartupInit", LONG_PAUSE);
	}

	public void checkStartupInit()
	{
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
			return;
		}
		if (component.text == Localizer.NO_TOKEN)
		{
			IntegrationTest.Fail(base.gameObject);
			return;
		}
		AssetManager.SIMULATE_CONNECTION_LOST = false;
		DoHardLogin();
	}

	private void DoHardLogin()
	{
		GameObject gameObject = GameObject.Find("LoginBtn");
		gameObject.GetComponent<Button>().onClick.Invoke();
		Invoke("AnimationDone", MEDIUM_PAUSE);
	}

	private void AnimationDone()
	{
		NativeTextView component = GameObject.Find("UsernameInputField").GetComponent<NativeTextView>();
		NativeTextView component2 = GameObject.Find("password_input").GetComponent<NativeTextView>();
		component.Value = "logintest@disney.com";
		component2.Value = "qwe123";
		GameObject gameObject = GameObject.Find("SignInBtn");
		gameObject.GetComponent<Button>().onClick.Invoke();
		Invoke("OnLoginComplete", LONG_PAUSE);
	}

	private void OnLoginComplete()
	{
		NavigationRequest lastProcessedRequest = MonoSingleton<NavigationManager>.Instance.GetLastProcessedRequest();
		if (lastProcessedRequest.PrefabPath == "Prefabs/Screens/Login/LoginScreen")
		{
			IntegrationTest.Fail(base.gameObject);
			Cleanup();
		}
		else
		{
			Cleanup();
			IntegrationTest.Pass(base.gameObject);
		}
	}

	private void Cleanup()
	{
		Service.ResetAll();
		GameObject gameObject = GameObject.Find("Screens");
		foreach (Transform item in gameObject.transform)
		{
			Object.Destroy(item.gameObject);
		}
	}
}
