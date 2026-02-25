namespace Disney.Mix.SDK.Internal
{
	public interface IInternalUnidentifiedUser : IUnidentifiedUser
	{
		IInternalAvatar InternalAvatar { get; }

		void DispatchOnAvatarChanged();
	}
}
