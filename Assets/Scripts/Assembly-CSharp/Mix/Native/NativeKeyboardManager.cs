namespace Mix.Native
{
	public class NativeKeyboardManager : MonoSingleton<NativeKeyboardManager>
	{
		private NativeKeyboard keyboard;

		public NativeKeyboard Keyboard
		{
			get
			{
				return keyboard;
			}
			private set
			{
				keyboard = value;
			}
		}

		public void Init()
		{
			base.gameObject.name = "NativeKeyboard";
		}

		protected void Awake()
		{
			Keyboard = base.gameObject.AddComponent<NativeAndroidKeyboard>();
		}
	}
}
