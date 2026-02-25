using System.Text;
using UnityEngine;

namespace Mix.Native
{
	public class NativeAndroidKeyboard : NativeKeyboard
	{
		private AndroidJavaClass JavaClass;

		public NativeAndroidKeyboard()
		{
			Initialize();
		}

		public override void Initialize()
		{
			JavaClass = new AndroidJavaClass("com.disney.nativekeyboard.NativeKeyboard");
		}

		public override void ShowKeyboard(INativeKeyboardEvents aCaller, NativeKeyboardAlignment aAlignment = NativeKeyboardAlignment.Left, NativeKeyboardEntryType aKeyboardType = NativeKeyboardEntryType.Default, NativeKeyboardReturnKey aReturnKeyType = NativeKeyboardReturnKey.Done, int aMaxCharacters = 0, bool aShowSuggestions = false, bool aMultipleLines = false, bool aPassword = false)
		{
			if (caller != null && caller != aCaller)
			{
				caller.OnNativeKeyboardHiding();
				caller.OnNativeKeyboardHidden();
			}
			caller = aCaller;
			JavaClass.CallStatic("ShowKeyboard", (int)aAlignment, (int)aKeyboardType, (int)aReturnKeyType, aMaxCharacters, aShowSuggestions, aMultipleLines, aPassword);
		}

		public override void ShowInput(string aPrePopulatedText, string aDefaultText, int aXPos, int aYPos, int aWidth, int aHeight, int aCanvasHeight, int aFontSize = 20, string aFontColor = "#000000", string aFontFace = "")
		{
			aFontSize = (int)((float)aFontSize * ((float)Singleton<SettingsManager>.Instance.GetScreenHeight() / MixConstants.CANVAS_HEIGHT));
			JavaClass.CallStatic("ShowInput", Encoding.UTF32.GetBytes(aPrePopulatedText), Encoding.UTF32.GetBytes(aDefaultText), aXPos, aYPos, aWidth, aHeight, aCanvasHeight, aFontSize, aFontColor);
		}

		public override void SetText(string aText)
		{
			JavaClass.CallStatic("SetInputText", Encoding.UTF32.GetBytes(aText));
		}

		public override void Reposition(int aXPos, int aYPos, int aWidth, int aHeight)
		{
			JavaClass.CallStatic("RepositionInput", aXPos, aYPos, aWidth, aHeight);
		}

		public override void Hide()
		{
			JavaClass.CallStatic("HideKeyboard");
		}

		public override void SetClipboardText(string aText)
		{
			JavaClass.CallStatic("SetClipboardText", Encoding.UTF32.GetBytes(aText));
		}
	}
}
