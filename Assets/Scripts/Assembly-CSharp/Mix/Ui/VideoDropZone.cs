using Disney.Native;
using Mix.Games;
using Mix.Native;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.UI
{
	public class VideoDropZone : MonoBehaviour
	{
		private const string ANIM_STATE_OUTRO = "Outro";

		private const string ANIM_PARAM_HIDE = "Hide";

		private const string ANIM_PARAM_FADE = "Fade";

		public Image ImageObject;

		private Rect dropZoneRect;

		private Animator animController;

		private Rect lastVideoPos = default(Rect);

		public void Awake()
		{
			MonoSingleton<NativeVideoPlaybackManager>.Instance.Native.OnVideoDragging += HandleOnVideoDragging;
			MonoSingleton<NativeVideoPlaybackManager>.Instance.Native.OnVideoRectUpdated += HandleOnVideoRectUpdated;
			MonoSingleton<NativeVideoPlaybackManager>.Instance.Native.OnFullScreenToggled += HandleOnFullScreenToggled;
			animController = ImageObject.GetComponent<Animator>();
		}

		private void HandleOnFullScreenToggled(object aSender, NativeVideoPlaybackFullScreenToggledEventArgs aArgs)
		{
			if (!this.IsNullOrDisposed() && aArgs.IsFullScreen)
			{
				MonoSingleton<NativeKeyboardManager>.Instance.Keyboard.Hide();
				MonoSingleton<GameManager>.Instance.PauseGameSession();
			}
		}

		private void HandleOnVideoRectUpdated(object aSender, NativeVideoPlaybackRectUpdateEventArgs aArgs)
		{
			if (!this.IsNullOrDisposed() && aArgs.Visible)
			{
				lastVideoPos = aArgs.VideoRect;
			}
		}

		private void HandleOnVideoDragging(object aSender, NativeVideoPlaybackDraggingEventArgs aArgs)
		{
			if (this.IsNullOrDisposed())
			{
				return;
			}
			switch (aArgs.State)
			{
			case DragState.Start:
			{
				if (!MonoSingleton<NativeKeyboardManager>.Instance.Keyboard.KeyboardIsShowing)
				{
					ImageObject.gameObject.SetActive(true);
					if (animController.GetCurrentAnimatorStateInfo(0).IsName("Outro"))
					{
						animController.SetBool("Hide", false);
					}
				}
				int num = (int)(MixConstants.CANVAS_HEIGHT - (float)MonoSingleton<NativeKeyboardManager>.Instance.Keyboard.KeyboardCurrentHeight * Singleton<SettingsManager>.Instance.GetHeightScale());
				MonoSingleton<NativeVideoPlaybackManager>.Instance.Native.UpdateDragBounds(0, 0, (int)(MixConstants.CANVAS_WIDTH / Singleton<SettingsManager>.Instance.GetWidthScale()), (int)((float)num / Singleton<SettingsManager>.Instance.GetHeightScale()));
				dropZoneRect = Util.GetRectInScreenSpace(ImageObject.GetComponent<RectTransform>());
				MonoSingleton<GameManager>.Instance.PauseGameSession();
				break;
			}
			case DragState.Dragging:
				if (!ImageObject.gameObject.activeSelf)
				{
					ImageObject.gameObject.SetActive(true);
				}
				if (IsOverDropZone(aArgs.Position))
				{
					animController.SetBool("Fade", true);
				}
				else
				{
					animController.SetBool("Fade", false);
				}
				break;
			case DragState.End:
				if (!MonoSingleton<NativeKeyboardManager>.Instance.Keyboard.KeyboardIsShowing)
				{
					animController.SetBool("Hide", true);
					animController.SetBool("Fade", false);
					if (IsOverDropZone(aArgs.Position))
					{
						GameObject original = Resources.Load<GameObject>("Prefabs/Screens/ChatMix/PictureInPicture_Screen");
						GameObject gameObject = Object.Instantiate(original);
						Transform child = gameObject.transform.GetChild(0);
						gameObject.transform.SetParent(GameObject.Find("Persistent_Holder").transform, false);
						gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(lastVideoPos.x * Singleton<SettingsManager>.Instance.GetWidthScale(), 0f - lastVideoPos.y * Singleton<SettingsManager>.Instance.GetHeightScale());
						gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(lastVideoPos.width * Singleton<SettingsManager>.Instance.GetWidthScale(), lastVideoPos.height * Singleton<SettingsManager>.Instance.GetHeightScale());
						child.GetComponent<Animator>().Play("Close");
						MonoSingleton<NativeVideoPlaybackManager>.Instance.Native.Unload();
					}
				}
				break;
			}
		}

		private bool IsOverDropZone(Vector2 aPosition)
		{
			return dropZoneRect.Contains(aPosition);
		}
	}
}
