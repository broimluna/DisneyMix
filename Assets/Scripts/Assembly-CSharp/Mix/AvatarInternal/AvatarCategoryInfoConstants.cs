namespace Mix.AvatarInternal
{
	public class AvatarCategoryInfoConstants
	{
		public const float MOUTH_OFFSET_X = 0.375f;

		public const float MOUTH_OFFSET_Y = 57f / 128f;

		public const float NOSE_OFFSET_X = 0.4375f;

		public const float NOSE_OFFSET_Y = 59f / 128f;

		public const float ACC_OFFSET_X = 0.25f;

		public const float ACC_OFFSET_Y = 0.33203125f;

		public const float NO_CROP = 1f;

		public const float HALF_CROP = 2f;

		public const float QUARTER_CROP = 4f;

		public const float EIGHTH_CROP = 8f;

		public const float MULTI_MOUTH_OFFSET_X = 0f;

		public const float MULTI_MOUTH_OFFSET_Y = 0f;

		public const float MULTI_NOSE_OFFSET_X = 0.4375f;

		public const float MULTI_NOSE_OFFSET_Y = 29f / 64f;

		public const float MULTI_BROW_OFFSET_X = 0f;

		public const float MULTI_BROW_OFFSET_Y = 0f;

		public const float MULTI_ACC_OFFSET_X = 0f;

		public const float MULTI_ACC_OFFSET_Y = 0f;

		public static AvatarCategoryInfo[] Categories = new AvatarCategoryInfo[10]
		{
			new AvatarCategoryInfo("Skin", 1, 0.25f, 0.33203125f, 2f, true, false),
			new AvatarCategoryInfo("Mouth", 2, 0.375f, 57f / 128f, 4f, false, false),
			new AvatarCategoryInfo("Nose", 2, 0.4375f, 59f / 128f, 8f, false, true),
			new AvatarCategoryInfo("Eyes", 4, 0.25f, 0.33203125f, 2f, true, true),
			new AvatarCategoryInfo("Brow", 1, 0.25f, 0.33203125f, 2f, false, true),
			new AvatarCategoryInfo("Accessory", 2, 0.25f, 0.33203125f, 2f, true, true),
			new AvatarCategoryInfo("Hair", 3, 0f, 0f, 1f, true, true),
			new AvatarCategoryInfo("Costume", 1, 0f, 0f, 1f, false, true),
			new AvatarCategoryInfo("Glow", 1, 0f, 0f, 1f, false, false),
			new AvatarCategoryInfo("Hat", 2, 0.25f, 0.33203125f, 2f, true, true)
		};

		public static AvatarCategoryInfo[] MultiplaneCategories = new AvatarCategoryInfo[10]
		{
			new AvatarCategoryInfo("Skin", 1, 0.25f, 0.33203125f, 2f, true, false),
			new AvatarCategoryInfo("Mouth", 2, 0f, 0f, 1f, false, false),
			new AvatarCategoryInfo("Nose", 2, 0.4375f, 29f / 64f, 8f, false, false),
			new AvatarCategoryInfo("Eyes", 4, 0f, 0f, 1f, true, false),
			new AvatarCategoryInfo("Brow", 1, 0f, 0f, 1f, false, false),
			new AvatarCategoryInfo("Accessory", 2, 0f, 0f, 1f, true, true),
			new AvatarCategoryInfo("Hair", 3, 0f, 0f, 1f, true, true),
			new AvatarCategoryInfo("Costume", 1, 0f, 0f, 1f, false, false),
			new AvatarCategoryInfo("Glow", 1, 0f, 0f, 1f, false, false),
			new AvatarCategoryInfo("Hat", 2, 0f, 0f, 1f, true, true)
		};
	}
}
