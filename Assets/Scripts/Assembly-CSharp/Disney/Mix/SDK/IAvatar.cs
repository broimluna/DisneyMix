namespace Disney.Mix.SDK
{
	public interface IAvatar
	{
		IAvatarProperty Accessory { get; }

		IAvatarProperty Brow { get; }

		IAvatarProperty Costume { get; }

		IAvatarProperty Eyes { get; }

		IAvatarProperty Hair { get; }

		IAvatarProperty Nose { get; }

		IAvatarProperty Mouth { get; }

		IAvatarProperty Skin { get; }

		IAvatarProperty Hat { get; }
	}
}
