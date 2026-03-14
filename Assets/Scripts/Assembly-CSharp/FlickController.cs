using System.Collections.Generic;
using Mix;
using UnityEngine;
using UnityEngine.EventSystems;

public class FlickController : MonoBehaviour, IEventSystemHandler, IDragHandler
{
	private const int MAX_HISTORY = 4;

	private const int FLICK_AMOUNT = 75;

	private const int SINGLE_AMOUNT = 16;

	public bool Detecting;

	private IFlickCallback caller;

	private List<float> deltas = new List<float>();

	public void Detect(IFlickCallback aCaller)
	{
		Debug.Log("Detecting");
		caller = aCaller;
		Detecting = true;
		deltas.Clear();
	}

	public void Finish(bool aForced = false)
	{
		if (caller != null)
		{
			caller.OnFlicked(aForced || GetDragDelta() >= 75f);
		}
		Debug.Log("Finished 1");
		Detecting = false;
		deltas.Clear();
	}

	public void OnDrag(PointerEventData aEventData)
	{
		Debug.Log("Dragging: " + Detecting);
		if (Detecting)
		{
			deltas.Add(aEventData.delta.y * Singleton<SettingsManager>.Instance.GetHeightScale());
			if (deltas.Count > 4)
			{
				deltas.RemoveAt(0);
			}
			Debug.Log("Calling back");
			caller.OnDragging(deltas[deltas.Count - 1]);
		}
	}

	private float GetDragDelta()
	{
		float num = 0f;
		foreach (float delta in deltas)
		{
			float num2 = delta;
			if (num2 <= 0f)
			{
				break;
			}
			if (num2 > 16f)
			{
				num = 75f;
				break;
			}
			num += num2;
		}
		return num;
	}
}
