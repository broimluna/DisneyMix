namespace Mix.Assets
{
	public interface IFlow
	{
		void ParseNextFlowState();

		bool IsPathLocalForIsDemoMode(string aPath);
	}
}
