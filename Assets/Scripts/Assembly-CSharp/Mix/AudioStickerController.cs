using Mix.Ui;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Mix
{
	internal class AudioStickerController : MonoBehaviour
	{
		public bool autoplay = true;

		private Button button;

		private UnityAction listener;

		private void Start()
		{
			button = base.gameObject.GetComponent<Button>();
			if (button != null)
			{
				listener = StopOtherAudio;
				button.onClick.AddListener(listener);
				if (autoplay)
				{
					button.onClick.Invoke();
				}
			}
		}

		private void OnDestroy()
		{
			if (button != null && listener != null)
			{
				button.onClick.RemoveListener(listener);
			}
		}

		private void StopOtherAudio()
		{
			ChatController lastProcessedRequestController = MonoSingleton<NavigationManager>.Instance.GetLastProcessedRequestController<ChatController>();
			Singleton<SoundManager>.Instance.StopAllAudioSourcesFromRoot(lastProcessedRequestController.transform, base.gameObject);
		}
	}
}
