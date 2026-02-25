using System.Collections.Generic;
using UnityEngine;

public class MemoryLeakTest : MonoBehaviour
{
	private List<Texture2D> textures;

	private void Start()
	{
		textures = new List<Texture2D>();
	}

	private void Update()
	{
		Texture2D item = new Texture2D(2048, 2048, TextureFormat.ARGB32, false);
		textures.Add(item);
	}

	public void ClearTextures()
	{
		foreach (Texture2D texture in textures)
		{
			Object.Destroy(texture);
		}
		textures.Clear();
	}
}
