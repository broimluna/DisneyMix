using Mix;

namespace Disney.Native
{
	public class NativeCameraManager : MonoSingleton<NativeCameraManager>
	{
		private NativeCamera native;

		public NativeCamera Native
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
			native = base.gameObject.AddComponent<NativeAndroidCamera>();
		}

		public void Init()
		{
			base.gameObject.name = "NativeCameraManager";
		}

		public void OnApplicationPause(bool aState)
		{
			if (aState)
			{
				Native.OnApplicationPaused();
			}
			else
			{
				Native.OnApplicationResumed();
			}
		}
	}
}
