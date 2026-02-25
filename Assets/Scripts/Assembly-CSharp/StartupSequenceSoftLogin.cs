using Mix;
using Mix.Ui;
using UnityEngine;

public class StartupSequenceSoftLogin : MonoBehaviour
{
	private static float LONG_PAUSE = 10f;

	private void Start()
	{
		IntegrationTest.Pass();
	}

	public void checkStartupInit()
	{
		if (!MonoSingleton<StartUpSequence>.Instance.StartUpSequenceComplete)
		{
			IntegrationTest.Fail(base.gameObject);
		}
		else
		{
			Invoke("OnLoginComplete", LONG_PAUSE);
		}
	}

	private void OnLoginComplete()
	{
		NavigationRequest lastProcessedRequest = MonoSingleton<NavigationManager>.Instance.GetLastProcessedRequest();
		if (lastProcessedRequest.PrefabPath != "Prefabs/Screens/Conversations/ConversationsScreen")
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
		GameObject gameObject = GameObject.Find("Screens");
		foreach (Transform item in gameObject.transform)
		{
			Object.Destroy(item.gameObject);
		}
	}
}
