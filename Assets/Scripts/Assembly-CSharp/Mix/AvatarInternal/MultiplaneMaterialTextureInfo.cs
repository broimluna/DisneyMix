using System.Collections.Generic;
using UnityEngine;

namespace Mix.AvatarInternal
{
	public class MultiplaneMaterialTextureInfo
	{
		private Material mat;

		public Dictionary<string, KeyValuePair<string, Texture2D>> propertyTextures = new Dictionary<string, KeyValuePair<string, Texture2D>>();

		public MultiplaneMaterialTextureInfo(Material mat)
		{
			this.mat = mat;
		}

		public void Cleanup()
		{
			List<string> list = new List<string>();
			foreach (KeyValuePair<string, KeyValuePair<string, Texture2D>> propertyTexture in propertyTextures)
			{
				list.Add(propertyTexture.Key);
			}
			foreach (string item in list)
			{
				CleanupProp(item);
			}
		}

		public void AddProp(string prop, Texture2D tex, string id)
		{
			if (!propertyTextures.ContainsKey(prop) || string.IsNullOrEmpty(id) || !(propertyTextures[prop].Key == id))
			{
				CleanupProp(prop);
				propertyTextures.Add(prop, new KeyValuePair<string, Texture2D>(id, tex));
			}
		}

		public void CleanupProp(string prop)
		{
			if (propertyTextures.ContainsKey(prop))
			{
				MultiplaneMemCache.RemoveTexture(propertyTextures[prop].Key);
				propertyTextures.Remove(prop);
				mat.SetTexture(prop, null);
			}
		}
	}
}
