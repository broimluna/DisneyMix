using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PressureSensitiveButton : Button, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler
{
	public float Pressure;

	public bool HardPress;

	private bool press;

	private int pressId;

	private float pressureThreshholdPercent = 0.65f;

	private void Update()
	{
		if (Input.touchPressureSupported && press)
		{
			Touch touch = Input.GetTouch(pressId);
			if (touch.pressure > Pressure)
			{
				Pressure = touch.pressure;
			}
			if (touch.pressure / touch.maximumPossiblePressure >= pressureThreshholdPercent)
			{
				HardPress = true;
				press = false;
				base.onClick.Invoke();
			}
		}
	}

	public override void OnPointerDown(PointerEventData data)
	{
		HardPress = false;
		base.OnPointerDown(data);
		if (Input.touchPressureSupported && Input.GetTouch(data.pointerId).maximumPossiblePressure > 1f)
		{
			press = true;
			pressId = data.pointerId;
			Pressure = 0f;
		}
	}

	public override void OnPointerUp(PointerEventData data)
	{
		if (Input.touchPressureSupported)
		{
			press = false;
			HardPress = false;
		}
		base.OnPointerUp(data);
	}
}
