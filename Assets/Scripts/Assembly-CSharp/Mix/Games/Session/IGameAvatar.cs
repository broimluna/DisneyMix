using System;
using UnityEngine;

namespace Mix.Games.Session
{
	public interface IGameAvatar
	{
		void PreloadAvatar(string id, IGameThreadParameters threadParams);

		void LoadFriend(GameObject aMesh, string id, IGameThreadParameters threadParams, bool aHideCostumes, bool aHideGeoAccessories);

		void LoadSnapshot(string id, IGameThreadParameters threadParams, int spriteSize, Action<bool, Sprite> callback);

		void LoadRandomFriend(GameObject aMesh);
	}
}
