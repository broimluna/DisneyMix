namespace Mix.Assets.Worker
{
	public interface ISaveFile
	{
		void OnSaveFile(bool success, string path, object aUserData);
	}
}
