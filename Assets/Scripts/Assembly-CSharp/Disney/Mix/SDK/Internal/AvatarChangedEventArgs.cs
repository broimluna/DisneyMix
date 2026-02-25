namespace Disney.Mix.SDK.Internal
{
	internal class AvatarChangedEventArgs : AbstractAvatarChangedEventArgs
	{
		public AvatarChangedEventArgs(IAvatar avatar)
		{
			base.Avatar = avatar;
		}
	}
}
