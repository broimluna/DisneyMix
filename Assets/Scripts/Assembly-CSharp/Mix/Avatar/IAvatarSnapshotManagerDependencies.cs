using System;
using Avatar.DataTypes;
using Mix.DeviceDb;

namespace Mix.Avatar
{
	public interface IAvatarSnapshotManagerDependencies
	{
		void GenerateFolder(string relPath);

		bool DoesFileExist(string relPath);

		void LoadSnapshots(AvatarSnapshotDocument snapshotInfo, LoadSnapshotCallback callback);

		void SaveSnapshots(AvatarSnapshotDocument snapshotInfo, AvatarSnapshotData snapshot, Action<bool> callback);
	}
}
