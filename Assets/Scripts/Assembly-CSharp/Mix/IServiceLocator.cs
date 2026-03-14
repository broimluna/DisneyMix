namespace Mix
{
	public interface IServiceLocator
	{
		T GetService<T>() where T : new();

		void SetService<T>(T input) where T : new();
	}
}
