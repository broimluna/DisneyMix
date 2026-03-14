using System.Collections;
using UnityEngine;

public class TestPrefabHolder : MonoBehaviour
{
	public GameObject parent;

	public GameObject screenOne;

	public GameObject screenTwo;

	private int depth;

	private void Start()
	{
		Make();
		StartCoroutine(LoadNext());
	}

	private IEnumerator LoadNext()
	{
		yield return new WaitForSeconds(5f);
		Make();
	}

	private void Make()
	{
		GameObject gameObject = Object.Instantiate(screenOne);
		gameObject.transform.SetParent(GameObject.Find("Screens").transform, false);
		gameObject.SetActive(false);
		gameObject.SetActive(true);
		Canvas[] componentsInChildren = gameObject.GetComponentsInChildren<Canvas>(true);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			string text = componentsInChildren[i].name.Replace("Holder", "Camera");
			componentsInChildren[i].worldCamera = GameObject.Find(text).GetComponent<Camera>();
			componentsInChildren[i].planeDistance -= depth;
		}
		depth++;
	}

	private void Update()
	{
	}
}
