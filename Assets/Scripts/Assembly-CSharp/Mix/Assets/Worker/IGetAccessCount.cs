namespace Mix.Assets.Worker
{
	public interface IGetAccessCount
	{
		void OnGetAccessCount(long aAccessCount, object aUserData);
	}
}
