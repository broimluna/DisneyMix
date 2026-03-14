using System.Collections;
using Mix;

public class LoadingController : BaseController
{
	public static bool HideManagePC = true;

	private void Awake()
	{
	}

	private void Start()
	{
		StartUp.BeginStartSequence();
		StartCoroutine(LogLoadingPageView());
	}

	public IEnumerator LogLoadingPageView()
	{
		while (!Analytics.IsAnalyticsReady())
		{
			yield return null;
		}
		Analytics.LogLoadingPageView();
	}
}
