namespace Disney.Mix.SDK.Internal
{
	internal class FindUserResult : IFindUserResult
	{
		private readonly IInternalUnidentifiedUser user;

		public bool Success { get; private set; }

		public IUnidentifiedUser User
		{
			get
			{
				return user;
			}
		}

		internal FindUserResult(bool success, IInternalUnidentifiedUser user)
		{
			Success = success;
			this.user = user;
		}
	}
}
