using Avatar.DataTypes;

namespace Mix.Avatar
{
	public class AvatarSnapshotResult
	{
		public bool success;

		public string cacheId;

		public AvatarSnapshotData data;

		public AvatarSnapshotResult(bool aSuccess, string aCacheId, AvatarSnapshotData aData)
		{
			success = aSuccess;
			cacheId = aCacheId;
			data = aData;
		}
	}
}
