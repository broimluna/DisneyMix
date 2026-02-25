using System.Collections.Generic;
using Mix;
using UnityEngine;

public class SimpleAnimator : MonoBehaviour
{
	public enum AnimationDirection
	{
		Idle = 0,
		Up = 1,
		Down = 2,
		Left = 3,
		Right = 4
	}

	private float duration;

	private RectTransform[] one;

	private RectTransform[] two;

	private List<Vector2> startOne = new List<Vector2>();

	private List<Vector2> startTwo = new List<Vector2>();

	private AnimationDirection direction;

	private float startTime;

	public void StartAnimation(RectTransform[] aOne, RectTransform[] aTwo, AnimationDirection aDirection, float aDuration = 1f)
	{
		duration = aDuration;
		one = aOne;
		two = aTwo;
		startOne.Clear();
		foreach (RectTransform rectTransform in aOne)
		{
			startOne.Add(rectTransform.anchoredPosition);
		}
		startTwo.Clear();
		foreach (RectTransform rectTransform2 in aTwo)
		{
			startTwo.Add(rectTransform2.anchoredPosition);
		}
		direction = aDirection;
		startTime = Time.time;
	}

	public void FixedUpdate()
	{
		if (direction.Equals(AnimationDirection.Idle))
		{
			return;
		}
		float num = Time.time - startTime;
		float num2 = num / duration;
		switch (direction)
		{
		case AnimationDirection.Down:
		{
			for (int m = 0; m < one.Length; m++)
			{
				one[m].anchoredPosition = Vector2.Lerp(startOne[m], Vector2.zero, num2);
			}
			for (int n = 0; n < two.Length; n++)
			{
				two[n].anchoredPosition = Vector2.Lerp(startTwo[n], new Vector2(0f, 0f - MixConstants.CANVAS_HEIGHT), num2);
			}
			break;
		}
		case AnimationDirection.Up:
		{
			for (int num3 = 0; num3 < one.Length; num3++)
			{
				one[num3].anchoredPosition = Vector2.Lerp(startOne[num3], new Vector2(0f, MixConstants.CANVAS_HEIGHT), num2);
			}
			for (int num4 = 0; num4 < two.Length; num4++)
			{
				two[num4].anchoredPosition = Vector2.Lerp(startTwo[num4], Vector2.zero, num2);
			}
			break;
		}
		case AnimationDirection.Left:
		{
			for (int k = 0; k < one.Length; k++)
			{
				one[k].anchoredPosition = Vector2.Lerp(startOne[k], new Vector2(0f - MixConstants.CANVAS_WIDTH, 0f), num2);
			}
			for (int l = 0; l < two.Length; l++)
			{
				two[l].anchoredPosition = Vector2.Lerp(startTwo[l], Vector2.zero, num2);
			}
			break;
		}
		case AnimationDirection.Right:
		{
			for (int i = 0; i < one.Length; i++)
			{
				one[i].anchoredPosition = Vector2.Lerp(startOne[i], new Vector2(MixConstants.CANVAS_WIDTH, 0f), num2);
			}
			for (int j = 0; j < two.Length; j++)
			{
				two[j].anchoredPosition = Vector2.Lerp(startTwo[j], Vector2.zero, num2);
			}
			break;
		}
		}
		if ((double)num2 >= 1.0)
		{
			direction = AnimationDirection.Idle;
		}
	}
}
