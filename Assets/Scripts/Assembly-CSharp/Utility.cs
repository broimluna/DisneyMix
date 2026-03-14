using System.Collections;
using UnityEngine;

public class Utility : MonoBehaviour
{
	public Rect GetRectInPhysicalScreenSpace(RectTransform aRectTransform)
	{
		Vector3[] array = new Vector3[4];
		aRectTransform.GetWorldCorners(array);
		Canvas parentCanvas = GetParentCanvas(aRectTransform.gameObject);
		if (parentCanvas == null)
		{
			return new Rect(0f, 0f, 0f, 0f);
		}
		Camera worldCamera = parentCanvas.worldCamera;
		Vector2 vector = RectTransformUtility.WorldToScreenPoint(worldCamera, array[0]);
		Vector2 vector2 = RectTransformUtility.WorldToScreenPoint(worldCamera, array[1]);
		Vector2 vector3 = RectTransformUtility.WorldToScreenPoint(worldCamera, array[2]);
		float num = (float)GetScreenWidth() / (float)Screen.width;
		float num2 = (float)GetScreenHeight() / (float)Screen.height;
		int num3 = (int)((vector3.x - vector2.x) * num);
		int num4 = (int)((vector2.y - vector.y) * num2);
		int num5 = (int)(vector2.x * num);
		int num6 = (int)(vector2.y * num2);
		num6 = GetScreenHeight() - num6;
		return new Rect(num5, num6, num3, num4);
	}

	public static Canvas GetParentCanvas(GameObject aGameObject)
	{
		Canvas canvas = null;
		Transform parent = aGameObject.transform.parent;
		while (canvas == null && parent != null)
		{
			canvas = parent.GetComponent<Canvas>();
			parent = parent.parent;
		}
		return canvas;
	}

	public int GetScreenWidth()
	{
		return Display.displays[0].systemWidth;
	}

	public int GetScreenHeight()
	{
		return Display.displays[0].systemHeight;
	}

	public static string GetJsonString(Hashtable data, string key)
	{
		if (data == null || !data.Contains(key) || data[key].GetType() != typeof(string))
		{
			return null;
		}
		return data[key] as string;
	}
}
