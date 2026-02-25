using UnityEngine;

public class AnimatedTextureController : MonoBehaviour
{
	public string jointName = "ENTER_JOINT_NAME";

	public GameObject jointController;

	private int jointScaleX;

	private int jointScaleY;

	public float offsetRatioX = 0.333f;

	public float offsetRatioY = 0.333f;

	private float currentOffsetX;

	private float previousOffsetX;

	private float currentOffsetY;

	private float previousOffsetY;

	private void Start()
	{
		base.enabled = FindAnimatedTexture();
	}

	private void Update()
	{
		SetAnimatedTextureOffset();
	}

	private bool FindAnimatedTexture()
	{
		Vector2 scale = new Vector2(offsetRatioX, offsetRatioY);
		GetComponent<Renderer>().material.SetTextureScale("_MainTex", scale);
		return true;
	}

	private void SetAnimatedTextureOffset()
	{
		if (jointController.transform.localScale.y % 1f == 0f)
		{
			jointScaleX = Mathf.RoundToInt(jointController.transform.localScale.y);
		}
		if (jointController.transform.localScale.z % 1f == 0f)
		{
			jointScaleY = Mathf.RoundToInt(jointController.transform.localScale.z);
		}
		currentOffsetX = (float)(jointScaleX - 1) * offsetRatioX;
		currentOffsetY = (float)(jointScaleY - 1) * offsetRatioY;
		if (currentOffsetX != previousOffsetX || currentOffsetY != previousOffsetY)
		{
			Vector2 offset = new Vector2(currentOffsetX, currentOffsetY);
			GetComponent<Renderer>().material.SetTextureOffset("_MainTex", offset);
			previousOffsetX = currentOffsetX;
			previousOffsetY = currentOffsetY;
		}
	}
}
