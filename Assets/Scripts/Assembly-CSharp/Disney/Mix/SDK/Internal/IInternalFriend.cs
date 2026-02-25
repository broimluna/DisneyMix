namespace Disney.Mix.SDK.Internal
{
	public interface IInternalFriend : IFriend
	{
		string Swid { get; }

		IInternalAvatar InternalAvatar { get; }

		void ChangeTrust(bool isTrusted);

		void UpdateNickname(IUserNickname nickname);

		void DispatchOnAvatarChanged();
	}
}
