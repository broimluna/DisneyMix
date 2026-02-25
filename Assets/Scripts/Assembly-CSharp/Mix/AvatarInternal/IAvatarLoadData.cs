using UnityEngine;

namespace Mix.AvatarInternal
{
	public interface IAvatarLoadData
	{
		void TexturesLoaded(Texture aDiffuse, Texture aNormal);

		void LoadError();
	}
}
