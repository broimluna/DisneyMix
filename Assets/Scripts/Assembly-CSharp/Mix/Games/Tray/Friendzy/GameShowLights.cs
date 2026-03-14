using UnityEngine;

namespace Mix.Games.Tray.Friendzy
{
	public class GameShowLights : MonoBehaviour
	{
		public Material[] LightMaterials;

		public Texture[] LightTextures;

		public float BlinkInterval = 0.75f;

		private float mNextUpdateTime;

		private int mTextureIndex;

		private void Start()
		{
			mNextUpdateTime = Time.time + BlinkInterval;
			mTextureIndex = 0;
		}

		private void Update()
		{
			if (Time.time >= mNextUpdateTime)
			{
				for (int i = 0; i < LightMaterials.Length; i++)
				{
					LightMaterials[i].mainTexture = LightTextures[mTextureIndex];
				}
				mNextUpdateTime = Time.time + BlinkInterval;
				if (++mTextureIndex >= LightTextures.Length)
				{
					mTextureIndex = 0;
				}
			}
		}
	}
}
