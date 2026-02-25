using Disney.Mix.SDK;
using Mix.Localization;
using Mix.Native;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Ui
{
	public class ChatBar : MonoBehaviour
	{
		public GameObject SendButton;

		public NativeTextView InputField;

		public KeyboardTray KeyboardTray;

		public GameObject GroupBevel;

		private IChatBar listener;

		private IChatThread clientThread;

		private bool listenersActive;

		private bool disabled;

		private float padding;

		private void Start()
		{
			InputField.KeyboardHeightChanged += OnKeyboardHeightChanged;
			MonoSingleton<NativeKeyboardManager>.Instance.Keyboard.OnNativeKeyboardInputHeightChanged += OnKeyboardInputResize;
			MonoSingleton<NativeKeyboardManager>.Instance.Keyboard.OnNativeKeyboardReturnKeyPressed += OnKeyboardReturnKey;
			InputField.TextBGColor = new Color32(246, 246, 246, byte.MaxValue);
			padding = base.gameObject.GetComponent<LayoutElement>().minHeight - InputField.StartHeight;
			listenersActive = true;
		}

		private void Update()
		{
			if (disabled)
			{
				SendButton.SetActive(false);
			}
			else
			{
				SendButton.SetActive(!InputField.Value.Equals(string.Empty));
			}
		}

		private void OnDestroy()
		{
			if (InputField != null)
			{
				InputField.KeyboardHeightChanged -= OnKeyboardHeightChanged;
			}
			if (MonoSingleton<NativeKeyboardManager>.Instance != null)
			{
				MonoSingleton<NativeKeyboardManager>.Instance.Keyboard.OnNativeKeyboardInputHeightChanged -= OnKeyboardInputResize;
				MonoSingleton<NativeKeyboardManager>.Instance.Keyboard.OnNativeKeyboardReturnKeyPressed -= OnKeyboardReturnKey;
			}
			listenersActive = false;
		}

		public void Init(IChatBar aListener, IChatThread aClientThread)
		{
			listener = aListener;
			clientThread = aClientThread;
			if (clientThread is IGroupChatThread)
			{
				GroupBevel.SetActive(true);
			}
		}

		public void ToggleState(bool aState)
		{
			if (disabled == !aState)
			{
				return;
			}
			if (!aState)
			{
				if (clientThread is IOfficialAccountChatThread)
				{
					InputField.Value = Singleton<Localizer>.Instance.getString("customtokens.friends.oa_chat_notfollowing");
				}
				else
				{
					InputField.Value = Singleton<Localizer>.Instance.getString("customtokens.chat.no_longer_friends");
				}
			}
			else if (disabled)
			{
				InputField.Value = string.Empty;
			}
			InputField.interactable = aState;
			disabled = !aState;
		}

		public void ToggleListeners(bool aState)
		{
			listenersActive = aState;
		}

		public void ToggleInput(bool aState)
		{
			if (aState)
			{
				if (!InputField.SelectInput())
				{
					InputField.Invoke("SelectInput", 0.5f);
				}
			}
			else
			{
				MonoSingleton<NativeKeyboardManager>.Instance.Keyboard.Hide();
			}
		}

		public void SendChat()
		{
			string text = InputField.Value.TrimEnd(' ', '\n').TrimStart(' ', '\n');
			if (text.Length >= 1 && !InputField.DefaultText.Equals(text))
			{
				Singleton<SoundManager>.Instance.PlaySoundEvent("MainApp/UI/SFX_4_SendChat");
				listener.OnSendTextMessage(text);
				Analytics.LogSendChat(clientThread);
				InputField.Value = string.Empty;
				OnKeyboardInputResize(null, new NativeKeyboardInputHeightChangedEventArgs((int)(InputField.StartHeight / Singleton<SettingsManager>.Instance.GetHeightScale())));
			}
		}

		public void OnChatInputClicked()
		{
			ToggleInput(true);
		}

		public void UpdateState(ChatController.UIState aState)
		{
			base.gameObject.SetActive(aState.Equals(ChatController.UIState.Default) || aState.Equals(ChatController.UIState.KeyboardTray) || aState.Equals(ChatController.UIState.GamePausedTray));
		}

		private void OnKeyboardInputResize(object sender, NativeKeyboardInputHeightChangedEventArgs args)
		{
			if (listenersActive)
			{
				float num = padding + (float)args.Height * Singleton<SettingsManager>.Instance.GetHeightScale();
				base.gameObject.GetComponent<LayoutElement>().minHeight = ((!(num < InputField.StartHeight)) ? num : InputField.StartHeight);
			}
		}

		private void OnKeyboardReturnKey(object sender, NativeKeyboardReturnKeyPressedEventArgs args)
		{
			SendChat();
		}

		private void OnKeyboardHeightChanged(NativeTextView aField, int aHeight)
		{
			if (aHeight > 0 && listener != null)
			{
				listener.OnHidePreviewPanel();
			}
			if (listenersActive)
			{
				KeyboardTray.SetSize(aHeight);
				if (aField.Selected)
				{
					listener.OnKeyboardShown();
				}
				else
				{
					listener.OnKeyboardHidden();
				}
				InputField.Reposition();
			}
		}
	}
}
