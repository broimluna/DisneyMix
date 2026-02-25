using Mix.Native;
using UnityEngine;
using UnityEngine.UI;

namespace Mix.Games.Tray.MemeGenerator
{
	public class MemeCaptionText : MonoBehaviour
	{
		private Text mText;

		private bool mIsDirty;

		private Outline highlightedTextComponent;

		private bool mIsActive;

		private string mDefaultText;

		private string mCurrentText;

		public NativeTextView mNativeTextView;

		public Color textEnteredColor;

		public Color defaultColor;

		public float activeWobbleFrequency = 5f;

		public float activeWobbleAmount = 1f;

		public string DefaultMessageToken;

		public bool IsDirty
		{
			get
			{
				return mIsDirty;
			}
		}

		public string Caption
		{
			get
			{
				return mCurrentText;
			}
			set
			{
				if (value == null)
				{
					mCurrentText = string.Empty;
				}
				else
				{
					mCurrentText = value;
				}
				string text = mCurrentText;
				if (string.IsNullOrEmpty(text))
				{
					text = mDefaultText;
				}
				if (mText.text != text)
				{
					mText.text = text;
					mIsDirty = true;
				}
			}
		}

		public bool MatchesSelected(Text aSelected)
		{
			return mText == aSelected;
		}

		public void SetActive(bool aIsActive)
		{
			if (aIsActive)
			{
				mText.color = textEnteredColor;
				highlightedTextComponent.enabled = true;
				mNativeTextView.gameObject.SetActive(true);
				if (IsEmpty())
				{
					mNativeTextView.Value = string.Empty;
				}
				else
				{
					mNativeTextView.Value = Caption;
				}
				mNativeTextView.SelectInput();
				mNativeTextView.KeyboardValueChanged += OnKeyboardValueChanged;
			}
			else
			{
				if (IsEmpty())
				{
					Caption = string.Empty;
					mText.color = defaultColor;
				}
				highlightedTextComponent.enabled = false;
				mNativeTextView.gameObject.SetActive(false);
				mNativeTextView.KeyboardValueChanged -= OnKeyboardValueChanged;
			}
			mIsActive = aIsActive;
		}

		public bool IsEmpty()
		{
			if (string.IsNullOrEmpty(mCurrentText.Trim()))
			{
				return true;
			}
			return false;
		}

		public void PostModeration()
		{
			mIsDirty = false;
		}

		public void InitializeHighlightOutline(Color aColor, Vector2 aOffset)
		{
			highlightedTextComponent = base.gameObject.AddComponent<Outline>();
			highlightedTextComponent.effectColor = aColor;
			highlightedTextComponent.effectDistance = aOffset;
			highlightedTextComponent.enabled = false;
		}

		public void Reset()
		{
			Caption = string.Empty;
			SetActive(false);
		}

		private void OnKeyboardValueChanged(NativeTextView aInput, string aValue)
		{
			Caption = aValue.ToUpper();
		}

		private void Awake()
		{
			mText = GetComponent<Text>();
			mDefaultText = BaseGameController.Instance.Session.GetLocalizedString(DefaultMessageToken);
			Caption = string.Empty;
		}

		private void Update()
		{
			if (mIsActive)
			{
				base.transform.localEulerAngles = new Vector3(0f, 0f, Mathf.Sin(Time.time * activeWobbleFrequency) * activeWobbleAmount);
				if (!mNativeTextView.IsNullOrDisposed() && !string.Equals(Caption, mNativeTextView.Value))
				{
					Caption = mNativeTextView.Value;
				}
			}
			else
			{
				base.transform.localEulerAngles = Vector3.zero;
			}
		}

		private void OnDestroy()
		{
			if (highlightedTextComponent != null)
			{
				Object.Destroy(highlightedTextComponent);
				highlightedTextComponent = null;
			}
		}
	}
}
