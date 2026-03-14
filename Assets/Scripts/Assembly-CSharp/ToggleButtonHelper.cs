using UnityEngine;
using UnityEngine.UI;

public class ToggleButtonHelper : MonoBehaviour
{
	public RectTransform ToggleSwitch;

	public Image ToggleOffImage;

	private Toggle thisToggle;

	private void Start()
	{
		thisToggle = GetComponent<Toggle>();
		thisToggle.onValueChanged.AddListener(OnTargetToggleValueChanged);
		PositionSwitch();
	}

	private void OnTargetToggleValueChanged(bool isOn)
	{
		PositionSwitch();
	}

	private void PositionSwitch()
	{
		if (thisToggle.isOn)
		{
			ToggleSwitch.anchoredPosition = new Vector2(20f, 0f);
			ToggleOffImage.enabled = false;
		}
		else
		{
			ToggleSwitch.anchoredPosition = new Vector2(-20f, 0f);
			ToggleOffImage.enabled = true;
		}
	}
}
