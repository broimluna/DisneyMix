using Mix;
using UnityEngine;
using UnityEngine.UI;

public class LoginAnimationComplete : MonoBehaviour
{
	public InputField selectedField;

	public void OnAnimationComplete()
	{
		selectedField.Select();
	}

	public void OnIntroAnimationStart()
	{
		Singleton<SoundManager>.Instance.PlaySoundEvent("MainApp/Transitions/IntroScreen");
	}
}
