namespace Mix.Assets.Worker
{
	public interface ICheckForRecord
	{
		void OnCheckForRecord(bool aIsInDB, object aUserData);
	}
}
