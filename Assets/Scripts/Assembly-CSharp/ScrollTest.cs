using UnityEngine;
using UnityEngine.UI;

public class ScrollTest : MonoBehaviour
{
	public GameObject holder;

	public GameObject item;

	private int count;

	private void Start()
	{
		AddItemToScrollView();
	}

	private void Update()
	{
	}

	public void AddItemToScrollView()
	{
		GameObject gameObject = Object.Instantiate(item);
		float height = gameObject.GetComponent<RectTransform>().rect.height;
		gameObject.transform.localPosition = new Vector3(0f, -1f * ((float)count * height), 0f);
		gameObject.transform.SetParent(holder.transform, false);
		gameObject.transform.Find("Text").GetComponent<Text>().text = "Item " + count.ToString("0000");
		count++;
		holder.GetComponent<RectTransform>().sizeDelta = new Vector2(holder.GetComponent<RectTransform>().sizeDelta.x, (float)count * height);
		holder.transform.localPosition = new Vector3(holder.transform.localPosition.x, (float)count * height - holder.transform.parent.GetComponent<RectTransform>().rect.height, 0f);
	}
}
