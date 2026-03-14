namespace Disney.Mix.SDK.Internal
{
	internal class NicknameChangedEventArgs : AbstractNicknameChangedEventArgs
	{
		public NicknameChangedEventArgs(IUserNickname nickname)
		{
			base.Nickname = nickname;
		}
	}
}
