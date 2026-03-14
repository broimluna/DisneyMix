using System;
using System.Collections.Generic;
using Mix.Games.Session;
using UnityEngine;

namespace Mix.Games.Avatar
{
	public class FakeAvatarManager : MonoBehaviour, IGameAvatar
	{
		private class AvatarProperties
		{
			public Texture2D FaceTex;

			public Texture2D BrowTex;

			public Texture2D EyeTex;

			public Texture2D MouthTex;
		}

		private const int FACE_BYTE = 0;

		private const int BROW_BYTE = 1;

		private const int EYE_BYTE = 2;

		private const int MOUTH_BYTE = 3;

		public List<Texture2D> Faces;

		public List<Texture2D> Brows;

		public List<Texture2D> Eyes;

		public List<Texture2D> Mouths;

		private static FakeAvatarManager mInstance;

		public static FakeAvatarManager Instance
		{
			get
			{
				return mInstance;
			}
		}

		private void Awake()
		{
			if (mInstance == null)
			{
				mInstance = this;
			}
		}

		public void PreloadAvatar(string aId, IGameThreadParameters threadParams)
		{
		}

		public void LoadFriend(GameObject aTarget, string aId, IGameThreadParameters aThreadParams, bool aHideCostume, bool aHideGeoAccessories)
		{
		}

		public void LoadRandomFriend(GameObject aTarget)
		{
		}

		public void LoadSnapshot(string aId, IGameThreadParameters aThreadParams, int aSpriteSize, Action<bool, Sprite> aCallback)
		{
			if (aCallback != null)
			{
				aCallback(false, null);
			}
		}

		private void SetTextures(GameObject aTarget, AvatarProperties aProperties)
		{
			FakeAvatarDna fakeAvatarDna = aTarget.AddComponent<FakeAvatarDna>();
			fakeAvatarDna.FaceTex = aProperties.FaceTex;
			fakeAvatarDna.BrowTex = aProperties.BrowTex;
			fakeAvatarDna.EyeTex = aProperties.EyeTex;
			fakeAvatarDna.MouthTex = aProperties.MouthTex;
		}

		private AvatarProperties GetPropertiesFromId(string aId)
		{
			int hashCode = aId.GetHashCode();
			AvatarProperties avatarProperties = new AvatarProperties();
			int partIndex = GetPartIndex(hashCode, 0, Faces.Count);
			avatarProperties.FaceTex = Faces[partIndex];
			partIndex = GetPartIndex(hashCode, 1, Brows.Count);
			avatarProperties.BrowTex = Brows[partIndex];
			partIndex = GetPartIndex(hashCode, 2, Eyes.Count);
			avatarProperties.EyeTex = Eyes[partIndex];
			partIndex = GetPartIndex(hashCode, 3, Mouths.Count);
			avatarProperties.MouthTex = Mouths[partIndex];
			return avatarProperties;
		}

		private int GetPartIndex(int aHashCode, int aByteNum, int aNumItems)
		{
			int num = (aHashCode >> 8 * aByteNum) & 0xFF;
			return num % aNumItems;
		}
	}
}
