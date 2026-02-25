using System;

namespace Disney.Mix.SDK
{
	public abstract class AbstractNicknameChangedEventArgs : EventArgs
	{
		public IUserNickname Nickname { get; protected set; }
	}
}
