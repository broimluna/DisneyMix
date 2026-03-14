using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class ToggleOffImage : MonoBehaviour
{
	[Tooltip("The toggle swaps to this image when set to off")]
	public Image OffImage;

	private Toggle thisToggle;

	private void Start()
	{
		thisToggle = GetComponent<Toggle>();
		thisToggle.onValueChanged.AddListener(OnTargetToggleValueChanged);
		SwapImage();
	}

	private void OnTargetToggleValueChanged(bool isOn)
	{
		SwapImage();
	}

	private void SwapImage()
	{
		if (OffImage == null)
		{
			Debug.LogError("No off image set");
		}
		else if (thisToggle.isOn)
		{
			OffImage.enabled = false;
		}
		else
		{
			OffImage.enabled = true;
		}
	}
}
