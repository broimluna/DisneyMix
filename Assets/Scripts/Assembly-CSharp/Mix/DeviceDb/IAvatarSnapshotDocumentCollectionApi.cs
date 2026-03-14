namespace Mix.DeviceDb
{
	public interface IAvatarSnapshotDocumentCollectionApi
	{
		void AddAvatarSnapshotData(string index, string aPath, bool isHd, bool hasNormals, int size, float loadPercentage);

		AvatarSnapshotDocument GetAvatarSnapshotData(string index);

		void RemoveAvatarSnapshotData(string index);
	}
}
