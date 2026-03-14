namespace Mix.Assets
{
	public interface ICpipeReady
	{
		void OnCpipeReady(CpipeEvent cpipeEvent);

		void OnCpipeFail(CpipeEvent cpipeEvent);
	}
}
