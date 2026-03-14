using System;
using Avatar.Interfaces;
using Disney.Mix.SDK;
using Mix.AvatarInternal;
using UnityEngine;

namespace Avatar.Multiplane
{
	public class MultiplanePrefab
	{
		private MonoBehaviour monoEngine;

		private MultiplaneMaterials mats;

		private GameObject avatarObj;

		public MultiplanePrefab(MonoBehaviour aMonoEngine, IAssetManager aAssetManager, GameObject aAvatarObj)
		{
			monoEngine = aMonoEngine;
			avatarObj = aAvatarObj;
			mats = new MultiplaneMaterials(monoEngine, aAssetManager, avatarObj);
		}

		public void CompositeAvatar(IAvatar dna, AvatarFlags flags, Action<bool> callback)
		{
			mats.LoadAvatarTextures(dna, flags, callback);
		}

		public void CancelAvatar()
		{
			mats.hasErrored = true;
		}

		public void ResetAvatarObject(string texturePath, Action callback)
		{
			mats.ResetAvatarHeadToDefault(texturePath, callback);
		}
	}
}
