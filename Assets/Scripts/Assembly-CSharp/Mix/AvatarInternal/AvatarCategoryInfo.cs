namespace Mix.AvatarInternal
{
	public class AvatarCategoryInfo
	{
		public string name;

		public int layerCount;

		public float xOffset;

		public float yOffset;

		public float crop;

		public bool hasColor;

		public bool hasNormal;

		public AvatarCategoryInfo(string aName, int aLayerCount, float aXOffset, float aYOffset, float aCrop, bool aHasColor, bool aHasNormal)
		{
			name = aName;
			layerCount = aLayerCount;
			xOffset = aXOffset;
			yOffset = aYOffset;
			crop = aCrop;
			hasColor = aHasColor;
			hasNormal = aHasNormal;
		}
	}
}
