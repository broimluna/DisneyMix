namespace Mix.Assets.Worker
{
	public interface ILoadFile
	{
		void OnLoadFile(bool success, string path, byte[] bytes, object aUserData);
	}
}
