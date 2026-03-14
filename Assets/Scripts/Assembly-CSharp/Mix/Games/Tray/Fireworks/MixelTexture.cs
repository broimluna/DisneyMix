using Avatar.DataTypes;
using UnityEngine;

namespace Mix.Games.Tray.Fireworks
{
	public class MixelTexture : MonoBehaviour
	{
		private void SkinCallback(AvatarTextureData textureData)
		{
			GetComponent<FireworkT>().TextureFirework.Texture = textureData.diffuse;
		}
	}
}
