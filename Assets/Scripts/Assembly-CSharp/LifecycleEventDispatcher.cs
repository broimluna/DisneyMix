using Mix;

public class LifecycleEventDispatcher : MonoSingleton<LifecycleEventDispatcher>
{
	public delegate void OnApplicationFocusDelegate(bool focus);

	public delegate void OnApplicationPauseDelegate(bool pause);

	public delegate void OnApplicationQuitDelegate();

	public event OnApplicationFocusDelegate OnApplicationFocusEvents;

	public event OnApplicationPauseDelegate OnApplicationPauseEvents;

	public event OnApplicationQuitDelegate OnApplicationQuitEvents;

	public void Init()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
	}

	private void OnApplicationFocus(bool focus)
	{
		if (this.OnApplicationFocusEvents != null)
		{
			this.OnApplicationFocusEvents(focus);
		}
	}

	private void OnApplicationPause(bool pauseState)
	{
		if (this.OnApplicationPauseEvents != null)
		{
			this.OnApplicationPauseEvents(pauseState);
		}
	}

	private void OnApplicationQuit()
	{
		if (this.OnApplicationQuitEvents != null)
		{
			this.OnApplicationQuitEvents();
		}
	}
}
