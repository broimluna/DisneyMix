namespace Mix.Assets.Worker
{
	public interface IUpdateAccessCount
	{
		void OnUpdateAccessCount(int aRowsAffected, object aUserData);
	}
}
