using System;
using UnityEngine;

namespace Mix.Games.Session
{
	public interface IGameAssetManager
	{
		void CancelBundles();

		void CancelBundles(string aUrl);

		void DestroyBundleInstance(string aPath, UnityEngine.Object aObject);

		UnityEngine.Object GetBundleInstance(string aPath);

		void LoadAsset(IGameAsset aSessionAsset, string aPath, object aParam);

		void LoadData(GameSession aSession, string aPath, string aFileName, Func<string, object> aMethod);

		bool WillBundleLoadFromWeb(string aPath);
	}
}
