using System.Collections.Generic;
using UnityEngine;

namespace Mix.AvatarInternal
{
	public class AvatarTextureTracker
	{
		public static Dictionary<Texture, int> textureRefs = new Dictionary<Texture, int>();

		public static void AddTextureReference(Texture texture)
		{
			if (texture != null && textureRefs.ContainsKey(texture))
			{
				Dictionary<Texture, int> dictionary2;
				Dictionary<Texture, int> dictionary = (dictionary2 = textureRefs);
				Texture key2;
				Texture key = (key2 = texture);
				int num = dictionary2[key2];
				dictionary[key] = num + 1;
			}
			else if (texture != null)
			{
				textureRefs.Add(texture, 1);
			}
		}

		public static bool DecrementTextureReference(Texture texture)
		{
			if (texture != null && textureRefs.ContainsKey(texture))
			{
				Dictionary<Texture, int> dictionary2;
				Dictionary<Texture, int> dictionary = (dictionary2 = textureRefs);
				Texture key2;
				Texture key = (key2 = texture);
				int num = dictionary2[key2];
				dictionary[key] = num - 1;
				if (textureRefs[texture] == 0)
				{
					textureRefs.Remove(texture);
					return true;
				}
			}
			return false;
		}
	}
}
