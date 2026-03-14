using Mix;

namespace Disney.Native
{
	public class NativeVideoPlaybackManager : MonoSingleton<NativeVideoPlaybackManager>
	{
		private NativeVideoPlayback native;

		public NativeVideoPlayback Native
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
			native = base.gameObject.AddComponent<NativeAndroidVideoPlayback>();
		}

		public void Init()
		{
			base.gameObject.name = "NativeVideoPlayback";
		}

		private void OnApplicationPause(bool aState)
		{
			Native.Pause(aState);
		}
	}
}
