using Avatar.DataTypes;

namespace Mix.Avatar
{
	public class SnapshotInformation
	{
		public AvatarSnapshotData data;

		public int refCount;

		public SnapshotInformation(AvatarSnapshotData aData)
		{
			data = aData;
		}
	}
}
