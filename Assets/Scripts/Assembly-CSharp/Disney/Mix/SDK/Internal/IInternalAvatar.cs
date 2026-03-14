namespace Disney.Mix.SDK.Internal
{
	public interface IInternalAvatar : IAvatar
	{
		IInternalAvatarProperty InternalAccessory { get; set; }

		IInternalAvatarProperty InternalBrow { get; set; }

		IInternalAvatarProperty InternalCostume { get; set; }

		IInternalAvatarProperty InternalEyes { get; set; }

		IInternalAvatarProperty InternalHair { get; set; }

		IInternalAvatarProperty InternalNose { get; set; }

		IInternalAvatarProperty InternalMouth { get; set; }

		IInternalAvatarProperty InternalSkin { get; set; }

		IInternalAvatarProperty InternalHat { get; set; }

		long AvatarId { get; set; }

		int SlotId { get; set; }
	}
}
