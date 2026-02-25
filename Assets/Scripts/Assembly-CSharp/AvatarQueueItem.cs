using Avatar;
using Disney.Mix.SDK;
using Mix;
using Mix.Avatar;

public class AvatarQueueItem : OfflineQueueItem
{
	public AvatarQueueItem(string data)
		: base(data)
	{
		type = "avatar";
		base.data = data;
	}

	public override bool Process()
	{
		IAvatar currentUsersDna = AvatarApi.DeserializeAvatar(data);
		MonoSingleton<AvatarManager>.Instance.setCurrentUsersDna(currentUsersDna);
		MonoSingleton<AvatarManager>.Instance.saveCurrentUsersDna();
		return true;
	}
}
