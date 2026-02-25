namespace Disney.Mix.SDK.Internal
{
	public interface IInternalRemoteChatMember : IRemoteChatMember
	{
		string Swid { get; }

		IInternalAvatar InternalAvatar { get; }

		void DispatchOnAvatarChanged();

		void UpdateNames(string displayName, string firstName);
	}
}
