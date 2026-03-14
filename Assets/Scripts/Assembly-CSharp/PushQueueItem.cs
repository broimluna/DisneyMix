using System;
using Mix;

public class PushQueueItem : OfflineQueueItem
{
	public PushQueueItem(string aData)
		: base(aData)
	{
		type = "push";
		data = aData;
	}

	public override bool Process()
	{
		MonoSingleton<PushNotifications>.Instance.ToggleVisiblePushNotifications(Convert.ToBoolean(data));
		return true;
	}
}
