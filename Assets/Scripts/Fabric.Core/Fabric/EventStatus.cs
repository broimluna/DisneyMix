namespace Fabric
{
	public enum EventStatus
	{
		Idle = 0,
		InQueue = 1,
		Handled = 2,
		Handled_Virtualized = 3,
		Not_Handled = 4,
		Not_Handled_MinimumPlaybackInterval = 5,
		Not_Handled_Probability = 6,
		Not_Handled_VolumeThreshold = 7,
		Failed_Uknown = 8,
		Failed_Invalid_Instance = 9,
		Failed_No_Listeners = 10,
		Failed_Invalid_GameObject = 11,
		Failed_SetProperty = 12,
		Failed_MaxInstancesLimit = 13,
		Failed_MaxAudioComponentsLimit = 14
	}
}
