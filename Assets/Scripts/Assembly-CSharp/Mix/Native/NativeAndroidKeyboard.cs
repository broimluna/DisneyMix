using System.Text;
using UnityEngine;

namespace Mix.Native
{
	public class NativeAndroidKeyboard : NativeKeyboard
	{
		public NativeAndroidKeyboard()
		{
			Initialize();
		}

		public override void Initialize()
		{
			// Native implementation removed
		}

		public override void ShowKeyboard(INativeKeyboardEvents aCaller, NativeKeyboardAlignment aAlignment = NativeKeyboardAlignment.Left, NativeKeyboardEntryType aKeyboardType = NativeKeyboardEntryType.Default, NativeKeyboardReturnKey aReturnKeyType = NativeKeyboardReturnKey.Done, int aMaxCharacters = 0, bool aShowSuggestions = false, bool aMultipleLines = false, bool aPassword = false)
		{
			if (caller != null && caller != aCaller)
			{
				caller.OnNativeKeyboardHiding();
				caller.OnNativeKeyboardHidden();
			}
			caller = aCaller;
			
			// JavaClass.CallStatic removed
		}

		public override void ShowInput(string aPrePopulatedText, string aDefaultText, int aXPos, int aYPos, int aWidth, int aHeight, int aCanvasHeight, int aFontSize = 20, string aFontColor = "#000000", string aFontFace = "")
		{
			aFontSize = (int)((float)aFontSize * ((float)Singleton<SettingsManager>.Instance.GetScreenHeight() / MixConstants.CANVAS_HEIGHT));
			
			// JavaClass.CallStatic removed
		}

		public override void SetText(string aText)
		{
			// JavaClass.CallStatic removed
		}

		public override void Reposition(int aXPos, int aYPos, int aWidth, int aHeight)
		{
			// JavaClass.CallStatic removed
		}

		public override void Hide()
		{
			// JavaClass.CallStatic removed
		}

		public override void SetClipboardText(string aText)
		{
			// JavaClass.CallStatic removed
		}
	}
}
