using UnityEngine;

namespace Mix.AvatarInternal
{
	public class AvatarLoadData : IAvatarLoadData
	{
		public enum LoadState
		{
			LOADING = 0,
			LOADED = 1,
			UNLOADED = 2
		}

		public LoadState status;

		public string sha;

		public Texture cachedDiffuse;

		public Texture cachedNormal;

		public AvatarLoadData(string aSha)
		{
			sha = aSha;
			status = LoadState.LOADING;
		}

		public void TexturesLoaded(Texture aDiffuse, Texture aNormal)
		{
			cachedDiffuse = aDiffuse;
			cachedNormal = aNormal;
			status = LoadState.LOADED;
		}

		public void LoadError()
		{
			status = LoadState.UNLOADED;
		}

		public void CleanupTextures()
		{
			if (status == LoadState.LOADED)
			{
				cachedDiffuse = null;
				cachedNormal = null;
				status = LoadState.UNLOADED;
			}
		}
	}
}
