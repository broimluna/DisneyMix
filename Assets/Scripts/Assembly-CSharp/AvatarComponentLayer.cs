using System;
using UnityEngine;

[Serializable]
public class AvatarComponentLayer
{
	public enum LayerType
	{
		NONE = 0,
		MULTIPLY = 1,
		SCREEN = 2,
		OVERLAY = 3,
		COLOR = 4,
		IGNORE = 5,
		MASK = 6
	}

	public LayerType BlendMode;

	public Texture2D Texture;

	public Texture2D NormalMap;
}
