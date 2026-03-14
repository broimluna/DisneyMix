namespace Mix.DeviceDb
{
	public interface IAvatarTextureDocumentCollectionApi
	{
		void AddAvatarTextureData(string index, string diffusePath, string normalPath, bool isHd, float loadPercentage);

		AvatarTextureDocument GetAvatarTextureData(string index);

		void RemoveAvatarTextureData(string index);
	}
}
