using Mix;
using Mix.Native;
using UnityEngine;

public class LoginAnimationComplete : MonoBehaviour
{
	public NativeTextView selectedField;

	public void OnAnimationComplete()
	{
		selectedField.SelectInput();
	}

	public void OnIntroAnimationStart()
	{
		Singleton<SoundManager>.Instance.PlaySoundEvent("MainApp/Transitions/IntroScreen");
	}
}
