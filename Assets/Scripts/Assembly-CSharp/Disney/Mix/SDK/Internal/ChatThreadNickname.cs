namespace Disney.Mix.SDK.Internal
{
	public class ChatThreadNickname : IInternalChatThreadNickname, IChatThreadNickname
	{
		public string Nickname { get; private set; }

		public bool Applied { get; private set; }

		public ChatThreadNickname(string nickname)
		{
			Nickname = nickname;
		}

		public void ApplyFinished()
		{
			Applied = true;
		}
	}
}
