using System.Text;
using UnityEngine;

namespace Mix.Native
{
    public class NativeAndroidKeyboard : NativeKeyboard
    {
        private AndroidJavaClass JavaClass;
        private bool isAvailable;

        public NativeAndroidKeyboard()
        {
            Initialize();
        }

        public override void Initialize()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
			try
			{
				Debug.Log("[NativeAndroidKeyboard] Initializing Java plugin");
				JavaClass = new AndroidJavaClass("com.disney.nativekeyboard.NativeKeyboard");
				isAvailable = JavaClass != null;
				if (!isAvailable)
				{
					Debug.LogWarning("[NativeAndroidKeyboard] JavaClass is null, disabling native keyboard.");
				}
			}
			catch (AndroidJavaException ex)
			{
				Debug.LogWarning("[NativeAndroidKeyboard] Failed to create Java plugin, disabling. Exception: " + ex);
				isAvailable = false;
				JavaClass = null;
			}
#else
            isAvailable = false;
            JavaClass = null;
#endif
        }

        public override void ShowKeyboard(INativeKeyboardEvents aCaller, NativeKeyboardAlignment aAlignment = NativeKeyboardAlignment.Left, NativeKeyboardEntryType aKeyboardType = NativeKeyboardEntryType.Default, NativeKeyboardReturnKey aReturnKeyType = NativeKeyboardReturnKey.Done, int aMaxCharacters = 0, bool aShowSuggestions = false, bool aMultipleLines = false, bool aPassword = false)
        {
            if (!isAvailable || JavaClass == null)
            {
                return;
            }
            if (caller != null && caller != aCaller)
            {
                caller.OnNativeKeyboardHiding();
                caller.OnNativeKeyboardHidden();
            }
            caller = aCaller;
            try
            {
                JavaClass.CallStatic("ShowKeyboard", (int)aAlignment, (int)aKeyboardType, (int)aReturnKeyType, aMaxCharacters, aShowSuggestions, aMultipleLines, aPassword);
            }
            catch (AndroidJavaException ex)
            {
                Debug.LogWarning("[NativeAndroidKeyboard] ShowKeyboard failed: " + ex);
            }
        }

        public override void ShowInput(string aPrePopulatedText, string aDefaultText, int aXPos, int aYPos, int aWidth, int aHeight, int aCanvasHeight, int aFontSize = 20, string aFontColor = "#000000", string aFontFace = "")
        {
            if (!isAvailable || JavaClass == null)
            {
                return;
            }
            try
            {
                aFontSize = (int)((float)aFontSize * ((float)Singleton<SettingsManager>.Instance.GetScreenHeight() / MixConstants.CANVAS_HEIGHT));
                JavaClass.CallStatic(
                    "ShowInput",
                    Encoding.UTF32.GetBytes(aPrePopulatedText ?? string.Empty),
                    Encoding.UTF32.GetBytes(aDefaultText ?? string.Empty),
                    aXPos,
                    aYPos,
                    aWidth,
                    aHeight,
                    aCanvasHeight,
                    aFontSize,
                    aFontColor ?? "#000000"
                );
            }
            catch (AndroidJavaException ex)
            {
                Debug.LogWarning("[NativeAndroidKeyboard] ShowInput failed: " + ex);
            }
        }

        public override void SetText(string aText)
        {
            if (!isAvailable || JavaClass == null)
            {
                return;
            }
            try
            {
                JavaClass.CallStatic("SetInputText", Encoding.UTF32.GetBytes(aText ?? string.Empty));
            }
            catch (AndroidJavaException ex)
            {
                Debug.LogWarning("[NativeAndroidKeyboard] SetText failed: " + ex);
            }
        }

        public override void Reposition(int aXPos, int aYPos, int aWidth, int aHeight)
        {
            if (!isAvailable || JavaClass == null)
            {
                return;
            }
            try
            {
                JavaClass.CallStatic("RepositionInput", aXPos, aYPos, aWidth, aHeight);
            }
            catch (AndroidJavaException ex)
            {
                Debug.LogWarning("[NativeAndroidKeyboard] Reposition failed: " + ex);
            }
        }

        public override void Hide()
        {
            if (!isAvailable || JavaClass == null)
            {
                return;
            }
            try
            {
                JavaClass.CallStatic("HideKeyboard");
            }
            catch (AndroidJavaException ex)
            {
                Debug.LogWarning("[NativeAndroidKeyboard] Hide failed: " + ex);
            }
        }

        public override void SetClipboardText(string aText)
        {
            if (!isAvailable || JavaClass == null)
            {
                return;
            }
            try
            {
                JavaClass.CallStatic("SetClipboardText", Encoding.UTF32.GetBytes(aText ?? string.Empty));
            }
            catch (AndroidJavaException ex)
            {
                Debug.LogWarning("[NativeAndroidKeyboard] SetClipboardText failed: " + ex);
            }
        }
    }
}