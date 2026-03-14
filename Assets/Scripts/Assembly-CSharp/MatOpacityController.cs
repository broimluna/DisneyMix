using UnityEngine;

public class MatOpacityController : MonoBehaviour
{
	private Material mat;

	private Transform opacityTarget;

	private bool assetsFound;

	public float meshImportScale = 100f;

	private void Start()
	{
		if (base.transform == null || base.transform.parent == null || base.transform.parent.parent == null || GetComponent<Renderer>() == null || base.transform.parent.parent.Find("grp_joints") == null || GetComponent<Renderer>().material == null)
		{
			Debug.LogWarning("Invalid values for transform and renderer, is this script on the right object?");
			return;
		}
		mat = GetComponent<Renderer>().material;
		Transform transform = base.transform.parent.parent.Find("grp_joints");
		Transform transform2 = transform.Find("prop_" + base.name + "_opacity");
		if (transform2 != null)
		{
			opacityTarget = transform2;
			assetsFound = true;
		}
	}

	private void Update()
	{
		if (assetsFound)
		{
			Color color = new Color(mat.color.r, mat.color.g, mat.color.b, opacityTarget.localPosition.y / meshImportScale);
			mat.color = color;
		}
	}
}
