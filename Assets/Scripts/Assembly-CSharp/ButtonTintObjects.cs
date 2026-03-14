using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTintObjects : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
{
	public List<Image> TintImage;

	public List<Text> TintText;

	public List<Image> EnableImageOnPress;

	private Button thisButton;

	private Color pressedColor;

	private Color upColor;

	private void Start()
	{
		thisButton = GetComponent<Button>();
		ColorBlock colors = thisButton.colors;
		pressedColor = new Color(colors.pressedColor.r, colors.pressedColor.g, colors.pressedColor.b);
		upColor = new Color(colors.normalColor.r, colors.normalColor.g, colors.normalColor.b);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		for (int i = 0; i < TintImage.Count; i++)
		{
			Image image = TintImage[i];
			image.color = pressedColor;
		}
		for (int j = 0; j < TintText.Count; j++)
		{
			Text text = TintText[j];
			text.color = pressedColor;
		}
		for (int k = 0; k < EnableImageOnPress.Count; k++)
		{
			Image image2 = EnableImageOnPress[k];
			image2.gameObject.SetActive(true);
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		for (int i = 0; i < TintImage.Count; i++)
		{
			Image image = TintImage[i];
			image.color = upColor;
		}
		for (int j = 0; j < TintText.Count; j++)
		{
			Text text = TintText[j];
			text.color = upColor;
		}
		for (int k = 0; k < EnableImageOnPress.Count; k++)
		{
			Image image2 = EnableImageOnPress[k];
			image2.gameObject.SetActive(false);
		}
	}
}
