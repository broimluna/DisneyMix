using System;
using Avatar;
using Disney.Mix.SDK;
using UnityEngine;

namespace Mix.Avatar
{
	public interface IAvatarSnapshotCallDependencies
	{
		Action GenerateSnapshotFromDna(IAvatar dna, AvatarFlags flags, int size, Action<AvatarSnapshotResult> callback, Vector3 avatarRotation, Vector3 avatarOffset);

		string CreateSnapshotId(IAvatar dna, AvatarFlags flags, int size, Vector3 avatarRotation, Vector3 avatarOffset);
	}
}
