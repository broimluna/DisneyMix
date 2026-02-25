using UnityEngine;

public class AvatarComponent : MonoBehaviour
{
	public enum CategoryName
	{
		SKIN = 0,
		MOUTH = 1,
		NOSE = 2,
		EYES = 3,
		BROW = 4,
		ACCESSORY = 5,
		HAIR = 6,
		COSTUME = 7,
		GLOW = 8,
		HAT = 9
	}

	public CategoryName Category;

	public int xOffset;

	public int yOffset;

	public bool ignoreCropAndOffset;

	public AvatarComponentLayer[] Layers;

	private void Start()
	{
	}

	private void Update()
	{
	}
}
