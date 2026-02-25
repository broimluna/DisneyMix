using Mix;

namespace Disney.Native
{
	public class NativeUtilitiesManager : MonoSingleton<NativeUtilitiesManager>
	{
		public const string PERMISSION_CAMERA = "camera";

		public const string PERMISSION_RECORD_AUDIO = "record_audio";

		public const string PERMISSION_EXTERNAL_STORAGE = "write_external_storage";

		private NativeUtilities native;

		public NativeUtilities Native
		{
			get
			{
				return native;
			}
			private set
			{
				native = value;
			}
		}

		protected void Awake()
		{
			native = base.gameObject.AddComponent<NativeAndroidUtilities>();
		}

		public void Init()
		{
			base.gameObject.name = "NativeUtilitiesManager";
			native.InitNativeUtilities();
		}
	}
}
