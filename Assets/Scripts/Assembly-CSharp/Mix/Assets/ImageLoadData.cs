using UnityEngine;

namespace Mix.Assets
{
	public class ImageLoadData
	{
		public OnImageLoaded callback;

		public Texture2D texture;

		public ImageLoadData(OnImageLoaded aCallback, Texture2D aTexture)
		{
			callback = aCallback;
			texture = aTexture;
		}
	}
}
