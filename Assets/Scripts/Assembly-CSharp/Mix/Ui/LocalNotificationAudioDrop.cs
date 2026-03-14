using System.Collections;
using Mix.AssetBundles;
using Mix.Assets;
using Mix.Data;
using UnityEngine;

namespace Mix.Ui
{
	public class LocalNotificationAudioDrop : IBundleObject
	{
		private GameObject gameObject;

		private AudioSource audioSource;

		private Sticker sticker;

		public LocalNotificationAudioDrop(Sticker aSticker)
		{
			sticker = aSticker;
			MonoSingleton<AssetManager>.Instance.LoadABundle(this, sticker.GetHd(), sticker, string.Empty, true, false, true);
		}

		void IBundleObject.OnBundleAssetObject(Object aGameObject, object aUserData)
		{
			if (this == null || !(aUserData is Sticker))
			{
				return;
			}
			gameObject = MonoSingleton<AssetManager>.Instance.GetBundleInstance(sticker.GetHd()) as GameObject;
			if (gameObject != null)
			{
				audioSource = gameObject.GetComponent<AudioSource>();
				if (audioSource == null)
				{
					MonoSingleton<AssetManager>.Instance.DestroyBundleInstance(sticker.GetHd(), gameObject);
					return;
				}
				audioSource.Play();
				MonoSingleton<AssetManager>.Instance.StartCoroutine(WaitForSoundComplete());
			}
		}

		private IEnumerator WaitForSoundComplete()
		{
			yield return new WaitForSeconds(0.2f);
			while (audioSource.isPlaying)
			{
				yield return new WaitForSeconds(0.2f);
			}
			MonoSingleton<AssetManager>.Instance.DestroyBundleInstance(sticker.GetHd(), gameObject);
		}
	}
}
