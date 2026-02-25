public class OfflineQueueItem
{
	public long index;

	public string type;

	public string data;

	public bool unique { get; private set; }

	public OfflineQueueItem(string data, bool unique = true)
	{
		this.data = data;
		this.unique = unique;
	}

	public virtual bool Process()
	{
		return false;
	}
}
