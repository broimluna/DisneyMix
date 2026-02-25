namespace Fabric
{
	public enum AudioComponentState
	{
		WaitingToPlay = 0,
		Playing = 1,
		WaitingToStop = 2,
		ScheduledToStop = 3,
		Stopped = 4,
		Paused = 5,
		Virtual = 6,
		LostFocus = 7
	}
}
