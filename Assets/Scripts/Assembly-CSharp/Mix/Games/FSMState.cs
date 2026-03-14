namespace Mix.Games
{
	public class FSMState
	{
		public delegate void UpdateFunction();

		public delegate void EnterFunction();

		public delegate void ExitFunction();

		public UpdateFunction Update;

		public EnterFunction Enter;

		public ExitFunction Exit;

		public FSMState(EnterFunction enter_func, UpdateFunction update_func, ExitFunction exit_func)
		{
			Enter = enter_func;
			Update = update_func;
			Exit = exit_func;
		}
	}
}
