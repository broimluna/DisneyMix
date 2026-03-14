namespace Disney.Mix.SDK.Internal
{
	public class Avatar : IInternalAvatar, IAvatar
	{
		public IAvatarProperty Accessory
		{
			get
			{
				return InternalAccessory;
			}
		}

		public IAvatarProperty Brow
		{
			get
			{
				return InternalBrow;
			}
		}

		public IAvatarProperty Costume
		{
			get
			{
				return InternalCostume;
			}
		}

		public IAvatarProperty Eyes
		{
			get
			{
				return InternalEyes;
			}
		}

		public IAvatarProperty Hair
		{
			get
			{
				return InternalHair;
			}
		}

		public IAvatarProperty Nose
		{
			get
			{
				return InternalNose;
			}
		}

		public IAvatarProperty Mouth
		{
			get
			{
				return InternalMouth;
			}
		}

		public IAvatarProperty Skin
		{
			get
			{
				return InternalSkin;
			}
		}

		public IAvatarProperty Hat
		{
			get
			{
				return InternalHat;
			}
		}

		public IInternalAvatarProperty InternalAccessory { get; set; }

		public IInternalAvatarProperty InternalBrow { get; set; }

		public IInternalAvatarProperty InternalCostume { get; set; }

		public IInternalAvatarProperty InternalEyes { get; set; }

		public IInternalAvatarProperty InternalHair { get; set; }

		public IInternalAvatarProperty InternalNose { get; set; }

		public IInternalAvatarProperty InternalMouth { get; set; }

		public IInternalAvatarProperty InternalSkin { get; set; }

		public IInternalAvatarProperty InternalHat { get; set; }

		public long AvatarId { get; set; }

		public int SlotId { get; set; }

		public Avatar()
			: this(new AvatarProperty(), new AvatarProperty(), new AvatarProperty(), new AvatarProperty(), new AvatarProperty(), new AvatarProperty(), new AvatarProperty(), new AvatarProperty(), new AvatarProperty(), long.MinValue, 0)
		{
		}

		public Avatar(IInternalAvatarProperty accessory, IInternalAvatarProperty brow, IInternalAvatarProperty costume, IInternalAvatarProperty eyes, IInternalAvatarProperty nose, IInternalAvatarProperty mouth, IInternalAvatarProperty skin, IInternalAvatarProperty hair, IInternalAvatarProperty hat, long avatarId, int slotId)
		{
			InternalAccessory = accessory;
			InternalBrow = brow;
			InternalCostume = costume;
			InternalEyes = eyes;
			InternalNose = nose;
			InternalMouth = mouth;
			InternalSkin = skin;
			InternalHair = hair;
			InternalHat = hat;
			AvatarId = avatarId;
			SlotId = slotId;
		}
	}
}
