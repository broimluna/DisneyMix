namespace Disney.Mix.SDK.Internal
{
	public class UserNickname : IInternalUserNickname, IUserNickname
	{
		public string Text { get; private set; }

		public bool Applied { get; private set; }

		public UserNickname(string text)
		{
			Text = text;
		}

		public void ApplyFinished()
		{
			Applied = true;
		}
	}
}
