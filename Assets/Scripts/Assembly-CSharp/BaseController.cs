using Mix;
using Mix.Ui;
using UnityEngine;

public class BaseController : MonoBehaviour
{
	public void OnAnimationComplete(string aAnimationState)
	{
		NavigationRequest navigationRequest = MonoSingleton<NavigationManager>.Instance.FindCurrentRequest();
		if (navigationRequest != null)
		{
			navigationRequest.Transition.OnAnimationComplete(aAnimationState);
		}
	}

	public virtual void OnDataReceived(string aToken, object aData)
	{
	}

	public virtual void OnUILoaded(NavigationRequest aNavigationRequest = null)
	{
	}

	public virtual void OnUIUnLoaded(NavigationRequest aNavigationRequest = null)
	{
	}

	public virtual void OnUITransitionStart()
	{
	}

	public virtual void OnUITransitionEnd()
	{
	}

	public virtual void OnAndroidBackButtonClicked()
	{
	}
}
