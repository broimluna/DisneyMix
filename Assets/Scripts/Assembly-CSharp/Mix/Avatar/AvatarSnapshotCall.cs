using System;
using Avatar;
using Disney.Mix.SDK;
using UnityEngine;

namespace Mix.Avatar
{
	public class AvatarSnapshotCall : IAsyncCall<AvatarSnapshotResult>
	{
		private string id;

		private IAvatar dna;

		private int size;

		private AvatarFlags flags;

		private Vector3 avatarRotation;

		private Vector3 avatarOffset;

		private IAvatarSnapshotCallDependencies dep;

		private bool cancelled;

		private Action<AvatarSnapshotResult> outgoingRequest;

		private Action cancelRequest;

		public AvatarSnapshotCall(IAvatar aDna, AvatarFlags aFlags, int aSize, Vector3 aAvatarRotation, Vector3 aAvatarOffset, IAvatarSnapshotCallDependencies aDep)
		{
			dna = aDna;
			flags = aFlags;
			size = aSize;
			dep = aDep;
			avatarRotation = aAvatarRotation;
			avatarOffset = aAvatarOffset;
			id = dep.CreateSnapshotId(dna, flags, size, avatarRotation, avatarOffset);
		}

		public void Execute(Action<AvatarSnapshotResult> callback)
		{
			cancelled = false;
			outgoingRequest = delegate(AvatarSnapshotResult result)
			{
				cancelRequest = null;
				outgoingRequest = null;
				if (!cancelled)
				{
					callback(result);
				}
			};
			cancelRequest = dep.GenerateSnapshotFromDna(dna, flags, size, outgoingRequest, avatarRotation, avatarOffset);
		}

		public void Cancel()
		{
			if (outgoingRequest != null && cancelRequest != null)
			{
				cancelled = true;
				cancelRequest();
			}
		}

		public string GetIdentifier()
		{
			return id;
		}
	}
}
